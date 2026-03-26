namespace wepAPI_denemeler.DTOs
{
    public class QueryParams
    {
        // Pagination
        public bool IsPaginationEnabled { get; set; } = true;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        // Sorting
        public string? SortField { get; set; } // Örn: "Username" veya "Title"

        // Filtering
        public string? FilterField { get; set; } // Örn: "Username"
        public string? Keyword { get; set; } // Örn: "Toprak"
    }
}