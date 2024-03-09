using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BookUniverse.Domain.Common;

namespace BookUniverse.Domain.Entities
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MinLength(CategoryValidationConstants.CATEGORYNAME_MIN_LENGTH)]
        [MaxLength(CategoryValidationConstants.CATEGORYNAME_MAX_LENGTH)]
        public string CategoryName { get; set; }
    }
}
