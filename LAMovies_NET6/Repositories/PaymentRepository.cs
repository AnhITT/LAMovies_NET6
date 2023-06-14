using LAMovies_NET6.Data;
using LAMovies_NET6.Interfaces;
using LAMovies_NET6.Migrations;
using LAMovies_NET6.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LAMovies_NET6.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _data;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public PaymentRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _data = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public void SaveDataService(int id)
        {
            var user = _httpContextAccessor.HttpContext.User;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var pricing = _data.Pricings.FirstOrDefault(p => p.idPricing == id);
            var checkUserPricing = _data.UserPricings.FirstOrDefault(
                                          up => up.idUser == userId);
            if(checkUserPricing == null)
            {
                UserPricing userPricing = new UserPricing
                {
                    idPricing = id,
                    idUser = userId,
                    startTime = DateTime.Now,
                    endTime = DateTime.Now.AddMonths(pricing.timePricing),
                    totalAmount = pricing.pricePricing
                };
                _data.Add(userPricing);
                _data.SaveChanges();
            }
            else
            {
                //Hết hạn
                if (checkUserPricing.endTime < DateTime.Now)
                {
                    UserPricing updateUserPricing = new UserPricing()
                    {
                        idPricing = id,
                        idUser = userId,
                        startTime = DateTime.Now,
                        endTime = DateTime.Now.AddMonths(pricing.timePricing),
                        totalAmount = checkUserPricing.totalAmount + pricing.pricePricing 
                    };
                    _data.Remove(checkUserPricing);
                    _data.Add(updateUserPricing);
                    _data.SaveChanges();
                }
                //Chưa hết hạn, đăng ký thêm
                else
                {

                    UserPricing updateUserPricing = new UserPricing()
                    {
                        idUser = userId,
                        endTime = checkUserPricing.endTime.AddMonths(pricing.timePricing),
                        startTime = checkUserPricing.startTime,
                        totalAmount = checkUserPricing.totalAmount + pricing.pricePricing
                    };
                    if (id > checkUserPricing.idPricing)
                    {
                        updateUserPricing.idPricing = id;
                    }
                    else
                    {
                        updateUserPricing.idPricing = checkUserPricing.idPricing;
                    }
                    _data.Remove(checkUserPricing);
                    _data.Add(updateUserPricing);
                    _data.SaveChanges();
                }
            }
            
        }
        
    }
}
