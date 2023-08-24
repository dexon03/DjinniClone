using ProfilesService.Domain.DTO;
using ProfilesService.Domain.Models;

namespace ProfilesService.Domain.Contracts;

public interface ILocationService
{
    Task<List<Location>> GetAllLocations(CancellationToken cancellationToken = default);
    Task<Location> GetLocationById(Guid id, CancellationToken cancellationToken = default);
    Task<Location> CreateLocation(LocationCreateDto location, CancellationToken cancellationToken = default);
    Task<Location> UpdateLocation(LocationUpdateDto location, CancellationToken cancellationToken = default);
    Task DeleteLocation(Guid id, CancellationToken cancellationToken = default);
    Task DeleteManyLocations(Location[] locations, CancellationToken cancellationToken = default);
}