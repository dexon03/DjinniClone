namespace VacanciesService.Domain.DTO;

public record GenerateVacancyRequest
{
    public string? Description { get; set; }
    public string? Title { get; set; }
    public string? СompanyDescription { get; set; }
    public string[] Skills { get; set; }
};