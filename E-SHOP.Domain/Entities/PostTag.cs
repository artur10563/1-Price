using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_SHOP.Domain.Entities
{
	public class PostTag
	{
		public int Id { get; set; }
		public int IdPost { get; set; }
		public int IdTag { get; set; }

		public virtual Post Post { get; set; }
		public virtual Tag Tag { get; set; }

	}
}
