using _3legant.Server.Models;
using _3legant.Shared.Models;

namespace _3legant.Server.Interfaces
{
    public interface IShopService
    {
        public Task<IList<ProductModel>> GetProducts(CatalogQueryParametersModel catalogQueryParametersModel);
        public Task<int> GetTotalPages(CatalogQueryParametersModel catalogQueryParametersModel);
        public Task<ProductModel> GetProductById(int ProductId);
    }
}