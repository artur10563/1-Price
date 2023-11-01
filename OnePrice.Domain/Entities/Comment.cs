using OnePrice.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePrice.Domain.Entities
{
	public class Comment : BaseEntity
	{
		public string Content { get; set; }

		public int AuthorId { get; set; }
		public AppUser Author { get; set; }

		public int PostId { get; set; }
		public Post Post { get; set; }
	}
}
