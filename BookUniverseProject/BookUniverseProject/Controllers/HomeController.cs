using BookUniverseProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BookUniverse.Application.DTOs.BookDTOs;
using BookUniverse.Application.MediatR.Books.Queries.GetAllBooks;

namespace BookUniverseProject.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
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
            ActionResult<IEnumerable<BookDto>> res = HandleResult(await Mediator.Send(new GetAllBooksQuery()));
            if (res.Result is OkObjectResult okObjectResult)
            {
                return (IEnumerable<BookDto>)okObjectResult.Value;
            }
            return Enumerable.Empty<BookDto>();
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}