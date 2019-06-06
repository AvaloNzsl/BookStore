using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BookStore.Model
{
    public class Book
    {
        [HiddenInput(DisplayValue=false)]
        [Display(Name = "ID")]
        public int BookId { get; set; }

        [Display(Name="Book Title")]
        [Required(ErrorMessage = "Please, enter book Title")]
        public string Name { get; set; }

        [Display(Name = "Author")]
        [Required(ErrorMessage = "Please, enter Author name")]
        public string Author { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Please, enter a Description of the book")]
        public string Description { get; set; }

        [Display(Name = "Genre")]
        [Required(ErrorMessage = "Please, specify the Genre of the book")]
        public string Genre { get; set; }

        [Display(Name = "Price - $")]
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please, enter a positive Integer value")]
        public decimal Price { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
