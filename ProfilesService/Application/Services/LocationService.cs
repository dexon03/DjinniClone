using AutoMapper;
using Core.Database;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProfilesService.Domain.Contracts;
using ProfilesService.Domain.DTO;
using ProfilesService.Domain.Models;

namespace ProfilesService.Application.Services;

public class LocationService : ILocationService
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<LocationCreateDto> _createValidator;
    private readonly IValidator<LocationUpdateDto> _updateValidator;

    public LocationService(IRepository repository, 
        IMapper mapper,
        IValidator<LocationCreateDto> createValidator, 
        IValidator<LocationUpdateDto> updateValidator)
    {
        _repository = repository;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
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
        var validationResult = await _createValidator.ValidateAsync(location,cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var locationEntity = _mapper.Map<Location>(location);
        var result = await _repository.CreateAsync(locationEntity);
        await _repository.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task<Location> UpdateLocation(LocationUpdateDto location, CancellationToken cancellationToken = default)
    {
        var validationResult = await _updateValidator.ValidateAsync(location,cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
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