using BookUniverseProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BookUniverse.Application.DTOs.BookDTOs;
using BookUniverse.Application.MediatR.Books.Queries.GetAllBooks;
using BookUniverse.Application.MediatR.Books.Queries.GetBook;
using BookUniverse.Application.MediatR.Books.Queries.GetAllBooksByCategory;
using BookUniverse.Application.MediatR.Books.Queries.GetAllBooksByUser;
using System.Security.Claims;
using BookUniverse.Web.Models;
using BookUniverse.Application.MediatR.SearchBooks.Queries.GetAllBooksByText;

namespace BookUniverseProject.Controllers
{
    public class BookController : BaseController
    {
        public IActionResult SearchBookPage()
        {
            ViewBag.Books = Enumerable.Empty<string>();
            ViewBag.Count = 0;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FindBooks(BookSearchModel model)
        {
            string searchQuery = $"\"{model.SearchQuery}\"";
            if (ModelState.IsValid)
            {
                var foundBooks = await GetAllBooksByText(searchQuery);
                ViewBag.Books = foundBooks;
                ViewBag.Count = foundBooks.Count();
                return View("SearchBookPage");
            }

            return RedirectToAction("SearchBookPage");
        }

        public async Task<IActionResult> MainPage()
        {
            var books = await GetAllBooks();
            ViewBag.Books = books;
            return View(books);
        }
        
        public async Task<IActionResult> HomePage()
        {
            var userClaims = HttpContext.User.Claims;
            var userIdClaim = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim != null)
            {
                var books = await GetAllBooksByUser(userIdClaim.Value);
                ViewBag.Books = books;
                return View();
            }
            return View();
        }

        public async Task<IActionResult> FilterByCategory(int categoryId)
        {
            var filteredBooks = await GetAllBooksByCategory(categoryId);
            ViewBag.Books = filteredBooks;
            return View("HomePage");
        }

        private async Task<IEnumerable<string>> GetAllBooksByText(string queryString)
        {
            ActionResult<IEnumerable<string>> allBooksResult = HandleResult(await Mediator.Send(new GetAllBooksByTextQuery(queryString)));
            if (allBooksResult.Result is OkObjectResult okObjectResult)
            {
                return (IEnumerable<string>)okObjectResult.Value;
            }
            return Enumerable.Empty<string>();
        }

        private async Task<IEnumerable<BookDto>> GetAllBooksByCategory(int categoryId)
        {
            ActionResult<IEnumerable<BookDto>> allBooksResult = HandleResult(await Mediator.Send(new GetAllBooksByCategoryQuery(categoryId)));
            if (allBooksResult.Result is OkObjectResult okObjectResult)
            {
                return (IEnumerable<BookDto>)okObjectResult.Value;
            }
            return Enumerable.Empty<BookDto>();
        }


        private async Task<IEnumerable<BookDto>> GetAllBooks()
        {
            ActionResult<IEnumerable<BookDto>> allBooksResult = HandleResult(await Mediator.Send(new GetAllBooksQuery()));
            if (allBooksResult.Result is OkObjectResult okObjectResult)
            {
                return (IEnumerable<BookDto>)okObjectResult.Value;
            }
            return Enumerable.Empty<BookDto>();
        }

        private async Task<IEnumerable<BookDto>> GetAllBooksByUser(string userId)
        {
            ActionResult<IEnumerable<BookDto>> allUserBooksResult = HandleResult(await Mediator.Send(new GetAllBooksByUserQuery(userId)));
            if (allUserBooksResult.Result is OkObjectResult okObjectResult)
            {
                return (IEnumerable<BookDto>)okObjectResult.Value;
            }
            return Enumerable.Empty<BookDto>();
        }

        private async Task<BookDto> GetBook(int id)
        {
            ActionResult<BookDto> book = HandleResult(await Mediator.Send(new GetBookQuery(id)));
            if (book.Result is OkObjectResult okObjectResult)
            {
                BookDto res = (BookDto)okObjectResult.Value;
                return res;
            }
            return null;
        }
        
        public async Task<IActionResult> BookPage(int id)
        {
            BookDto book = await GetBook(id);
            ViewBag.Book = book;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
