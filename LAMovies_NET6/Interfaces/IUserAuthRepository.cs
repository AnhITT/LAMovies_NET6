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
        List<User> GetAllAccount();
        Task<User> GetAccountById(string id);
        Task<bool> Update(User user);
        Task<Respone> UpdatePassword(CheckPasswordDTO checkPasswordDTO);
        Task<bool> DeleteAccount(string id);
    }
}
