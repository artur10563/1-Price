﻿using System.ComponentModel.DataAnnotations;

namespace OnePrice.UI.Models.CommonIdDTOs
{

    public class CommonIdTagDTO
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tag name is required")]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
    }
}
