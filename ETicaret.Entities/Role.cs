using System.ComponentModel.DataAnnotations;

namespace ETicaret.Entities
{
    public class Role: IEntity
    {
        public int Id { get; set; }
        [StringLength(50), Required(ErrorMessage = "{0} Boş Bırakılamaz!")]
        public string Name { get; set; }
    }
}
