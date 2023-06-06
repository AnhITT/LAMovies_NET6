using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LAMovies_NET6.Models
{
    public class Movie
    {
        [Key]
        public int idMovie { get; set; }
        [Required]
        public string nameMovie { get; set; }
        [Required]
        public string? descriptionMovie { get; set; }
        [Required]
        public string uriMovie { get; set; }
        [Required]
        public string uriMovieTrailer { get; set; }
        [Required]
        public string uriImg { get; set; }
        [Required]
        public string uriImgCover { get; set; }

        [Required]
        public string subLanguageMovie { get; set; }
        [Required]
        public int minAgeMovie { get; set; }
        [Required]
        public string qualityMovie { get; set; }
        [Required]
        public string timeMovie { get; set; }
        [Required]
        public string yearCreateMovie { get; set; }
        [Required]
        public string typeMovie { get; set; }
        public int viewMovie { get; set; }
        public ICollection<MovieGenre> MovieGenre { get; set; }
        [NotMapped]
        [Required]
        public List<int>? Genres { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem>? GenreList { get; set; }
        [NotMapped]
        public List<string>? GenreNames { get; set; }
        [NotMapped]
        public MultiSelectList? MultiGenreList { get; set; }
       
        public ICollection<MovieHistory> MovieHistory { get; set; }

        public OddMovie OddMovie { get; set; }
        public ICollection<SeriesMovie> SeriesMovie { get; set; }
        public ICollection<MovieActor> MovieActors { get; set; }
        [NotMapped]
        [Required]
        public List<int>? Actor { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem>? ActorList { get; set; }
        [NotMapped]
        public List<string>? ActorNames { get; set; }
        [NotMapped]
        public MultiSelectList? MultiActorList { get; set; }
    }
}
