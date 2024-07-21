using System.ComponentModel.DataAnnotations;
using System;

namespace ETicaret.Entities
{
    public class Orders: IEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="{0} Boş Bırakılamaz!")]
        public int UserId { get; set; }
        [Display(Name = "Sipariş Tarihi")]
        public DateTime OrderDate { get; set; }
        [Display(Name = "Teslim Tarihi")]
        public DateTime DeliveryDate { get; set; }
        [Required(ErrorMessage = "{0} Boş Bırakılamaz!")]
        public int Amount { get; set; }
        [StringLength(200)]
        [Display(Name = "Not")]
        public string? Notes { get; set; }
        [StringLength(200),Required(ErrorMessage ="{0} Boş Bırakılamaz!")]
        [Display(Name ="Adres")]
        public string Adress { get; set; }
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public virtual User? User { get; set; } 
    }
}
