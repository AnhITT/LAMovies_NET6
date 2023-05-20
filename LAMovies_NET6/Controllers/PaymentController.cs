using LAMovies_NET6.Interfaces;
using LAMovies_NET6.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace LAMovies_NET6.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentRepository _paymentRepository;
        public PaymentController(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        public IActionResult PaymentPricing(int id)
        {
            var payment = _paymentRepository.Payment(id);
            if(payment == null)
            {
                return NotFound();
            }
            else
            {
                return View(payment);
            }
        }
        
    }
}
