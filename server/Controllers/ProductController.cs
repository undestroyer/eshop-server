using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Models;
using server.Models.Converters;
using server.Models.ViewModels;

namespace server.Controllers
{
    [ApiController]
    public class ProductController : Controller
    {

        const int PRODUCT_PER_PAGE = 10;

        private readonly Context context;
        public ProductController(Context context)
        {
            this.context = context;
        }
        
        [Route("product/index")]
        public async Task<IActionResult> Index([FromQuery] int page = 1, [FromQuery] string nameFilter = "")
        {
            var productQuery = context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(nameFilter))
            {
                productQuery = productQuery.Where(x => x.Name.Contains(nameFilter));
            }
            var products = await productQuery
                .OrderBy(x => x.Name)
                .Include(x => x.Mesurement)
                .Take(PRODUCT_PER_PAGE)
                .Skip((page - 1) * PRODUCT_PER_PAGE)
                .ToListAsync();
            var result = new List<ProductViewModel>();
            foreach (var product in products)
            {
                result.Add(ProductConverter.ConvertProduct(product));
            }
            return Json(result);
        }

    }
}