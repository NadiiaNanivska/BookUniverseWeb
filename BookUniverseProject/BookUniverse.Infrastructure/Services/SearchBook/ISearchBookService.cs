namespace BookUniverse.Infrastructure.Services.SearchBook
{
    using Google.Apis.Books.v1.Data;

    public interface ISearchBook
    {
        public Task<Volumes> SearchAsync(string query);
    }
}
