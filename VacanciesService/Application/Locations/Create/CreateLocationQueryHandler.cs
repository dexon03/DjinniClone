using Core.Database;
using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Locations.Create;

public class CreateLocationQueryHandler : IRequestHandler<CreateLocationQuery,Location>
{
    private readonly IRepository _repository;

    public CreateLocationQueryHandler(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<Location> Handle(CreateLocationQuery request, CancellationToken cancellationToken)
    {
        var location  = await _repository.CreateAsync(request.location);
        await _repository.SaveChangesAsync(cancellationToken);
        return location;
    }
}