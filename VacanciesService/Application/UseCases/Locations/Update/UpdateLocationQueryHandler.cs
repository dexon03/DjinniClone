using Core.Database;
using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Locations.Update;

public class UpdateLocationQueryHandler : IRequestHandler<UpdateLocationQuery,Location>
{
    private readonly IRepository _repository;

    public UpdateLocationQueryHandler(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<Location> Handle(UpdateLocationQuery request, CancellationToken cancellationToken)
    {
        var result = _repository.Update(request.location);
        await _repository.SaveChangesAsync(cancellationToken);
        return result;
    }
}