using AutoMapper;
using Core.Database;
using VacanciesService.Application.Services;
using VacanciesService.Domain.DTO;
using VacanciesService.Domain.Models;
using ValidationException = Core.Exceptions.ValidationException;

namespace UnitTests.VacanciesServiceTests;

public class CompanyServiceTests
{
    private readonly Mock<IRepository> _repositoryMock = new Mock<IRepository>();
    private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
    private readonly Mock<IValidator<CompanyCreateDto>> _createValidatorMock = new Mock<IValidator<CompanyCreateDto>>();
    private readonly Mock<IValidator<CompanyUpdateDto>> _updateValidatorMock = new Mock<IValidator<CompanyUpdateDto>>();

    private readonly CompanyService _companyService;

    public CompanyServiceTests()
    {
        _companyService = new CompanyService(
            _repositoryMock.Object,
            _mapperMock.Object,
            _createValidatorMock.Object,
            _updateValidatorMock.Object
        );
    }

    [Fact]
    public async Task GetCompanyById_ExistingId_ShouldReturnCompany()
    {
        // Arrange
        var existingId = Guid.NewGuid();
        var mockCompany = new Company { Id = existingId, Name = "Test Company", Description = "Test Description" };
        _repositoryMock.Setup(repo => repo.GetByIdAsync<Company>(existingId)).ReturnsAsync(mockCompany);

        // Act
        var result = await _companyService.GetCompanyById(existingId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingId, result.Id);
    }

    [Fact]
    public async Task GetCompanyById_NonExistingId_ShouldThrowException()
    {
        // Arrange
        var nonExistingId = Guid.NewGuid();
        _repositoryMock.Setup(repo => repo.GetByIdAsync<Company>(nonExistingId)).ReturnsAsync((Company)null);

        // Act and Assert
        await Assert.ThrowsAsync<Exception>(() => _companyService.GetCompanyById(nonExistingId));
    }

    [Fact]
    public async Task CreateCompany_ValidDto_ShouldCreateAndReturnCompany()
    {
        // Arrange
        var companyCreateDto = new CompanyCreateDto
        {
            Name = "New Company",
            Description = "New Description"
        };
        var mockCompany = new Company { Id = Guid.NewGuid(), Name = companyCreateDto.Name, Description = companyCreateDto.Description };
        _createValidatorMock.Setup(validator => validator.ValidateAsync(companyCreateDto, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());

        _mapperMock.Setup(mapper => mapper.Map<Company>(companyCreateDto)).Returns(mockCompany);
        _repositoryMock.Setup(repo => repo.CreateAsync(mockCompany)).ReturnsAsync(mockCompany);

        // Act
        var result = await _companyService.CreateCompany(companyCreateDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(companyCreateDto.Name, result.Name);
        Assert.Equal(companyCreateDto.Description, result.Description);
    }

    [Fact]
    public async Task CreateCompany_InvalidDto_ShouldThrowValidationException()
    {
        // Arrange
        var companyCreateDto = new CompanyCreateDto
        {
            Name = null, // Name is required
            Description = "New Description"
        };
        var validationFailures = new List<FluentValidation.Results.ValidationFailure> { new FluentValidation.Results.ValidationFailure("Name", "Name is required") };
        _createValidatorMock.Setup(validator => validator.ValidateAsync(companyCreateDto, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult(validationFailures));

        // Act and Assert
        await Assert.ThrowsAsync<ValidationException>(() => _companyService.CreateCompany(companyCreateDto));
    }

    [Fact]
    public async Task UpdateCompany_ValidDtoAndExistingCompany_ShouldUpdateAndReturnCompany()
    {
        // Arrange
        var companyUpdateDto = new CompanyUpdateDto
        {
            Id = Guid.NewGuid(),
            Name = "Updated Company",
            Description = "Updated Description"
        };
        var mappedCompany = new Company { Id = companyUpdateDto.Id, Name = companyUpdateDto.Name, Description = companyUpdateDto.Description };
        _updateValidatorMock.Setup(validator => validator.ValidateAsync(companyUpdateDto, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());

        _mapperMock.Setup(mapper => mapper.Map<Company>(companyUpdateDto)).Returns(mappedCompany);
        _repositoryMock.Setup(repo => repo.AnyAsync<Company>(x => x.Id == mappedCompany.Id)).ReturnsAsync(true);
        _repositoryMock.Setup(repo => repo.Update(mappedCompany)).Returns(mappedCompany);

        // Act
        var result = await _companyService.UpdateCompany(companyUpdateDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(companyUpdateDto.Id, result.Id);
        Assert.Equal(companyUpdateDto.Name, result.Name);
        Assert.Equal(companyUpdateDto.Description, result.Description);
    }

        [Fact]
    public async Task UpdateCompany_InvalidDto_ShouldThrowValidationException()
    {
        // Arrange
        var companyUpdateDto = new CompanyUpdateDto
        {
            Id = Guid.NewGuid(),
            Name = null, // Name is required
            Description = "Updated Description"
        };
        var validationFailures = new List<FluentValidation.Results.ValidationFailure> { new FluentValidation.Results.ValidationFailure("Name", "Name is required") };
        _updateValidatorMock.Setup(validator => validator.ValidateAsync(companyUpdateDto, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult(validationFailures));

        // Act and Assert
        await Assert.ThrowsAsync<ValidationException>(() => _companyService.UpdateCompany(companyUpdateDto));
    }

    [Fact]
    public async Task UpdateCompany_NonExistingCompany_ShouldThrowException()
    {
        // Arrange
        var companyUpdateDto = new CompanyUpdateDto
        {
            Id = Guid.NewGuid(),
            Name = "Updated Company",
            Description = "Updated Description"
        };
        _updateValidatorMock.Setup(validator => validator.ValidateAsync(companyUpdateDto, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
        _repositoryMock.Setup(repo => repo.AnyAsync<Company>(x => x.Id == companyUpdateDto.Id)).ReturnsAsync(false);

        // Act and Assert
        await Assert.ThrowsAsync<Exception>(() => _companyService.UpdateCompany(companyUpdateDto));
    }

    [Fact]
    public async Task DeleteCompany_ExistingId_ShouldDeleteCompany()
    {
        // Arrange
        var existingId = Guid.NewGuid();
        var mockCompany = new Company { Id = existingId, Name = "Test Company", Description = "Test Description" };
        _repositoryMock.Setup(repo => repo.GetByIdAsync<Company>(existingId)).ReturnsAsync(mockCompany);

        // Act
        await _companyService.DeleteCompany(existingId);

        // Assert
        _repositoryMock.Verify(repo => repo.Delete(mockCompany), Times.Once);
        _repositoryMock.Verify(repo => repo.SaveChangesAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task DeleteCompany_NonExistingId_ShouldThrowException()
    {
        // Arrange
        var nonExistingId = Guid.NewGuid();
        _repositoryMock.Setup(repo => repo.GetByIdAsync<Company>(nonExistingId)).ReturnsAsync((Company)null);

        // Act and Assert
        await Assert.ThrowsAsync<Exception>(() => _companyService.DeleteCompany(nonExistingId));
    }
}
