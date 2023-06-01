using System.ComponentModel.DataAnnotations;

namespace LAMovies_NET6.Models
{
    public class SeriesMovies
    {
        [Key]
        public int idSeries { get; set; }
        [Required]
        public int idMovie { get; set; }
        [Required]
        public int episodes { get; set; }
        [Required]
        public int practice { get; set;}

        public Movie Movie { get; set; }
    }
}
