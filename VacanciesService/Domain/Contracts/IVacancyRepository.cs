using VacanciesService.Domain.Models;

namespace VacanciesService.Domain.Contracts;

public interface IVacancyRepository
{
    public Task<Vacancy?> Get(Guid id);
    public Task<IEnumerable<Vacancy>> GetAll();
    public Task<Vacancy> Create(Vacancy vacancy);
    public Task<Vacancy> Update(Vacancy vacancy);
    public Task Delete(Vacancy vacancy);
}