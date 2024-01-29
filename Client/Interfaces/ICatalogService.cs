using _3legant.Server.Models;
using _3legant.Shared.Models;

namespace Interfaces
{
    public interface ICatalogService
    {
        public Task<ProductsResponseModel> GetProducts(CatalogQueryParametersModel catalogQueryParametersModel);
        public Task<ProductModel> GetProductById(int ProductId);
        public Task<List<OptionsModel>> GetPriceRangeFilters();
        public Task<List<string>> GetCategoryFilters();
        public Task<List<SortOptionModel>> GetSortObtions();
    }
}