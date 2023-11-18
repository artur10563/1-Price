using OnePrice.UI.Models.CommonDTOs;
using X.PagedList;

namespace OnePrice.UI.Models.PostDTOs
{
	public class SearchPostDisplayViewModel
	{
		public SearchDTO Filters { get; set; }
		public IPagedList<PostDisplayDTO> Posts { get; set; }
	}
}
