using BookUniverse.Application.DTOs.CategoryDTOs;
using BookUniverse.Application.MediatR.Categories.Queries.GetAllCategories;
using BookUniverse.Application.MediatR.ResultVariations;
using BookUniverse.Web.Models;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookUniverseProject.Controllers
{
    public class BaseController : Controller
    {
        private IMediator? _mediator;

        public BaseController()
        {
        }

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;

        protected async Task InitializeLayoutModelAsync()
        {
            var model = new LayoutModel
            {
                Categories = await GetAllCategories()
            };
            ViewBag.LayoutModel = model;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await InitializeLayoutModelAsync();
            await base.OnActionExecutionAsync(context, next);
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategories()
        {
            ActionResult<IEnumerable<CategoryDto>> allCategoriesResult = HandleResult(await Mediator.Send(new GetAllCategoriesQuery()));
            if (allCategoriesResult.Result is OkObjectResult okObjectResult)
            {
                return (IEnumerable<CategoryDto>)okObjectResult.Value;
            }
            return Enumerable.Empty<CategoryDto>();
        }

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
