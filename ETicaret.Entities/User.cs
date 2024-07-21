using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Entities
{
    public class User: IEntity
    {
        public int Id { get; set; }
        [StringLength(50)]
        [Display(Name = "İsim")]
        public string Name { get; set; }
        [StringLength(50), Required(ErrorMessage =" {0} Boş bırakılamaz!")]
        [Display(Name = "Soyisim")]
        public string Surname { get; set; }
        [StringLength(50), Required(ErrorMessage = " {0} Boş bırakılamaz!")]
        public string Email { get; set; }
        [StringLength(20)]
        [Display(Name = "Telefon")]
        public string? Phone { get; set; }
        [StringLength(50), Required(ErrorMessage = " {0} Boş bırakılamaz!")]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
        [Display(Name = "Oluşturulma Tarihi"), ScaffoldColumn(false)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        //[Required(ErrorMessage = " {0} Boş bırakılamaz!")]
        public int RoleId { get; set; }
        public virtual Role? Role { get; set; }
        public Guid? UserGuid { get; set; } = Guid.NewGuid();
        
    }
}
