using _3legant.Server.Interfaces;
using _3legant.Shared.Models;
using Newtonsoft.Json;

namespace _3legant.Server.Services
{
    public class ShopService : IShopService
    {

        private readonly string _jsonFilePath = Path.Combine("Sample-Data", "Products.json");
        private List<ProductModel> _allProducts;
        public ShopService()
        {
            _allProducts = JsonConvert.DeserializeObject<List<ProductModel>>(File.ReadAllText(_jsonFilePath)) ?? new List<ProductModel>();
        }

        public Task<List<ProductModel>> GetProducts(CatalogQueryParametersModel catalogQueryParametersModel)
        {
            if (catalogQueryParametersModel.PriceRanges.Count > 1)
            {
                var _valueToBeRemoved = "00.00-" + decimal.MaxValue;
                catalogQueryParametersModel.PriceRanges.Remove(_valueToBeRemoved);
            }
            var filteredProducts = SortProducts(_allProducts, catalogQueryParametersModel.SortBy);
            var sortedProducts = FilterProductsByCategoryAndPriceRange(filteredProducts, catalogQueryParametersModel.Category, catalogQueryParametersModel.PriceRanges);
            var startIndex = (catalogQueryParametersModel.Page - 1) * catalogQueryParametersModel.PageSize;
            var pagedProducts = sortedProducts.Skip(startIndex).Take(catalogQueryParametersModel.PageSize).ToList();

            return Task.FromResult(pagedProducts);
        }

        public Task<ProductModel> GetProductById(int ProductId)
        {
            var product = _allProducts.FirstOrDefault(p => p.Id == ProductId) ?? new ProductModel();
            return Task.FromResult(product);
        }

        public async Task<int> GetTotalPages(CatalogQueryParametersModel catalogQueryParametersModel)
        {
            var filteredProducts = SortProducts(_allProducts, catalogQueryParametersModel.SortBy);
            var sortedProducts = FilterProductsByCategoryAndPriceRange(filteredProducts, catalogQueryParametersModel.Category, catalogQueryParametersModel.PriceRanges);

            int totalProducts = sortedProducts.Count;
            int totalPages = (int)Math.Ceiling((double)totalProducts / catalogQueryParametersModel.PageSize);

            return await Task.FromResult(totalPages);
        }

        private List<ProductModel> FilterProductsByCategoryAndPriceRange(List<ProductModel> productsModel, string categories, List<string> priceRanges)
        {
            if (priceRanges != null && !priceRanges.Contains("All Price") && priceRanges.Any())
            {
                var filteredProducts = new List<ProductModel>();
                foreach (var priceRange in priceRanges)
                {
                    string[] rangeParts = priceRange.Split('-');
                    if (decimal.TryParse(rangeParts[0], out decimal minPrice) && decimal.TryParse(rangeParts[1], out decimal maxPrice))
                    {
                        filteredProducts.AddRange(productsModel.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList());
                    }
                }
                productsModel = filteredProducts;
            }

            if (categories != null && !categories.Contains("All Rooms"))
            {
                productsModel = productsModel.Where(p => categories.Contains(p.Category)).ToList();
            }
            return productsModel;
        }

        private List<ProductModel> SortProducts(List<ProductModel> productsModel, string sortBy)
        {
            switch (sortBy)
            {
                case "PriceLowToHigh":
                    return productsModel.OrderBy(p => p.Price).ToList();
                case "PriceHighToLow":
                    return productsModel.OrderByDescending(p => p.Price).ToList();
                case "NameAZ":
                    return productsModel.OrderBy(p => p.Title).ToList();
                case "NameZA":
                    return productsModel.OrderByDescending(p => p.Title).ToList();
                default:
                    return productsModel.OrderBy(p => p.Price).ToList();
            }
        }
    }
}