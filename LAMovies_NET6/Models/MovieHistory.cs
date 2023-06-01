using System.ComponentModel.DataAnnotations.Schema;

namespace LAMovies_NET6.Models
{
    public class MovieHistory
    {
        public int idMovie { get; set; }
        public string idUser { get; set; }
        public DateTime dateTimeWatch { get; set; }
        public TimeSpan? remainingTime { get; set; }
        public int? minutes { get; set; }
        public bool? status { get; set; }    
        public Movie Movie { get; set; }
        public User User { get; set; }

    }
}
