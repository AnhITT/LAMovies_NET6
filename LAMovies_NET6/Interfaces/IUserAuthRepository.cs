using LAMovies_NET6.Models;
using LAMovies_NET6.Models.DTO;

namespace LAMovies_NET6.Interfaces
{
    public interface IUserAuthRepository
    {
        Task<Respone> RegisterAsync(RegistrationDTO model);
        Task<Respone> LoginAsync(LoginDTO model);
        Task LogoutAsync();
        Task<User> GetInfoAccount();
    }
}
