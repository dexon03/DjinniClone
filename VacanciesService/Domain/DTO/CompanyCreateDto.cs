namespace VacanciesService.Domain.DTO;

public class CompanyCreateDto
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}