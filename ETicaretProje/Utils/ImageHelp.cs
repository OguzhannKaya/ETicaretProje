namespace ETicaretProje.Utils
{
    public class ImageHelp
    {
        public static async Task<string> ImageLoaderAsync(IFormFile Image,string filepath="/Img/")
        {
            var filename = "";
            if(Image != null && Image.Length > 0)
            {
                filename =Image.FileName;
                string directory = Directory.GetCurrentDirectory() + "/wwwroot" + filepath + filename;
                using var stream = new FileStream(directory, FileMode.Create);
                await Image.CopyToAsync(stream);
            }
            return filename;
        }
    }
}
