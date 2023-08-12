using Core.Database;
using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Vacancies.ActivateDeactivate;

public class ActivateDeactivateVacancyCommandHandler : IRequestHandler<ActivateDeactivateVacancyCommand>
{
    private readonly IRepository _repository;

    public ActivateDeactivateVacancyCommandHandler(IRepository repository)
    {
        _repository = repository;
    }
    public async Task Handle(ActivateDeactivateVacancyCommand request, CancellationToken cancellationToken)
    {
        var vacancy = await _repository.GetByIdAsync<Vacancy>(request.Id);
        if (vacancy == null)
        {
            throw new Exception("Vacancy not found");
        }
        vacancy.IsActive = !vacancy.IsActive;
        _repository.Update(vacancy);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}