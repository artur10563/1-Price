﻿using System.ComponentModel.DataAnnotations;

namespace OnePrice.UI.Models.CommonDTOs
{
    public class CommonCategoryDTO
    {
        [Required(ErrorMessage = "Name required")]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        public string? ImgPath { get; set; }
    }
}
