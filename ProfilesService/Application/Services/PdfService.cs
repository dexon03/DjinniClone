using ProfilesService.Domain.Contracts;

namespace ProfilesService.Application.Services;

public class PdfService : IPdfService
{
    private readonly string fileStoragePath = Path.Combine(Directory.GetCurrentDirectory(), "PdfResumesFiles");
    
    public async Task UploadPdf(IFormFile file, Guid profileId, CancellationToken cancellationToken = default)
    {
        var filePath = Path.Combine(fileStoragePath, profileId.ToString());
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }
        await using var fileStream = new FileStream(Path.Combine(filePath, file.FileName), FileMode.Create);
        await file.CopyToAsync(fileStream, cancellationToken);
    }

    public async Task<bool> CheckIfPdfExistsAndEqual(Guid profileId, IFormFile? formFile = null, CancellationToken cancellationToken = default)
    {
        var filePath = Path.Combine(fileStoragePath, profileId.ToString());
        if (!Directory.Exists(filePath))
        {
            return false;
        }
        if (formFile != null)
        {
            filePath = Path.Combine(filePath, formFile.FileName);
            if (File.Exists(filePath) 
                && (await File.ReadAllBytesAsync(filePath, cancellationToken)).Length == formFile.Length)
            {
                return true;
            }
        }
        return false;
    }

    public Task DeletePdf(Guid profileId, CancellationToken cancellationToken = default)
    {
        var filePath = Path.Combine(fileStoragePath, profileId.ToString());
        if (Directory.Exists(filePath))
        {
            Directory.Delete(filePath, true);
        }
        return Task.CompletedTask;
    }

    public Task<byte[]> DownloadPdf(Guid profileId, CancellationToken cancellationToken = default)
    {
        var filePath = Path.Combine(fileStoragePath, profileId.ToString());
        if (!Directory.Exists(filePath))
        {
            throw new FileNotFoundException("File not found");
        }
        var file = Directory.GetFiles(filePath).FirstOrDefault();
        if (file == null)
        {
            throw new FileNotFoundException();
        }
        return File.ReadAllBytesAsync(file, cancellationToken);
    }
}