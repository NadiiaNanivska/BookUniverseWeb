using BookUniverseProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BookUniverse.Application.DTOs.BookDTOs;
using BookUniverse.Application.MediatR.Books.Queries.GetAllBooks;
using BookUniverse.Application.MediatR.Books.Queries.GetBook;
using BookUniverse.Application.DTOs.CategoryDTOs;
using BookUniverse.Application.MediatR.Categories.Queries.GetAllCategories;

namespace BookUniverseProject.Controllers
{
    public class BookController : BaseController
    {
        public async Task<IActionResult> MainPage()
        {
            var books = await GetAllBooks();
            ViewBag.Books = books;
            return View(books);
        }
        
        public async Task<IActionResult> HomePage()
        {
            var books = await GetAllBooks();
            ViewBag.Books = books;
            return View();
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
