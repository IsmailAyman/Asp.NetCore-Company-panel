using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Company.PL.Helpers
{
    public class DocumentSettings
    {
        public static string UploadImage(IFormFile file, string FolderName)
        {
            string FoldePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Files", FolderName);
            string fileName = $"{Guid.NewGuid()}{file.FileName}";
            string filePath = Path.Combine(FoldePath, fileName);
            var fs = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fs);
            return fileName;
        }

        public static void DeleteFile(string FileName, string FolderName)
        {
            if(FileName is not null && FolderName is not null)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName, FileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
           
        }
    }
}
