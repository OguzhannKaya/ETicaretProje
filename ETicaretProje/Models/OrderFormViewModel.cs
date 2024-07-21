using ETicaret.Entities;

namespace ETicaretProje.Models
{
    public class OrderFormViewModel
    {
        public User User { get; set; }
        public Orders Order { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        
    }
}
