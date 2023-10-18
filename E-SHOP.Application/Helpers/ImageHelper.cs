namespace E_SHOP.Application.Helpers
{
	public static class ImageHelper
	{
		public static async Task SaveAsync(byte[] imgBytes, string savePath)
		{

			using (MemoryStream stream = new MemoryStream(imgBytes))
			{
				using (FileStream fs = new FileStream(savePath, FileMode.Create))
				{
					await stream.CopyToAsync(fs);
				}
			}
		}
	}
}
