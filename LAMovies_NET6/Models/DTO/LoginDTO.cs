using Microsoft.Build.Framework;

namespace LAMovies_NET6.Models.DTO
{
    public class LoginDTO
    {
        [Required]
        public string userName { get; set; }
        [Required]
        public string password { get; set; }
    }
}
