namespace ProfilesService.Domain.Contracts;

public interface IPdfService
{
    Task UploadPdf(IFormFile file, Guid profileId, CancellationToken cancellationToken = default);
    Task<bool> CheckIfPdfExistsAndEqual(Guid profileId, IFormFile? formFile = null, CancellationToken cancellationToken = default);
    Task DeletePdf(Guid profileId, CancellationToken cancellationToken = default);
    Task<byte[]> DownloadPdf(Guid profileId, CancellationToken cancellationToken = default);
}