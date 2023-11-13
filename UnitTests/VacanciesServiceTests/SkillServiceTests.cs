using AutoMapper;
using Core.Database;
using VacanciesService.Application.Services;
using VacanciesService.Domain.DTO;
using VacanciesService.Domain.Models;
using ValidationException = Core.Exceptions.ValidationException;

namespace UnitTests.VacanciesServiceTests;

public class SkillServiceTests
{
    private readonly Mock<IRepository> _repositoryMock = new Mock<IRepository>();
    private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
    private readonly Mock<IValidator<SkillCreateDto>> _createValidatorMock = new Mock<IValidator<SkillCreateDto>>();
    private readonly Mock<IValidator<SkillUpdateDto>> _updateValidatorMock = new Mock<IValidator<SkillUpdateDto>>();

    private readonly SkillService _skillService;

    public SkillServiceTests()
    {
        _skillService = new SkillService(
            _repositoryMock.Object,
            _mapperMock.Object
        );
    }

    [Fact]
    public async Task GetSkillById_ExistingId_ShouldReturnSkill()
    {
        // Arrange
        var existingId = Guid.NewGuid();
        var mockSkill = new Skill { Id = existingId, Name = "Test Skill" };
        _repositoryMock.Setup(repo => repo.GetByIdAsync<Skill>(existingId)).ReturnsAsync(mockSkill);

        // Act
        var result = await _skillService.GetSkillById(existingId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingId, result.Id);
    }

    [Fact]
    public async Task GetSkillById_NonExistingId_ShouldThrowException()
    {
        // Arrange
        var nonExistingId = Guid.NewGuid();
        _repositoryMock.Setup(repo => repo.GetByIdAsync<Skill>(nonExistingId)).ReturnsAsync((Skill)null!);

        // Act and Assert
        await Assert.ThrowsAsync<Exception>(() => _skillService.GetSkillById(nonExistingId));
    }

    [Fact]
    public async Task CreateSkill_ValidDto_ShouldCreateAndReturnSkill()
    {
        // Arrange
        var skillCreateDto = new SkillCreateDto
        {
            Name = "New Skill"
        };
        var mockSkill = new Skill { Id = Guid.NewGuid(), Name = skillCreateDto.Name };
        _createValidatorMock.Setup(validator => validator.ValidateAsync(skillCreateDto, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());

        _mapperMock.Setup(mapper => mapper.Map<Skill>(skillCreateDto)).Returns(mockSkill);
        _repositoryMock.Setup(repo => repo.CreateAsync(mockSkill)).ReturnsAsync(mockSkill);

        // Act
        var result = await _skillService.CreateSkill(skillCreateDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(skillCreateDto.Name, result.Name);
    }

    [Fact]
    public async Task UpdateSkill_ValidDtoAndExistingSkill_ShouldUpdateAndReturnSkill()
    {
        // Arrange
        var skillUpdateDto = new SkillUpdateDto
        {
            Id = Guid.NewGuid(),
            Name = "Updated Skill"
        };
        var mappedSkill = new Skill { Id = skillUpdateDto.Id, Name = "Updated Skill" };
        _updateValidatorMock.Setup(validator => validator.ValidateAsync(skillUpdateDto, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());

        _mapperMock.Setup(mapper => mapper.Map<Skill>(skillUpdateDto)).Returns(mappedSkill);
        _repositoryMock.Setup(repo => repo.AnyAsync<Skill>(x => x.Id == mappedSkill.Id)).ReturnsAsync(true);
        _repositoryMock.Setup(repo => repo.Update(mappedSkill)).Returns(mappedSkill);

        // Act
        var result = await _skillService.UpdateSkill(skillUpdateDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(skillUpdateDto.Id, result.Id);
        Assert.Equal(skillUpdateDto.Name, result.Name);
    }

    [Fact]
    public async Task UpdateSkill_NonExistingSkill_ShouldThrowException()
    {
        // Arrange
        var skillUpdateDto = new SkillUpdateDto
        {
            Id = Guid.NewGuid(),
            Name = "Updated Skill"
        };
        _updateValidatorMock.Setup(validator => validator.ValidateAsync(skillUpdateDto, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
        _repositoryMock.Setup(repo => repo.AnyAsync<Skill>(x => x.Id == skillUpdateDto.Id)).ReturnsAsync(false);

        // Act and Assert
        await Assert.ThrowsAsync<Exception>(() => _skillService.UpdateSkill(skillUpdateDto));
    }

    [Fact]
    public async Task DeleteSkill_ExistingId_ShouldDeleteSkill()
    {
        // Arrange
        var existingId = Guid.NewGuid();
        var mockSkill = new Skill { Id = existingId, Name = "Test Skill" };
        _repositoryMock.Setup(repo => repo.GetByIdAsync<Skill>(existingId)).ReturnsAsync(mockSkill);

        // Act
        await _skillService.DeleteSkill(existingId);

        // Assert
        _repositoryMock.Verify(repo => repo.Delete(mockSkill), Times.Once);
        _repositoryMock.Verify(repo => repo.SaveChangesAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task DeleteSkill_NonExistingId_ShouldThrowException()
    {
        // Arrange
        var nonExistingId = Guid.NewGuid();
        _repositoryMock.Setup(repo => repo.GetByIdAsync<Skill>(nonExistingId)).ReturnsAsync((Skill)null!);

        // Act and Assert
        await Assert.ThrowsAsync<Exception>(() => _skillService.DeleteSkill(nonExistingId));
    }

    [Fact]
    public async Task DeleteManySkills_ShouldDeleteRangeAndSaveChanges()
    {
        // Arrange
        var skillsToDelete = new[]
        {
            new Skill { Id = Guid.NewGuid(), Name = "Skill 1" },
            new Skill { Id = Guid.NewGuid(), Name = "Skill 2" }
        };

        // Act
        await _skillService.DeleteManySkills(skillsToDelete);

        // Assert
        _repositoryMock.Verify(repo => repo.DeleteRange(skillsToDelete), Times.Once);
        _repositoryMock.Verify(repo => repo.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}

