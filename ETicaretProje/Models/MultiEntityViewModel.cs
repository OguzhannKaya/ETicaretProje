using ETicaret.Entities;

namespace ETicaretProje.Models
{
    public class MultiEntityViewModel
    {
        public Product Product { get; set; }
        public Category Category { get; set; }
        public User Users { get; set; }
        public Orders Orders { get; set; }
        public OrderFormViewModel OrderFormViewModel => new() { CategoryId = Category.Id, Order = Orders, ProductId = Product.Id, User = Users };
    }
}
