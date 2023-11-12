using AutoMapper;
using Core.Database;
using ProfilesService.Application.Services;
using ProfilesService.Domain.DTO;
using ProfilesService.Domain.Models;
using ValidationException = Core.Exceptions.ValidationException;

namespace UnitTests.ProfileServiceTests;

public class UserServiceTests
{
    private readonly Mock<IRepository> _repositoryMock = new Mock<IRepository>();
    private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
    private readonly Mock<IValidator<UserCreateDto>> _createValidatorMock = new Mock<IValidator<UserCreateDto>>();
    private readonly Mock<IValidator<UserUpdateDto>> _updateValidatorMock = new Mock<IValidator<UserUpdateDto>>();

    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userService = new UserService(
            _repositoryMock.Object,
            _mapperMock.Object
        );
    }

    [Fact]
    public async Task GetUserById_ExistingId_ShouldReturnUser()
    {
        // Arrange
        var existingId = Guid.NewGuid();
        var mockUser = new User { Id = existingId, UserName = "TestUser", Email = "test@example.com", UserRole = "TestRole" };
        _repositoryMock.Setup(repo => repo.GetByIdAsync<User>(existingId)).ReturnsAsync(mockUser);

        // Act
        var result = await _userService.GetUserById(existingId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingId, result.Id);
    }

    [Fact]
    public async Task GetUserById_NonExistingId_ShouldThrowException()
    {
        // Arrange
        var nonExistingId = Guid.NewGuid();
        _repositoryMock.Setup(repo => repo.GetByIdAsync<User>(nonExistingId)).ReturnsAsync((User)null);

        // Act and Assert
        await Assert.ThrowsAsync<Exception>(() => _userService.GetUserById(nonExistingId));
    }

    [Fact]
    public async Task CreateUser_ValidDto_ShouldCreateAndReturnUser()
    {
        // Arrange
        var userCreateDto = new UserCreateDto { UserName = "NewUser", Email = "newuser@example.com", UserRole = "NewRole" };
        var mockUser = new User { Id = Guid.NewGuid(), UserName = userCreateDto.UserName, Email = userCreateDto.Email, UserRole = userCreateDto.UserRole };
        _createValidatorMock.Setup(validator => validator.ValidateAsync(userCreateDto, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());

        _mapperMock.Setup(mapper => mapper.Map<User>(userCreateDto)).Returns(mockUser);
        _repositoryMock.Setup(repo => repo.CreateAsync(mockUser)).ReturnsAsync(mockUser);

        // Act
        var result = await _userService.CreateUser(userCreateDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userCreateDto.UserName, result.UserName);
        Assert.Equal(userCreateDto.Email, result.Email);
        Assert.Equal(userCreateDto.UserRole, result.UserRole);
    }

    [Fact]
    public async Task UpdateUser_ValidDtoAndExistingUser_ShouldUpdateAndReturnUser()
    {
        // Arrange
        var userUpdateDto = new UserUpdateDto { Id = Guid.NewGuid(), UserName = "UpdatedUser", Email = "updateduser@example.com", UserRole = "UpdatedRole" };
        var mappedUser = new User { Id = userUpdateDto.Id, UserName = "UpdatedUser", Email = "updateduser@example.com", UserRole = "UpdatedRole" };
        _updateValidatorMock.Setup(validator => validator.ValidateAsync(userUpdateDto, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());

        _mapperMock.Setup(mapper => mapper.Map<User>(userUpdateDto)).Returns(mappedUser);
        _repositoryMock.Setup(repo => repo.AnyAsync<User>(x => x.Id == mappedUser.Id)).ReturnsAsync(true);
        _repositoryMock.Setup(repo => repo.Update(mappedUser)).Returns(mappedUser);

        // Act
        var result = await _userService.UpdateUser(userUpdateDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userUpdateDto.Id, result.Id);
        Assert.Equal(userUpdateDto.UserName, result.UserName);
        Assert.Equal(userUpdateDto.Email, result.Email);
        Assert.Equal(userUpdateDto.UserRole, result.UserRole);
    }

    [Fact]
    public async Task UpdateUser_NonExistingUser_ShouldThrowException()
    {
        // Arrange
        var userUpdateDto = new UserUpdateDto { Id = Guid.NewGuid(), UserName = "UpdatedUser", Email = "updateduser@example.com", UserRole = "UpdatedRole" };
        _updateValidatorMock.Setup(validator => validator.ValidateAsync(userUpdateDto, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
        _repositoryMock.Setup(repo => repo.AnyAsync<User>(x => x.Id == userUpdateDto.Id)).ReturnsAsync(false);

        // Act and Assert
        await Assert.ThrowsAsync<Exception>(() => _userService.UpdateUser(userUpdateDto));
    }

    [Fact]
    public async Task DeleteUser_ExistingId_ShouldDeleteUser()
    {
        // Arrange
        var existingId = Guid.NewGuid();
        var mockUser = new User { Id = existingId, UserName = "TestUser", Email = "test@example.com", UserRole = "TestRole" };
        _repositoryMock.Setup(repo => repo.GetByIdAsync<User>(existingId)).ReturnsAsync(mockUser);

        // Act
        await _userService.DeleteUser(existingId);

        // Assert
        _repositoryMock.Verify(repo => repo.Delete(mockUser), Times.Once);
        _repositoryMock.Verify(repo => repo.SaveChangesAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task DeleteUser_NonExistingId_ShouldThrowException()
    {
        // Arrange
        var nonExistingId = Guid.NewGuid();
        _repositoryMock.Setup(repo => repo.GetByIdAsync<User>(nonExistingId)).ReturnsAsync((User)null);

        // Act and Assert
        await Assert.ThrowsAsync<Exception>(() => _userService.DeleteUser(nonExistingId));
    }

    [Fact]
    public async Task DeleteManyUsers_ShouldDeleteRangeAndSaveChanges()
    {
        // Arrange
        var usersToDelete = new[]
        {
            new User { Id = Guid.NewGuid(), UserName = "User1", Email = "user1@example.com", UserRole = "Role1" },
            new User { Id = Guid.NewGuid(), UserName = "User2", Email = "user2@example.com", UserRole = "Role2" }
        };

        // Act
        await _userService.DeleteManyUsers(usersToDelete);

        // Assert
        _repositoryMock.Verify(repo => repo.DeleteRange(usersToDelete), Times.Once);
        _repositoryMock.Verify(repo => repo.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}
