using System.ComponentModel.DataAnnotations;

namespace OnePrice.UI.Models.CommentDTOs
{
    public class CommentAppUserDTO
    {
        [Required(ErrorMessage = "Nickname is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Nickname must be between 3 and 50 characters")]
        public string Nickname { get; set; }
        public string? ImgPath { get; set; }
    }
}
