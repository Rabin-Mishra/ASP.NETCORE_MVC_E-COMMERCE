namespace OnlineShopping.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }   
        
        public virtual IList<Product>? Products { get; set; }//size define nagari chaiyeko size define garna lai list use gareko la
                                                            
    }
}
