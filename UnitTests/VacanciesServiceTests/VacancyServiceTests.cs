using AutoMapper;
using Core.Database;
using Core.Exceptions;
using VacanciesService.Application.Services;
using VacanciesService.Domain.Contacts;
using VacanciesService.Domain.DTO;
using VacanciesService.Domain.Models;
using ValidationException = Core.Exceptions.ValidationException;

namespace UnitTests.VacanciesServiceTests;

public class VacancyServiceTests
{
    private readonly Mock<IRepository> _repositoryMock = new Mock<IRepository>();
    private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
    private readonly Mock<IValidator<VacancyCreateDto>> _createValidatorMock = new Mock<IValidator<VacancyCreateDto>>();
    private readonly Mock<IValidator<VacancyUpdateDto>> _updateValidatorMock = new Mock<IValidator<VacancyUpdateDto>>();
    private readonly IVacanciesService _vacancyService;

    public VacancyServiceTests()
    {
        _vacancyService = new VacancyService(
            _repositoryMock.Object,
            _mapperMock.Object
        );
    }
    
    // [Fact]
    // public async Task GetVacancyById_ExistingId_ShouldReturnVacancy()
    // {
    //     // Arrange
    //     var existingId = Guid.NewGuid();
    //     var mockVacancy = new Vacancy { Id = existingId, Title = "Test Vacancy" };
    //     _repositoryMock.Setup(repo => repo.GetByIdAsync<Vacancy>(existingId)).ReturnsAsync(mockVacancy);
    //
    //     // Act
    //     var result = await _vacancyService.GetVacancyById(existingId);
    //
    //     // Assert
    //     Assert.Equal(mockVacancy, result);
    // }

    [Fact]
    public async Task GetVacancyById_NonExistingId_ShouldThrowException()
    {
        // Arrange
        var nonExistingId = Guid.NewGuid();
        _repositoryMock.Setup(repo => repo.GetByIdAsync<Vacancy>(nonExistingId)).ReturnsAsync((Vacancy)null);

        // Act and Assert
        await Assert.ThrowsAsync<ExceptionWithStatusCode>(() => _vacancyService.GetVacancyById(nonExistingId));
    }

    [Fact]
    public async Task UpdateVacancy_NonExistingVacancy_ShouldThrowException()
    {
        // Arrange
        var vacancyUpdateDto = new VacancyUpdateDto
        {
            // Initialize your vacancy update DTO here
        };
        _updateValidatorMock.Setup(validator => validator.ValidateAsync(vacancyUpdateDto, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
        _repositoryMock.Setup(repo => repo.AnyAsync<Vacancy>(x => x.Id == vacancyUpdateDto.Id)).ReturnsAsync(false);

        // Act and Assert
        await Assert.ThrowsAsync<ExceptionWithStatusCode>(() => _vacancyService.UpdateVacancy(vacancyUpdateDto));
    }

    [Fact]
    public async Task DeleteVacancy_ExistingId_ShouldDeleteVacancy()
    {
        // Arrange
        var existingId = Guid.NewGuid();
        var mockVacancy = new Vacancy { Id = existingId, Title = "Test Vacancy" };
        _repositoryMock.Setup(repo => repo.GetByIdAsync<Vacancy>(existingId)).ReturnsAsync(mockVacancy);

        // Act
        await _vacancyService.DeleteVacancy(existingId);

        // Assert
        _repositoryMock.Verify(repo => repo.Delete(mockVacancy), Times.Once);
        _repositoryMock.Verify(repo => repo.SaveChangesAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task DeleteVacancy_NonExistingId_ShouldThrowException()
    {
        // Arrange
        var nonExistingId = Guid.NewGuid();
        _repositoryMock.Setup(repo => repo.GetByIdAsync<Vacancy>(nonExistingId)).ReturnsAsync((Vacancy)null);

        // Act and Assert
        await Assert.ThrowsAsync<ExceptionWithStatusCode>(() => _vacancyService.DeleteVacancy(nonExistingId));
    }

    [Fact]
    public async Task ActivateDeactivateVacancy_ExistingId_ShouldToggleIsActive()
    {
        // Arrange
        var existingId = Guid.NewGuid();
        var mockVacancy = new Vacancy { Id = existingId, Title = "Test Vacancy", IsActive = true };
        _repositoryMock.Setup(repo => repo.GetByIdAsync<Vacancy>(existingId)).ReturnsAsync(mockVacancy);

        // Act
        await _vacancyService.ActivateDeactivateVacancy(existingId);

        // Assert
        Assert.False(mockVacancy.IsActive);
        _repositoryMock.Verify(repo => repo.Update(mockVacancy), Times.Once);
        _repositoryMock.Verify(repo => repo.SaveChangesAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task ActivateDeactivateVacancy_NonExistingId_ShouldThrowException()
    {
        // Arrange
        var nonExistingId = Guid.NewGuid();
        _repositoryMock.Setup(repo => repo.GetByIdAsync<Vacancy>(nonExistingId)).ReturnsAsync((Vacancy)null!);

        // Act and Assert
        await Assert.ThrowsAsync<ExceptionWithStatusCode>(() => _vacancyService.ActivateDeactivateVacancy(nonExistingId));
    }
}

