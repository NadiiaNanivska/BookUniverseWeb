using Google.Apis.Books.v1;
using Google.Apis.Books.v1.Data;
using Google.Apis.Services;

namespace BookUniverse.Web.Views
{
    public class SearchBook : ISearchBook
    {
        private readonly BooksService _booksService;

        public SearchBook()
        {
            _booksService = new BooksService(new BaseClientService.Initializer
            {
                ApplicationName = "Book Universe",
            });
        }

        public async Task<Volumes> SearchAsync(string query)
        {
            var listRequest = _booksService.Volumes.List(query);
            return await listRequest.ExecuteAsync();
        }
    }
}
