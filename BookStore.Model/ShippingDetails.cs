using System.ComponentModel.DataAnnotations;

namespace BookStore.Model
{
    public class ShippingDetails
    {
        [Display(Name = "Your contact name")]
        [Required(ErrorMessage = "Your Name")]
        public string Name { get; set; }

        [Display(Name = "First Address")]
        [Required(ErrorMessage = "Shipping addres")]
        public string Addres_1 { get; set; }

        [Required(ErrorMessage = "Shipping addres")]
        [Display(Name = "Second Address")]
        public string Addres_2 { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "Enter your City")]
        public string City { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "Enter your Country")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}
