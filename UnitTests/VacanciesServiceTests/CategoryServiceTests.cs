using AutoMapper;
using Core.Database;
using VacanciesService.Application.Services;
using VacanciesService.Domain.DTO;
using VacanciesService.Domain.Models;
using ValidationException = Core.Exceptions.ValidationException;

namespace UnitTests.VacanciesServiceTests;

public class CategoryServiceTests
{
    private readonly Mock<IRepository> _repositoryMock = new Mock<IRepository>();
    private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
    private readonly Mock<IValidator<CategoryCreateDto>> _createValidatorMock = new Mock<IValidator<CategoryCreateDto>>();
    private readonly Mock<IValidator<CategoryUpdateDto>> _updateValidatorMock = new Mock<IValidator<CategoryUpdateDto>>();

    private readonly CategoryService _categoryService;

    public CategoryServiceTests()
    {
        _categoryService = new CategoryService(
            _repositoryMock.Object,
            _mapperMock.Object
        );
    }

    [Fact]
    public async Task GetCategoryById_ExistingId_ShouldReturnCategory()
    {
        // Arrange
        var existingId = Guid.NewGuid();
        var mockCategory = new Category { Id = existingId, Name = "Test Category" };
        _repositoryMock.Setup(repo => repo.GetByIdAsync<Category>(existingId)).ReturnsAsync(mockCategory);

        // Act
        var result = await _categoryService.GetCategoryById(existingId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingId, result.Id);
    }

    [Fact]
    public async Task GetCategoryById_NonExistingId_ShouldThrowException()
    {
        // Arrange
        var nonExistingId = Guid.NewGuid();
        _repositoryMock.Setup(repo => repo.GetByIdAsync<Category>(nonExistingId)).ReturnsAsync((Category)null!);

        // Act and Assert
        await Assert.ThrowsAsync<Exception>(() => _categoryService.GetCategoryById(nonExistingId));
    }

    [Fact]
    public async Task CreateCategory_ValidDto_ShouldCreateAndReturnCategory()
    {
        // Arrange
        var categoryCreateDto = new CategoryCreateDto
        {
            Name = "New Category"
        };
        var mockCategory = new Category { Id = Guid.NewGuid(), Name = categoryCreateDto.Name };
        _createValidatorMock.Setup(validator => validator.ValidateAsync(categoryCreateDto, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());

        _mapperMock.Setup(mapper => mapper.Map<Category>(categoryCreateDto)).Returns(mockCategory);
        _repositoryMock.Setup(repo => repo.CreateAsync(mockCategory)).ReturnsAsync(mockCategory);

        // Act
        var result = await _categoryService.CreateCategory(categoryCreateDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(categoryCreateDto.Name, result.Name);
    }

    [Fact]
    public async Task UpdateCategory_ValidDtoAndExistingCategory_ShouldUpdateAndReturnCategory()
    {
        // Arrange
        var categoryUpdateDto = new CategoryUpdateDto
        {
            Id = Guid.NewGuid(),
            Name = "Updated Category"
        };
        var mockCategory = new Category { Id = categoryUpdateDto.Id, Name = "Initial Category" };
        var mappedCategory = new Category { Id = categoryUpdateDto.Id, Name = categoryUpdateDto.Name };
        _updateValidatorMock.Setup(validator => validator.ValidateAsync(categoryUpdateDto, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());

        _mapperMock.Setup(mapper => mapper.Map<Category>(categoryUpdateDto)).Returns(mappedCategory);
        _repositoryMock.Setup(repo => repo.AnyAsync<Category>(x => x.Id == mappedCategory.Id)).ReturnsAsync(true);
        _repositoryMock.Setup(repo => repo.Update(mappedCategory)).Returns(mappedCategory);

        // Act
        var result = await _categoryService.UpdateCategory(categoryUpdateDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(categoryUpdateDto.Id, result.Id);
        Assert.Equal(categoryUpdateDto.Name, result.Name);
    }

    [Fact]
    public async Task UpdateCategory_NonExistingCategory_ShouldThrowException()
    {
        // Arrange
        var categoryUpdateDto = new CategoryUpdateDto
        {
            Id = Guid.NewGuid(),
            Name = "Updated Category"
        };
        _updateValidatorMock.Setup(validator => validator.ValidateAsync(categoryUpdateDto, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
        _repositoryMock.Setup(repo => repo.AnyAsync<Category>(x => x.Id == categoryUpdateDto.Id)).ReturnsAsync(false);

        // Act and Assert
        await Assert.ThrowsAsync<Exception>(() => _categoryService.UpdateCategory(categoryUpdateDto));
    }

    [Fact]
    public async Task DeleteCategory_ExistingId_ShouldDeleteCategory()
    {
        // Arrange
        var existingId = Guid.NewGuid();
        var mockCategory = new Category { Id = existingId, Name = "Test Category" };
        _repositoryMock.Setup(repo => repo.GetByIdAsync<Category>(existingId)).ReturnsAsync(mockCategory);

        // Act
        await _categoryService.DeleteCategory(existingId);

        // Assert
        _repositoryMock.Verify(repo => repo.Delete(mockCategory), Times.Once);
        _repositoryMock.Verify(repo => repo.SaveChangesAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task DeleteCategory_NonExistingId_ShouldThrowException()
    {
        // Arrange
        var nonExistingId = Guid.NewGuid();
        _repositoryMock.Setup(repo => repo.GetByIdAsync<Category>(nonExistingId)).ReturnsAsync((Category)null);

        // Act and Assert
        await Assert.ThrowsAsync<Exception>(() => _categoryService.DeleteCategory(nonExistingId));
    }

    [Fact]
    public async Task DeleteMany_ShouldDeleteRangeAndSaveChanges()
    {
        // Arrange
        var categoriesToDelete = new[]
        {
            new Category { Id = Guid.NewGuid(), Name = "Category 1" },
            new Category { Id = Guid.NewGuid(), Name = "Category 2" }
        };

        // Act
        await _categoryService.DeleteMany(categoriesToDelete);

        // Assert
        _repositoryMock.Verify(repo => repo.DeleteRange(categoriesToDelete), Times.Once);
        _repositoryMock.Verify(repo => repo.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}

