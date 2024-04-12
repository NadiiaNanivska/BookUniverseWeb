using BookUniverse.Application.MediatR.Categories.Commands.CreateCategory;
using BookUniverse.Application.MediatR.Categories.Queries.GetAllCategories;
using BookUniverseProject.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BookUniverse.Web.Controllers
{
    public class CategoryController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await Mediator.Send(new GetAllCategoriesQuery()));
        }

        [HttpPost]
        // only for admins - [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> Create([FromBody] string categoryName)
        {
            return HandleResult(await Mediator.Send(new CreateCategoryCommand(categoryName)));
        }
    }
}
