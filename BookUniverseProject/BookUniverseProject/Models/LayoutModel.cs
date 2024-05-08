using BookUniverse.Application.DTOs.CategoryDTOs;

namespace BookUniverse.Web.Models
{
    public class LayoutModel
    {
        public IEnumerable<CategoryDto> Categories { get; set; }
    }
}
