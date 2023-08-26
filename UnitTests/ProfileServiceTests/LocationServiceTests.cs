using AutoMapper;
using Core.Database;
using ProfilesService.Application.Services;
using ProfilesService.Domain.DTO;
using ProfilesService.Domain.Models;
using ValidationException = Core.Exceptions.ValidationException;

namespace UnitTests.ProfileServiceTests;


public class LocationServiceTests
{
    private readonly Mock<IRepository> _repositoryMock = new Mock<IRepository>();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly Mock<IValidator<LocationCreateDto>> _createValidatorMock = new();
    private readonly Mock<IValidator<LocationUpdateDto>> _updateValidatorMock = new();

    private readonly LocationService _locationService;

    public LocationServiceTests()
    {
        _locationService = new LocationService(
            _repositoryMock.Object,
            _mapperMock.Object,
            _createValidatorMock.Object,
            _updateValidatorMock.Object
        );
    }

    [Fact]
    public async Task GetLocationById_ExistingId_ShouldReturnLocation()
    {
        // Arrange
        var existingId = Guid.NewGuid();
        var mockLocation = new Location { Id = existingId, Country = "Test Country", City = "Test City" };
        _repositoryMock.Setup(repo => repo.GetByIdAsync<Location>(existingId)).ReturnsAsync(mockLocation);

        // Act
        var result = await _locationService.GetLocationById(existingId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingId, result.Id);
        Assert.Equal(mockLocation.Country, result.Country);
        Assert.Equal(mockLocation.City, result.City);
    }

    [Fact]
    public async Task GetLocationById_NonExistingId_ShouldThrowException()
    {
        // Arrange
        var nonExistingId = Guid.NewGuid();
        _repositoryMock.Setup(repo => repo.GetByIdAsync<Location>(nonExistingId)).ReturnsAsync((Location)null!);

        // Act and Assert
        await Assert.ThrowsAsync<Exception>(() => _locationService.GetLocationById(nonExistingId));
    }

    [Fact]
    public async Task CreateLocation_ValidDto_ShouldCreateAndReturnLocation()
    {
        // Arrange
        var locationCreateDto = new LocationCreateDto { Country = "New Country", City = "New City" };
        var mockLocation = new Location { Id = Guid.NewGuid(), Country = locationCreateDto.Country, City = locationCreateDto.City };
        _createValidatorMock.Setup(validator => validator.ValidateAsync(locationCreateDto, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());

        _mapperMock.Setup(mapper => mapper.Map<Location>(locationCreateDto)).Returns(mockLocation);
        _repositoryMock.Setup(repo => repo.CreateAsync(mockLocation)).ReturnsAsync(mockLocation);

        // Act
        var result = await _locationService.CreateLocation(locationCreateDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(locationCreateDto.Country, result.Country);
        Assert.Equal(locationCreateDto.City, result.City);
    }

    [Fact]
    public async Task CreateLocation_InvalidDto_ShouldThrowValidationException()
    {
        // Arrange
        var locationCreateDto = new LocationCreateDto { Country = "New Country", City = null };
        var validationFailures = new List<FluentValidation.Results.ValidationFailure> { new FluentValidation.Results.ValidationFailure("City", "City is required") };
        _createValidatorMock.Setup(validator => validator.ValidateAsync(locationCreateDto, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult(validationFailures));

        // Act and Assert
        await Assert.ThrowsAsync<ValidationException>(() => _locationService.CreateLocation(locationCreateDto));
    }

    [Fact]
    public async Task UpdateLocation_ValidDtoAndExistingLocation_ShouldUpdateAndReturnLocation()
    {
        // Arrange
        var locationUpdateDto = new LocationUpdateDto { Id = Guid.NewGuid(), Country = "Updated Country", City = "Updated City" };
        var mappedLocation = new Location { Id = locationUpdateDto.Id, Country = "Updated Country", City = "Updated City" };
        _updateValidatorMock.Setup(validator => validator.ValidateAsync(locationUpdateDto, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());

        _mapperMock.Setup(mapper => mapper.Map<Location>(locationUpdateDto)).Returns(mappedLocation);
        _repositoryMock.Setup(repo => repo.AnyAsync<Location>(x => x.Id == locationUpdateDto.Id)).ReturnsAsync(true);
        _repositoryMock.Setup(repo => repo.Update(mappedLocation)).Returns(mappedLocation);

        // Act
        var result = await _locationService.UpdateLocation(locationUpdateDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(locationUpdateDto.Id, result.Id);
        Assert.Equal(locationUpdateDto.Country, result.Country);
        Assert.Equal(locationUpdateDto.City, result.City);
    }

    [Fact]
    public async Task UpdateLocation_InvalidDto_ShouldThrowValidationException()
    {
        // Arrange
        var locationUpdateDto = new LocationUpdateDto { Id = Guid.NewGuid(), Country = "Updated Country", City = null };
        var validationFailures = new List<FluentValidation.Results.ValidationFailure> { new FluentValidation.Results.ValidationFailure("City", "City is required") };
        _updateValidatorMock.Setup(validator => validator.ValidateAsync(locationUpdateDto, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult(validationFailures));

        // Act and Assert
        await Assert.ThrowsAsync<ValidationException>(() => _locationService.UpdateLocation(locationUpdateDto));
    }

    [Fact]
    public async Task UpdateLocation_NonExistingLocation_ShouldThrowException()
    {
        // Arrange
        var locationUpdateDto = new LocationUpdateDto { Id = Guid.NewGuid(), Country = "Updated Country", City = "Updated City" };
        _updateValidatorMock.Setup(validator => validator.ValidateAsync(locationUpdateDto, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
        _repositoryMock.Setup(repo => repo.AnyAsync<Location>(x => x.Id == locationUpdateDto.Id)).ReturnsAsync(false);

        // Act and Assert
        await Assert.ThrowsAsync<Exception>(() => _locationService.UpdateLocation(locationUpdateDto));
    }

    [Fact]
    public async Task DeleteLocation_ExistingId_ShouldDeleteLocation()
    {
        // Arrange
        var existingId = Guid.NewGuid();
        var mockLocation = new Location { Id = existingId, Country = "Test Country", City = "Test City" };
        _repositoryMock.Setup(repo => repo.GetByIdAsync<Location>(existingId)).ReturnsAsync(mockLocation);

        // Act
        await _locationService.DeleteLocation(existingId);

        // Assert
        _repositoryMock.Verify(repo => repo.Delete(mockLocation), Times.Once);
        _repositoryMock.Verify(repo => repo.SaveChangesAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task DeleteLocation_NonExistingId_ShouldThrowException()
    {
        // Arrange
        var nonExistingId = Guid.NewGuid();
        _repositoryMock.Setup(repo => repo.GetByIdAsync<Location>(nonExistingId)).ReturnsAsync((Location)null);

        // Act and Assert
        await Assert.ThrowsAsync<Exception>(() => _locationService.DeleteLocation(nonExistingId));
    }

    [Fact]
    public async Task DeleteManyLocations_ShouldDeleteRangeAndSaveChanges()
    {
        // Arrange
        var locationsToDelete = new[]
        {
            new Location { Id = Guid.NewGuid(), Country = "Country 1", City = "City 1" },
            new Location { Id = Guid.NewGuid(), Country = "Country 2", City = "City 2" }
        };

        // Act
        await _locationService.DeleteManyLocations(locationsToDelete);

        // Assert
        _repositoryMock.Verify(repo => repo.DeleteRange(locationsToDelete), Times.Once);
        _repositoryMock.Verify(repo => repo.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}