using OnePrice.Application.Helpers;
using OnePrice.Domain.Enums;

namespace OnePrice.UI.HelpersExtensions
{
	public static class IFormFileHelper
	{
		private static readonly string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
		private static readonly int maxSize = 5 * 1024 * 1024; //5 mb

		public static ImageStatus ValidateImage(IFormFile image)
		{
			if (image != null && image.Length > 0)
			{

				var fileExtension = Path.GetExtension(image.FileName).ToLower();

				if (!allowedExtensions.Contains(fileExtension))
					return ImageStatus.ExtensionError;

				//TODO: fix error with IIS where  error is being thrown if file size > 32mb 
				if (image.Length > maxSize)
					return ImageStatus.SizeError;

				return ImageStatus.Success;
			}
			return ImageStatus.OtherError;
		}

		/// <summary>
		/// Saves IFormFile as savePath/name.jpg
		/// </summary>
		/// <param name="image">IFormFile</param>
		/// <param name="name">File name without extension</param>
		/// <param name="savePath">Directory at which file is being saved</param>
		/// <returns>File name with extension</returns>
		public static async Task<string> SaveAsync(IFormFile image, string name, string savePath)
		{

			var fileName = name + ".jpg";
			var path = Path.Combine(savePath, fileName);


			byte[] byteImg = await IFormFileToBytesAsync(image);
			await ImageHelper.SaveAsync(byteImg, path);

			return fileName;
		}

		private static async Task<byte[]> IFormFileToBytesAsync(IFormFile formFile)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				await formFile.CopyToAsync(memoryStream);
				return memoryStream.ToArray();
			}
		}
	}
}
