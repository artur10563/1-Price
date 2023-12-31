﻿using System.ComponentModel.DataAnnotations;

namespace OnePrice.UI.Models.CommonIdDTOs
{

    public class CommonIdCategoryDTO
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
    }
}
