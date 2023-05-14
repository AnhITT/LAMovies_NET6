using System.ComponentModel.DataAnnotations;

namespace LAMovies_NET6.Models
{
    public class Genre
    {
        [Key]
        public int idGenre { get; set; }
        [Required]
        public string nameGenre { get; set; }
        public ICollection<MovieGenre> MovieGenre { get; set; }

    }
}
