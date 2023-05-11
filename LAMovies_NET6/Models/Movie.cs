﻿using System.ComponentModel.DataAnnotations;

namespace LAMovies_NET6.Models
{
    public class Movie
    {
        [Key]
        public int idMovie { get; set; }
        public string nameMovie { get; set; }
        public string? descriptionMovie { get; set; }
        public string uriMovie { get; set; }
        public string uriMovieTrailer { get; set; }
        public string uriImg { get; set; }
        public string uriImgCover { get; set; }

        public string subLanguageMovie { get; set; }
        public int minAgeMovie { get; set; }
        public string qualityMovie { get; set; }
        public string timeMovie { get; set; }
        public string yearCreateMovie { get; set; }
        public int viewMovie { get; set; }
    }
}