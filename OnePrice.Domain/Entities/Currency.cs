using OnePrice.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePrice.Domain.Entities
{
	public class Currency : BaseEntity
	{
		public string Code { get; set; }
		public string FullName { get; set; }
		public char Symbol { get; set; }

		public ICollection<Post> Posts { get; set; }

	}
}
