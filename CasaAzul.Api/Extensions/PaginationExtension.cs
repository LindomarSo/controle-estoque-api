using System.Text.Json;

namespace CasaAzul.Api.Extensions
{
    public static class PaginationExtension
    {
        public static void AddPaginationHeader(this HttpResponse response,
                                               int pageNumber, 
                                               int totalItems,
                                               int totalPages,
                                               int itemsPerPage)
        {

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var items = JsonSerializer.Serialize(new { pageNumber = pageNumber, 
                                                            totalItems = totalItems, 
                                                            totalPages = totalPages, 
                                                            itemsPerPage = itemsPerPage }, options);

            response.Headers.Add("Pagination", items);

            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}
