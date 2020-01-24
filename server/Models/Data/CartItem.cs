namespace server.Models.Data
{
    public class CartItem
    {
        public string Id { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
    }
}
