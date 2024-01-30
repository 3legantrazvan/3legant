using _3legant.Server.Interfaces;
using _3legant.Server.Models;
using _3legant.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _3legant.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class CatalogController : ControllerBase
    {
        private IShopService _shopService;

        public CatalogController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpGet("CatalogProducts")]
        public async Task<ActionResult<ProductsResponseModel>> CatalogProducts([FromQuery] CatalogQueryParametersModel catalogQueryParametersModel)
        {
            var _products = await _shopService.GetProducts(catalogQueryParametersModel);
            var _totalPages = await _shopService.GetTotalPages(catalogQueryParametersModel);

            return new ProductsResponseModel
            {
                Products = _products,
                PaginationInfo = new PaginationInfoModel
                {
                    CurrentPage = catalogQueryParametersModel.Page,
                    TotalPages = _totalPages
                }
            };
        }

        [HttpGet("PriceRangeFilter")]
        public async Task<ActionResult<IList<OptionsModel>>> PriceRangeFilter()
        {
            var _priceOptions = new List<OptionsModel>()
                    {
                        new OptionsModel { Value = "00.00-"+decimal.MaxValue, Text = "All Price", Selected = true},
                        new OptionsModel { Value = "00.00-99", Text = "0.00-99.99" },
                        new OptionsModel { Value = "100-199", Text = "$100.00-199.99" },
                        new OptionsModel { Value = "200-299", Text = "$200.00-299.99" },
                        new OptionsModel { Value = "300-399", Text = "$300.00-399.99" },
                        new OptionsModel { Value = "400-"+decimal.MaxValue, Text = "$400.00+" },
                    };
            return Ok(_priceOptions);
        }

        [HttpGet("CategoriesFilter")]
        public ActionResult<IList<string>> CategoriesFilter()
        {
            var _categoryOptions = new List<string>
                    {
                        "All Rooms", "Living Room", "Kitchen", "Bathroom", "Dinning", "Outdoor"
                    };
            return Ok(_categoryOptions);
        }

        [HttpGet("SortOptions")]
        public ActionResult<IList<SortOptionModel>> SortOptions()
        {
            var _sortOptions = new List<SortOptionModel>
            {
                new SortOptionModel { Value = "PriceLowToHigh", Text = "Price Low to High" },
                new SortOptionModel { Value = "PriceHighToLow", Text = "Price High to Low" },
                new SortOptionModel { Value = "NameAZ", Text = "Name A-Z" },
                new SortOptionModel { Value = "NameZA", Text = "Name Z-A" },
            };

            return Ok(_sortOptions);
        }

    }
}