using _3legant.Shared.Models;

namespace _3legant.Server.Models
{
    public class ProductsResponseModel
    {
        public IList<ProductModel> Products { get; set; }
        public PaginationInfoModel PaginationInfo { get; set; }
    }
}
