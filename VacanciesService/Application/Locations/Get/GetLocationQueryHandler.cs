using Core.Database;
using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Locations.Get;

public class GetLocationQueryHandler : IRequestHandler<GetLocationQuery,Location>
{
    private readonly IRepository _repository;

    public GetLocationQueryHandler(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<Location> Handle(GetLocationQuery request, CancellationToken cancellationToken)
    {
        var location = await _repository.GetByIdAsync<Location>(request.Id);
        if (location == null)
        {
            throw new Exception("Location not found");
        }
        
        return location;
    }
}