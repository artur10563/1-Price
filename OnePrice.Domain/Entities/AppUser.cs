using Microsoft.AspNetCore.Identity;
using OnePrice.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePrice.Domain.Entities
{
	public class AppUser : IdentityUser<int>
	{
		public DateTime CreatedAt { get; set; }
		public DateTime? LastModifiedAt { get; set; }

		public string Nickname { get; set; }
		public string? ImgPath { get; set; }

		public ICollection<Post>? Posts { get; set; }
		public ICollection<Comment>? Comments { get; set; }

	}
}

