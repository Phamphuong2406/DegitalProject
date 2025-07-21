using System;
using System.IO;

namespace DigitalProject.Common.UploadFile
{
    public static class UploadHandler
    {
        public static string Upload(IFormFile file)
        {
            List<string> validExtentions = new List<string>() { ".jpg",".png",".gif"};
           string extention = Path.GetExtension(file.FileName);
            if (!validExtentions.Contains(extention)) {
                return $"extention is not vslid({string.Join('.', validExtentions)})";
            }
            long size = file.Length;
            if (size > (5 * 1024 * 1024))
                return "Maximum size can be 5mb";
            string fileName = Guid.NewGuid().ToString() + extention;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            using FileStream stream = new FileStream(Path.Combine(path,fileName), FileMode.Create);
            file.CopyTo(stream);
            return fileName;
        }
        public static string DeleteFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return "Filename is required";

            string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", fileName);

            if (!System.IO.File.Exists(path))
                return "File does not exist";

            try
            {
                System.IO.File.Delete(path);
                return "File deleted successfully";
            }
            catch (Exception ex)
            {
                return $"Error deleting file: {ex.Message}";
            }
        }

    }
}
