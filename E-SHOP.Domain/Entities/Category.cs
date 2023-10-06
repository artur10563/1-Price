using E_SHOP.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_SHOP.Domain.Entities
{
	public class Category : BaseEntity
	{

		//[Required(ErrorMessage ="Enter category name")]
		//[StringLength(50, MinimumLength = 3)]
		public string Name { get; set; }

		public string? ImgPath { get; set; }

		List<Post> Posts { get; set; }
	}
}
