using AutoMapper;
using BookUniverse.Application.MediatR.SearchBooks.Queries.GetAllBooksByText;
using BookUniverse.Domain.Common;
using BookUniverse.Infrastructure.Services.SearchBook;
using FluentResults;
using Google.Apis.Books.v1.Data;
using MediatR;

namespace BookUniverse.Application.MediatR.SearchBooks.Queries.GetAllBooksByQuery
{
    public class GetAllBooksByTextHandler : IRequestHandler<GetAllBooksByTextQuery, Result<IEnumerable<string>>>
    {
        private readonly IMapper _mapper;
        private readonly ISearchBook _searchBook;

        public GetAllBooksByTextHandler(IMapper mapper, ISearchBook searchBook)
        {
            _mapper = mapper;
            _searchBook = searchBook;
        }

        public async Task<Result<IEnumerable<string>>> Handle(GetAllBooksByTextQuery request, CancellationToken cancellationToken)
        {
            Volumes filteredBooks = await _searchBook.SearchAsync(request.queryString);

            if (filteredBooks.Items is null)
            {
                return Result.Fail(new Error(ResponseMessagesConstants.NOTHING_FOUND));
            }

            var booksInfo = new List<string>();

            foreach (var volume in filteredBooks.Items)
            {
                string authors = string.Join(", ", volume.VolumeInfo.Authors ?? Array.Empty<string>());
                string bookInfo = $"{volume.VolumeInfo.Title} by {authors}";
                booksInfo.Add(bookInfo);
            }

            return Result.Ok(booksInfo.AsEnumerable());
        }
    }
}
