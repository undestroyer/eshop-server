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
using server.Models.Data;

namespace server.Controllers
{
    [ApiController]
    public class CartController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly Context context;

        public CartController(UserManager<User> userManager, Context context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        [Route("cart/view")]
        [Authorize]
        public async Task<IActionResult> View()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name).ConfigureAwait(false);
            /// TODO: найти корзину пользователя
            return Ok();
        }

    }
}