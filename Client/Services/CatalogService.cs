using _3legant.Server.Models;
using _3legant.Shared.Models;
using Interfaces;
using System.Net.Http.Json;

namespace _3legant.Client.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;
        private readonly string _catalogBaseRoute = "Catalog";
        private readonly string _productBaseRoute = "Product";
        public CatalogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductsResponseModel> GetProducts(CatalogQueryParametersModel catalogQueryParametersModel)
        {
            var response = await _httpClient.GetFromJsonAsync<ProductsResponseModel>($"{_catalogBaseRoute}/CatalogProducts" + "?" + BuildQueryString(catalogQueryParametersModel)) ?? new ProductsResponseModel();
            return response;
        }

        public async Task<ProductModel> GetProductById(int ProductId)
        {
            var response = await _httpClient.GetFromJsonAsync<ProductModel>($"{_productBaseRoute}/IndividualProduct?ProductId={ProductId}") ?? new ProductModel();
            return response;
        }

        public async Task<IList<OptionsModel>> GetPriceRangeFilters()
        {
            var response = await _httpClient.GetFromJsonAsync<IList<OptionsModel>>($"{_catalogBaseRoute}/PriceRangeFilter") ?? new List<OptionsModel>();
            return response;
        }

        public async Task<IList<string>> GetCategoryFilters()
        {
            var response = await _httpClient.GetFromJsonAsync<IList<string>>($"{_catalogBaseRoute}/CategoriesFilter") ?? new List<string>();
            return response;
        }

        public async Task<IList<SortOptionModel>> GetSortObtions()
        {
            var response = await _httpClient.GetFromJsonAsync<IList<SortOptionModel>>($"{_catalogBaseRoute}/SortOptions") ?? new List<SortOptionModel>();
            return response;
        }

        private string BuildQueryString(CatalogQueryParametersModel catalogQueryParametersModel)
        {
            var parameters = new List<string>
                {
                    $"Page={catalogQueryParametersModel.Page}",
                    $"PageSize={catalogQueryParametersModel.PageSize}",
                    $"SortBy={Uri.EscapeDataString(catalogQueryParametersModel.SortBy)}",
                    $"Category={Uri.EscapeDataString(catalogQueryParametersModel.Category)}"
                };

            parameters.AddRange(catalogQueryParametersModel.PriceRanges.Select(range => $"PriceRanges={Uri.EscapeDataString(range)}"));
            string queryString = string.Join("&", parameters);
            return queryString;
        }
    }
}