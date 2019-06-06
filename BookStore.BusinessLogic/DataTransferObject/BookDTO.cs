
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BookStore.BusinessLogic.DataTransferObject
{
    public class BookDTO
    {
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "ID")]
        public int BookId { get; set; }

        [Display(Name = "Book Title")]
        public string Name { get; set; }

        [Display(Name = "Author")]
        public string Author { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Genre")]
        public string Genre { get; set; }

        [Display(Name = "Price - $")]
        public decimal Price { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
