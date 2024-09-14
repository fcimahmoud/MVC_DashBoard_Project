namespace DemoPresentationLayer.Utilities
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            // Create FolderPath
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files" ,folderName);

            // Create Unique Name For File
            string fileName = $"{Guid.NewGuid}-{file.FileName}";

            // Create FilePath
            string filePath = Path.Combine(folderPath, fileName);

            // Create FileStream to save File
            using var stream = new FileStream(filePath, FileMode.Create);

            // Copy File to FileStream
            file.CopyTo(stream);

            return fileName;
        }

        public static void DeleteFile(string folderName, string fileName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files", folderName, fileName);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}
