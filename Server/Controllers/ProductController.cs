using _3legant.Server.Interfaces;
using _3legant.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _3legant.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class ProductController : ControllerBase
    {
        private IShopService _shopService;

        public ProductController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpGet("IndividualProduct")]
        public async Task<ActionResult<ProductModel>> IndividualProduct([FromQuery] int ProductId)
        {
            var _product = await _shopService.GetProductById(ProductId);
            return _product;
        }
    }
}
