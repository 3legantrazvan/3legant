using _3legant.Server.Interfaces;
using _3legant.Shared.Models;
using _3legant.Shared.Utils;
using Newtonsoft.Json;

namespace _3legant.Server.Services
{
    public class ShopService : IShopService
    {

        private readonly string _jsonFilePath = Path.Combine("Sample-Data", "Products.json");
        private IList<ProductModel> _allProducts;
        public ShopService()
        {
            _allProducts = JsonConvert.DeserializeObject<IList<ProductModel>>(File.ReadAllText(_jsonFilePath)) ?? new List<ProductModel>();
        }

        public Task<IList<ProductModel>> GetProducts(CatalogQueryParametersModel catalogQueryParametersModel)
        {
            var _lowestValue = 00.00;
            if (catalogQueryParametersModel.PriceRanges.Count > 1)
            {
                var _valueToBeRemoved = _lowestValue + "-" + decimal.MaxValue;
                catalogQueryParametersModel.PriceRanges.Remove(_valueToBeRemoved);
            }
            var filteredProducts = SortProducts(_allProducts, catalogQueryParametersModel.SortBy);
            var sortedProducts = FilterProductsByCategoryAndPriceRange(filteredProducts, catalogQueryParametersModel.Category, catalogQueryParametersModel.PriceRanges);
            var startIndex = (catalogQueryParametersModel.Page - 1) * catalogQueryParametersModel.PageSize;
            var pagedProducts = sortedProducts.Skip(startIndex).Take(catalogQueryParametersModel.PageSize).ToList();

            IList<ProductModel> result = pagedProducts;
            return Task.FromResult(result);
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

        private IList<ProductModel> FilterProductsByCategoryAndPriceRange(IList<ProductModel> productsModel, string categories, IList<string> priceRanges)
        {
            if (priceRanges != null && !priceRanges.Contains(CatalogConstants.AllPrice) && priceRanges.Any())
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

            if (categories != null && !categories.Contains(CatalogConstants.AllRooms))
            {
                productsModel = productsModel.Where(p => categories.Contains(p.Category)).ToList();
            }
            return productsModel;
        }

        private IList<ProductModel> SortProducts(IList<ProductModel> productsModel, string sortBy)
        {
            switch (sortBy)
            {
                case CatalogSortConstants.PriceLowToHigh:
                    return productsModel.OrderBy(p => p.Price).ToList();
                case CatalogSortConstants.PriceHighToLow:
                    return productsModel.OrderByDescending(p => p.Price).ToList();
                case CatalogSortConstants.NameAZ:
                    return productsModel.OrderBy(p => p.Title).ToList();
                case CatalogSortConstants.NameZA:
                    return productsModel.OrderByDescending(p => p.Title).ToList();
                default:
                    return productsModel.OrderBy(p => p.Price).ToList();
            }
        }
    }
}