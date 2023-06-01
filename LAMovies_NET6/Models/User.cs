using Microsoft.AspNetCore.Identity;

namespace LAMovies_NET6.Models
{
    public class User : IdentityUser
    {
        public string fullName { set; get; }
        public DateTime dateBirthdayUser { get; set; }
        public ICollection<UserPricing> UserPricing { get; set; }
        public ICollection<MovieHistory> MovieHistory { get; set; }


    }
}
