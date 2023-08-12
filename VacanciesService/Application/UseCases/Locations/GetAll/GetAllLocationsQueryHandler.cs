using Core.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Locations.GetAll;

public class GetAllLocationsQueryHandler : IRequestHandler<GetAllLocationsQuery,List<Location>>
{
    private readonly IRepository _repository;

    public GetAllLocationsQueryHandler(IRepository repository)
    {
        _repository = repository;
    }
    public Task<List<Location>> Handle(GetAllLocationsQuery request, CancellationToken cancellationToken)
    {
        return _repository.GetAll<Location>().ToListAsync(cancellationToken);
    }
}