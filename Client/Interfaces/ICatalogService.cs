using _3legant.Server.Models;
using _3legant.Shared.Models;

namespace Interfaces
{
    public interface ICatalogService
    {
        public Task<ProductsResponseModel> GetProducts(CatalogQueryParametersModel catalogQueryParametersModel);
        public Task<ProductModel> GetProductById(int ProductId);
        public Task<IList<OptionsModel>> GetPriceRangeFilters();
        public Task<IList<string>> GetCategoryFilters();
        public Task<IList<SortOptionModel>> GetSortObtions();
    }
}