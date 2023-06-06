using System.ComponentModel.DataAnnotations;

namespace LAMovies_NET6.Models
{
    public class OddMovie
    {
        [Key]
        public int idOddMovie { get; set; }
        public int idMovie { get; set; }
        [Required]
        public string urlMovie { get; set; }
        public Movie Movie { get; set; }
    }
}
