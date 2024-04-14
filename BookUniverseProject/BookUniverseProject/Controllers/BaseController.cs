using BookUniverse.Application.MediatR.ResultVariations;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookUniverseProject.Controllers
{
    public class BaseController : Controller
    {
        private IMediator? _mediator;

        public BaseController()
        {
        }

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;

        protected ActionResult<T> HandleResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
            {
                if (result is NullResult<T>)
                {
                    return Ok(result.Value);
                }

                return (result.Value is null) ?
                    NotFound("Not Found") : Ok(result.Value);
            }

            return BadRequest(result.Reasons);
        }
    }
}
