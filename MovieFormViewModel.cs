using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LabMVC.Models;

namespace LabMVC.ViewModels
{
    public class MovieFormViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }

        //Postavljamo "PURE ViewModel" Movie mijenjamo sa njegovim Properties-ima 
        //public Movie Movie { get; set; }

        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        //Genre nam ne treba jer svi Genre u formi su GenreId
        //public Genre Genre { get; set; }

        [Display(Name = "Genre")]
        [Required]
        public byte? GenreId { get; set; } //EF po konvenciji prepozna byte GenreId i tretira ga kao Foreign Key

        [Display(Name = "Release Date")]
        [Required]
        public DateTime? ReleaseDate { get; set; }

        [Display(Name = "Number In Stock")]
        [Range(1, 20)]
        [Required]
        public int? NumberInStock { get; set; }

        public string Title
        {
            get
            {
                return Id != 0 ? "Edit Product" : "New Product";
            }
        }
        //public string Title
        //{
        //    get
        //    {
        //        if (Id != 0)
        //            return "Edit Product";
        //        return "New Product";
        //    }
        //}

        public MovieFormViewModel()
        {
            Id = 0; //Postavljamo Id za Hidden field
        }

        //Prosljedjena movie referenca iz Controller Edit Action je:
        //  var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
        public MovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
            GenreId = movie.GenreId;
        }
    }
}