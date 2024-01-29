namespace _3legant.Shared.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
        public string Measurements { get; set; }
        public bool IsProductNew { get; set; }
        public DateTime CountDownOffer { get; set; } = DateTime.Now.AddDays(48);
    }
}
