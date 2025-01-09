//namespace OnlineShopping.Models
//{
//    public class Product
//    {
//        public int Id { get; set; }
//        public string Title { get; set; }
//        public string Description { get; set; }
//        public int Price { get; set; }
//        public int CategoryId { get; set; }
//        public string? ProductIcon { get; set; }

//        public virtual Category? Category { get; set; }//yo table ma xaina so vitual banako relationship rakhna ko lagi
//        public string ProductImage { get; internal set; }
//    }
//}
namespace OnlineShopping.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; }
        public string? ProductIcon { get; set; }

        public virtual Category? Category { get; set; } // Virtual for lazy loading relationships
        
    }
}
