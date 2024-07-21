using System.ComponentModel.DataAnnotations;

namespace ETicaretProje.Models
{
    public class CustomerLoginViewModel
    {
        [StringLength(50), Required(ErrorMessage = " {0} Boş bırakılamaz!")]
        public string Email { get; set; }
        [StringLength(50), Required(ErrorMessage = " {0} Boş bırakılamaz!")]
        [Display(Name ="Şifre")]
        public string Password { get; set; }
    }
}
