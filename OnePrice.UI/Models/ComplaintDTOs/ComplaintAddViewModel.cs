namespace OnePrice.UI.Models.ComplaintDTOs
{
	public class ComplaintAddViewModel
	{
		public ComplaintAddDTO Complaint { get; set; }
		public IEnumerable<ComplaintTypeDTO> AvailableTypes { get; set; }
	}
}
