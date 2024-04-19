using BookUniverseProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BookUniverse.Application.DTOs.BookDTOs;
using BookUniverse.Application.MediatR.Books.Queries.GetAllBooks;
using BookUniverse.Application.MediatR.Books.Queries.GetBook;

namespace BookUniverseProject.Controllers
{
    public class HomeController : BaseController
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
            return View(books);
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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserPage()
        {
            return View("~/Views/Home/UserPage.cshtml");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [Route("Home/BookPage/{id}")]
        public async Task<IActionResult> BookPage(int id)
        {
            BookDto book = await GetBook(id);
            ViewBag.Book = book;
            return View(book);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
