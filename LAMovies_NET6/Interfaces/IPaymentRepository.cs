using LAMovies_NET6.Models;

namespace LAMovies_NET6.Interfaces
{
    public interface IPaymentRepository
    {
        UserPricing Payment(int id);
    }
}
