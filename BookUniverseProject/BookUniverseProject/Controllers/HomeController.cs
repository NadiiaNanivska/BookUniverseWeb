using BookUniverseProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BookUniverse.Application.DTOs.BookDTOs;
using BookUniverse.Application.MediatR.Books.Queries.GetAllBooks;
using BookUniverse.Infrastructure.Repositories.Base.UnitOfWork;
using MediatR;

namespace BookUniverseProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;


        public HomeController(ILogger<HomeController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
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
            var result = await _mediator.Send(new GetAllBooksQuery());
            if (result.IsSuccess)
            {
                return result.Value;
            }
            else
            {
                _logger.LogError("Failed to fetch books: {Error}", result.Errors);
                return Enumerable.Empty<BookDto>();
            }
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}