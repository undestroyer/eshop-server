namespace server.Models.Data
{
    public class CartItem
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public int Amount { get; set; }
        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }
}
