namespace Recipe.Api.Helpers;

public class FileHelper
{
    public static async Task<string> SaveFileAsync(IFormFile file, string uploadFolderPath, string? customFileName = null)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("No file provided.", nameof(file));
        if (!Directory.Exists(uploadFolderPath))
            Directory.CreateDirectory(uploadFolderPath);
        var fileName = customFileName ?? $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(uploadFolderPath, fileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        return filePath;
    }
    
    public static Task DeleteFileAsync(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
            throw new ArgumentException("Invalid file path.", nameof(filePath));
        
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        return Task.CompletedTask;
    }
}
