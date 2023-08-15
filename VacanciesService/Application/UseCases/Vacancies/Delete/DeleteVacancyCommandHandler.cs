using Core.Database;
using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Vacancies.Delete;

public class DeleteVacancyCommandHandler : IRequestHandler<DeleteVacancyCommand>
{
    private readonly IRepository _repository;

    public DeleteVacancyCommandHandler(IRepository repository)
    {
        _repository = repository;
    }
    public async Task Handle(DeleteVacancyCommand request, CancellationToken cancellationToken)
    {
        var vacancy = await _repository.GetByIdAsync<Vacancy>(request.Id);
        if (vacancy == null)
        {
            throw new Exception("Vacancy not found");
        }
        
        _repository.Delete(vacancy);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}