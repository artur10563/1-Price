using System.ComponentModel.DataAnnotations;

namespace OnePrice.UI.Models.CommonDTOs
{
    public class CommonTagDTO
    {
        [Required(ErrorMessage = "Tag name is required")]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
    }
}
