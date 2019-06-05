

using System.ComponentModel.DataAnnotations;

namespace BookStore.Model
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Your Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Shipping addres")]
        [Display(Name = "First Address")]
        public string Addres_1 { get; set; }
        [Display(Name = "Second Address")]
        public string Addres_2 { get; set; }

        [Required(ErrorMessage = "Enter your City")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Enter your Country")]
        [Display(Name = "Country")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}
