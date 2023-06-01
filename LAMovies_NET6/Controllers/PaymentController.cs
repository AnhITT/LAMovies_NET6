using AutoMapper;
using Azure.Core;
using BraintreeHttp;
using LAMovies_NET6.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayPal.Core;
using PayPal.v1.Payments;
using System.Net.Http.Headers;
using System.Text;

namespace LAMovies_NET6.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUserAuthRepository _userAuthRepository;
        private readonly IPricingRepository _priceRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly string _clientId;
        private readonly string _secretKey;
        public PaymentController(IPaymentRepository paymentRepository, IUserAuthRepository userAuthRepository, IPricingRepository pricingRepository, IMapper mapper, IConfiguration config)
        {
            _paymentRepository = paymentRepository;
            _userAuthRepository = userAuthRepository;
            _priceRepository = pricingRepository;
            _mapper = mapper;
            _clientId = config["PaypalSettings:ClientId"];
            _secretKey = config["PaypalSettings:SecretKey"];
        }
        public async Task<IActionResult> ViewPayment(int id)
        {
            var user = await _userAuthRepository.GetInfoAccount();
            ViewBag.User = user;
            var pricingInfo = await _priceRepository.GetPricingById(id);
            ViewBag.InfoPricing = pricingInfo;
            return View();
        }
        [Authorize]
        public async System.Threading.Tasks.Task<IActionResult> PaypalCheckout(int idPricing)
        {
            var environment = new SandboxEnvironment(_clientId, _secretKey);
            var client = new PayPalHttpClient(environment);
            var pricing = await _priceRepository.GetPricingById(idPricing);
            var item = new Item()
            {
                Name = pricing.namePricing,
                Currency = "USD",
                Price = pricing.pricePricing.ToString(),
                Quantity = "1",
                Sku = "sku",
                Tax = "0",
                Description = pricing.timePricing.ToString() + " month"
            };
            var paypalOrderId = DateTime.Now.Ticks;
            var hostname = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            var payment = new Payment()
            {
                Intent = "sale",
                Transactions = new List<Transaction>()
                {
                    new Transaction()
                    {
                        Amount = new Amount()
                        {
                            Total = item.Price,
                            Currency = "USD",
                            Details = new AmountDetails
                            {
                                Tax = "0",
                                Subtotal = item.Price
                            }
                        },
                        Description = $"Invoice #{paypalOrderId}",
                        InvoiceNumber = paypalOrderId.ToString()
                    }
                },
                RedirectUrls = new RedirectUrls()
                {
                    CancelUrl = $"{hostname}/Payment/CheckoutFail",
                    ReturnUrl = $"{hostname}/Payment/CheckoutSuccess?idPricing={idPricing}"
                },
                Payer = new Payer()
                {
                    PaymentMethod = "paypal"
                }
            };
            PaymentCreateRequest request = new PaymentCreateRequest();
            request.RequestBody(payment);

            try
            {
                var response = await client.Execute(request);
                var statusCode = response.StatusCode;
                Payment result = response.Result<Payment>();

                var links = result.Links.GetEnumerator();
                string paypalRedirectUrl = null;
                while (links.MoveNext())
                {
                    LinkDescriptionObject lnk = links.Current;
                    if (lnk.Rel.ToLower().Trim().Equals("approval_url"))
                    {
                        paypalRedirectUrl = lnk.Href;
                    }
                }
                return Redirect(paypalRedirectUrl);

            }
            catch (HttpException httpException)
            {
                var statusCode = httpException.StatusCode;
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();

                //Process when Checkout with Paypal fails
                return Redirect("/Payment/CheckoutFail");
            }
        }
        
        public IActionResult CheckoutFail()
        {
            return View();
        }

        public IActionResult CheckoutSuccess(int idPricing)
        {
            _paymentRepository.SaveDataService(idPricing);
            return View();
        }

    }
}
