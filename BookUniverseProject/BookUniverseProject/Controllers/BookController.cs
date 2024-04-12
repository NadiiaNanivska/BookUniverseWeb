using BookUniverse.Application.DTOs.BookDTOs;
using BookUniverse.Application.MediatR.Books.Commands.CreateBook;
using BookUniverse.Application.MediatR.Books.Queries.GetAllBooks;
using BookUniverse.Application.MediatR.Books.Queries.GetBook;
using BookUniverseProject.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BookUniverse.Web.Controllers
{
    public class BookController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await Mediator.Send(new GetAllBooksQuery()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new GetBookQuery(id)));
        }

        [HttpPost]
        // only for admins - [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> Create([FromBody] AddBookDto book)
        {
            return HandleResult(await Mediator.Send(new CreateBookCommand(book)));
        }
    }
}
