namespace server.Models.Data
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public Mesurement Mesurement { get; set; }
    }
}
