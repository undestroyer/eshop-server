using server.Models.Data;
using server.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Models.Converters
{
    public class ProductConverter
    {
        public static ProductViewModel ConvertProduct(Product product)
        {
            if (product.Mesurement == null)
                throw new ArgumentNullException(nameof(product), "У товара должно быть заполнено поле Mesurement");
            var mesurementVM = new MesurementViewModel
            {
                Name = product.Mesurement.Name,
                Size = product.Mesurement.Size
            };
            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Mesurement = mesurementVM
            };
        }
    }
}
