using System.Runtime.InteropServices;

namespace Student_Dashboard_Api.Services
{
    public class FileService
    {
        private readonly string _filesDirectory;

        public FileService()
        {
            _filesDirectory = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                                                ? Path.Combine(Directory.GetCurrentDirectory(), "Data", "Files")
                                                : Path.Combine("/app");

            Directory.CreateDirectory(_filesDirectory);
        }

        public Task RemoveFileAsync(string? fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return Task.CompletedTask;
            }
            try
            {
                string filePath = Path.Combine(_filesDirectory, fileName);
                string fullPath = Path.GetFullPath(filePath);

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing file: {ex.Message}");
                throw;
            }
        }

        public async Task<string?> SaveFileAsync(IFormFile? file)
        {
            if (file == null)
            {
                return null;
            }

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(_filesDirectory, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
        }

        public Stream? GetFileByFileName(string fileName)
        {
            try
            {
                string filePath = Path.Combine(_filesDirectory, fileName);
                string fullPath = Path.GetFullPath(filePath);

                if (File.Exists(fullPath))
                {
                    return new FileStream(fullPath, FileMode.Open, FileAccess.Read);
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving file: {ex.Message}");
                throw;
            }
        }

        public string GetFilePath(string fileName)
        {
            try
            {
                string filePath = Path.Combine(_filesDirectory, fileName);
                string fullPath = Path.GetFullPath(filePath);
                return fullPath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving file: {ex.Message}");
                throw;
            }
        }
    }
}
