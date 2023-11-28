using AutoMapper;
using Core.Database;
using Microsoft.EntityFrameworkCore;
using VacanciesService.Domain.Contacts;
using VacanciesService.Domain.DTO;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.Services;

public class LocationService : ILocationService
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;

    public LocationService(IRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public Task<List<Location>> GetAllLocations(CancellationToken cancellationToken = default)
    {
        return _repository.GetAll<Location>().ToListAsync(cancellationToken);
    }

    public async Task<Location> GetLocationById(Guid id, CancellationToken cancellationToken = default)
    {
        var location = await _repository.GetByIdAsync<Location>(id);
        if (location == null)
        {
            throw new Exception($"Location not found");
        }
        
        return location;
    }

    public async Task<Location> CreateLocation(LocationCreateDto location, CancellationToken cancellationToken = default)
    {
        var locationEntity = _mapper.Map<Location>(location);
        var isExists = await _repository.AnyAsync<Location>(x => x.City == location.City && x.Country == location.Country);
        if (isExists)
        {
            throw new Exception("Location already exists");
        }
        var result = await _repository.CreateAsync(locationEntity);
        await _repository.SaveChangesAsync(cancellationToken);
        
        return result;
    }

    public async Task<Location> UpdateLocation(LocationUpdateDto location, CancellationToken cancellationToken = default)
    {
        var locationEntity = _mapper.Map<Location>(location);
        var isExists = await _repository.AnyAsync<Location>(x => x.Id == location.Id);
        if (!isExists)
        {
            throw new Exception($"Location not found");
        }
        
        var result = _repository.Update(locationEntity);
        await _repository.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task DeleteLocation(Guid id, CancellationToken cancellationToken = default)
    {
        var location = await _repository.GetByIdAsync<Location>(id);
        if (location == null)
        {
            throw new Exception($"Location not found");
        }
        
        _repository.Delete(location);
        await _repository.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteManyLocations(Location[] locations, CancellationToken cancellationToken = default)
    {
        _repository.DeleteRange(locations);
        return _repository.SaveChangesAsync(cancellationToken);
    }
}