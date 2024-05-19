using BookUniverseProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BookUniverse.Application.DTOs.BookDTOs;
using BookUniverse.Application.MediatR.Books.Queries.GetAllBooks;
using BookUniverse.Application.MediatR.Books.Queries.GetBook;
using BookUniverse.Application.MediatR.Books.Queries.GetAllBooksByCategory;

namespace BookUniverseProject.Controllers
{
    public class BookController : BaseController
    {
        public async Task<IActionResult> MainPage()
        {
            var books = await GetAllBooks();
            ViewBag.Books = books;
            return View();
        }

        public async Task<IActionResult> FilterByCategory(int categoryId)
        {
            var filteredBooks = await GetAllBooksByCategory(categoryId);
            ViewBag.Books = filteredBooks;
            return View("HomePage");
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
        
        public IActionResult UserPage()
        {
            return View();
        }

        public async Task<IActionResult> BookPage(int id)
        {
            BookDto book = await GetBook(id);
            ViewBag.Book = book;
            return View();
        }
        
        public async Task<IActionResult> ReadBookPage(int id)
        {
            BookDto book = await GetBook(id);
            ViewBag.BookLink = book.Path + "/preview";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
