using MediatR;
using VacanciesService.Domain.Contracts;

namespace VacanciesService.Application.UseCases.Vacancies.ActivateDeactivate;

public class ActivateDeactivateVacancyCommandHandler : IRequestHandler<ActivateDeactivateVacancyCommand>
{
    private readonly IVacancyRepository _repository;

    public ActivateDeactivateVacancyCommandHandler(IVacancyRepository repository)
    {
        _repository = repository;
    }
    public async Task Handle(ActivateDeactivateVacancyCommand request, CancellationToken cancellationToken)
    {
        var vacancy = await _repository.Get(request.Id);
        if (vacancy == null)
        {
            throw new Exception("Vacancy not found");
        }
        vacancy.IsActive = !vacancy.IsActive;
        await _repository.Update(vacancy);
    }
}