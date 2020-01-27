using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Models;
using server.Models.Converters;
using server.Models.Data;
using server.Models.ViewModels;
using server.Service;

namespace server.Controllers
{
    [ApiController]
    public class CartController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly Context context;
        private readonly ICartUpdateService updateService;

        public CartController(UserManager<User> userManager, Context context, ICartUpdateService updateService)
        {
            this.userManager = userManager;
            this.context = context;
            this.updateService = updateService;
        }

        [Route("cart/view")]
        [Authorize]
        public async Task<IActionResult> View()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name).ConfigureAwait(false);
            var cart = await context.Carts
                .Where(x => x.UserId == user.Id)
                .Include(x => x.Items)
                .ThenInclude(y => y.Product)
                .ThenInclude(z => z.Mesurement)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if (cart != null)
            {
                return Ok(CartConverter.Convert(cart));
            } else
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpPost]
        [Route("cart/update")]
        public async Task<IActionResult> Update([FromBody]CartViewModel viewModel)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name).ConfigureAwait(false);
            var cart = await context.Carts
                .Where(x => x.UserId == user.Id)
                .Include(x => x.Items)
                .ThenInclude(y => y.Product)
                .ThenInclude(z => z.Mesurement)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            var newCart = updateService.UpdateCart(cart, viewModel, user);
            if (string.IsNullOrEmpty(newCart.Id))
            {
                context.Carts.Add(newCart);
            } else
            {
                context.Carts.Update(newCart);
            }
            await context.SaveChangesAsync().ConfigureAwait(false);
            return Ok(CartConverter.Convert(newCart));
        }

    }
}