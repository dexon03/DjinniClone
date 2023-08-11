using MediatR;
using VacanciesService.Domain.Contracts;

namespace VacanciesService.Application.UseCases.Vacancies.Delete;

public class DeleteVacancyCommandHandler : IRequestHandler<DeleteVacancyCommand>
{
    private readonly IVacancyRepository _repository;

    public DeleteVacancyCommandHandler(IVacancyRepository repository)
    {
        _repository = repository;
    }
    public async Task Handle(DeleteVacancyCommand request, CancellationToken cancellationToken)
    {
        var vacancy = await _repository.Get(request.Id);
        if (vacancy == null)
        {
            throw new Exception("Vacancy not found");
        }
        
        await _repository.Delete(vacancy);
    }
}