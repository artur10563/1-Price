using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePrice.Domain.Enums
{
	public enum ImageStatus
	{
		Success = 0,
		ExtensionError = 1,
		SizeError = 2,
		OtherError = 3,
	}
}
