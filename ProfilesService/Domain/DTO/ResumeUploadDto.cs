namespace ProfilesService.Domain.DTO;

public record ResumeUploadDto
{
    public Guid CandidateId { get; set; }
    public IFormFile Resume { get; set; }
};