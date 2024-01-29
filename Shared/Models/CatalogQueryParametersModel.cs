namespace _3legant.Shared.Models
{
    public class CatalogQueryParametersModel
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public string Category { get; set; }
        public List<string> PriceRanges { get; set; }
    }
}
