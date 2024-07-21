using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Entities
{
    public class Product: IEntity
    {
        public int Id { get; set; }
        [Display(Name ="Kategori Adı") ,Required(ErrorMessage = "{0} Boş Bırakılamaz!")]
        public int CategoryId { get; set; }
        [StringLength(50), Required(ErrorMessage = "{0} Boş Bırakılamaz!")]
        public string Name { get; set; }
        [Required(ErrorMessage ="{0} Boş Bırakılamaz")]
        public decimal Price { get; set; }
        [StringLength(800),Required(ErrorMessage = "{0} Boş Bırakılamaz!")]
        public string Description { get; set; }
        [Required(ErrorMessage = "{0} Boş Bırakılamaz!")]
        public int Stock { get; set; }
        public bool IsApproved { get; set; }
        public string? Image1 { get; set; }
        public string? Image2 { get; set; }
        public virtual Category? Category { get; set; }
    }
}
