using LAMovies_NET6.Data;
using LAMovies_NET6.Interfaces;
using LAMovies_NET6.Models;
using LAMovies_NET6.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System.Security.Claims;

namespace LAMovies_NET6.Repositories
{
    public class UserAuthRepository : IUserAuthRepository
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<User> signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAuthRepository(UserManager<User> userManager,
            SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Respone> RegisterAsync(RegistrationDTO model)
        {
            var respone = new Respone();
            var userExists = await userManager.FindByNameAsync(model.userName);
            //Check tài khoản trùng
            if (userExists != null)
            {
                respone.statusCode = 0;
                respone.message = "User already exist";
                return respone;
            }
            User user = new User()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = model.email,
                UserName = model.userName,
                fullName = model.fullName,
                dateBirthdayUser = model.dateBirthday,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            var result = await userManager.CreateAsync(user, model.password);
            if (!result.Succeeded)
            {
                respone.statusCode = 0;
                respone.message = "User creation failed";
                return respone;
            }

            if (!await roleManager.RoleExistsAsync(model.Role))
                await roleManager.CreateAsync(new IdentityRole(model.Role));


            if (await roleManager.RoleExistsAsync(model.Role))
            {
                await userManager.AddToRoleAsync(user, model.Role);
            }

            respone.statusCode = 1;
            respone.message = "You have registered successfully";
            return respone;
        } 
        public async Task<Respone> LoginAsync(LoginDTO model)
        {
            var respone = new Respone();
            var user = await userManager.FindByNameAsync(model.userName);
            if (user == null)
            {
                respone.statusCode = 0;
                respone.message = "Invalid username";
                return respone;
            }

            if (!await userManager.CheckPasswordAsync(user, model.password))
            {
                respone.statusCode = 0;
                respone.message = "Invalid Password";
                return respone;
            }

            var signInResult = await signInManager.PasswordSignInAsync(user, model.password, false, true);

            if (signInResult.Succeeded)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var isInRole = await userManager.IsInRoleAsync(user, "admin");
                if (isInRole)
                {
                    respone.statusCode = 1;
                    respone.message = "Logged Admin in successfully";
                }
                else
                {
                    respone.statusCode = 2;
                    respone.message = "Logged User in successfully";
                }
            }
            else if (signInResult.IsLockedOut)
            {
                respone.statusCode = 0;
                respone.message = "User is locked out";
            }
            else
            {
                respone.statusCode = 0;
                respone.message = "Error on logging in";
            }

            return respone;
        }
        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }
        public async Task<User> GetInfoAccount()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext.User.Identity.IsAuthenticated)
            {
                var userManager = httpContext.RequestServices.GetRequiredService<UserManager<User>>();
                var user = await userManager.GetUserAsync(httpContext.User);

                if (user != null)
                {
                    var email = user.Email;

                    return user;
                }
            }
            return null; 
        }
        public List<User> GetAllAccount()
        {
            var users = userManager.Users.ToList();
            // Xử lý danh sách tài khoản ở đây

            return users;
        }

        public async Task<User> GetAccountById(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            return user;
        }
    }
}
