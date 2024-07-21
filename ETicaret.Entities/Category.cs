using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Entities
{
    public class Category: IEntity
    {
        public int Id { get; set; }
        [StringLength(50),Display(Name= "Kategori Adı")]
        public string Name { get; set; }
        [StringLength(800), Required(ErrorMessage ="{0} Boş Bırakılamaz!")]
        public string Description { get; set; }
        [StringLength(100)]
        public string? Image {  get; set; }
        [Required(ErrorMessage = "{0} Boş Bırakılamaz!")]
        public decimal Fiyat {  get; set; }
    }
}
