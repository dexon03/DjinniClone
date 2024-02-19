namespace ProfilesService.Domain.Contracts;

public interface IPdfService
{
    Task UploadPdf(IFormFile formFile, Guid profileId, CancellationToken cancellationToken = default);
    bool CheckIfResumeFolderInitialised(Guid profileId);
    Task<bool> CheckIfPdfExistsAndEqual(Guid profileId, IFormFile? formFile = null, CancellationToken cancellationToken = default);
    Task DeletePdf(Guid profileId);
    Task<byte[]> DownloadPdf(Guid profileId, CancellationToken cancellationToken = default);
}