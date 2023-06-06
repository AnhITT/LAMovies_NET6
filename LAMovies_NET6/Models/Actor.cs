using System.ComponentModel.DataAnnotations;

namespace LAMovies_NET6.Models
{
    public class Actor
    {
        [Key]
        public int idActor { get; set; }
        [Required]
        public string nameActor { get; set; }
        public string avartaActor { get; set; }
        public string describeActor { get; set; }
        public ICollection<MovieActor> MovieActors { get; set; }

    }
}
