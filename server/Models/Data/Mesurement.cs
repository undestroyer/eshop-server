using System.Collections.Generic;

namespace server.Models.Data
{
    public class Mesurement
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Size { get; set; }
        public List<Product> Products { get; set; }
    }
}
