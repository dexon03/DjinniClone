// using AutoMapper;
// using Core.Database;
// using Core.Exceptions;
// using ProfilesService.Application.Services;
// using ProfilesService.Domain.DTO;
// using ProfilesService.Domain.Models;
// using ValidationException = Core.Exceptions.ValidationException;
//
// namespace UnitTests.ProfileServiceTests;
//
// public class ProfileServiceTests
// {
//     private readonly Mock<IRepository> _repositoryMock = new Mock<IRepository>();
//     private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
//     private readonly Mock<IValidator<ProfileCreateDto>> _createValidatorMock = new Mock<IValidator<ProfileCreateDto>>();
//     private readonly Mock<IValidator<CandidateProfileUpdateDto>> _updateValidatorMock = new Mock<IValidator<CandidateProfileUpdateDto>>();
//
//     private readonly ProfileService _profileService;
//
//     public ProfileServiceTests()
//     {
//         _profileService = new ProfileService(
//             _repositoryMock.Object,
//             _mapperMock.Object,
//             _createValidatorMock.Object,
//             _updateValidatorMock.Object
//         );
//     }
//
//     [Fact]
//     public async Task GetProfileById_ExistingId_ShouldReturnProfile()
//     {
//         // Arrange
//         var existingId = Guid.NewGuid();
//         var mockProfile = new CandidateProfile { Id = existingId, UserId = Guid.NewGuid(), Name = "Test", Surname = "User" };
//         _repositoryMock.Setup(repo => repo.GetByIdAsync<CandidateProfile>(existingId)).ReturnsAsync(mockProfile);
//
//         // Act
//         var result = await _profileService.GetProfileById(existingId);
//
//         // Assert
//         Assert.NotNull(result);
//         Assert.Equal(existingId, result.Id);
//         Assert.Equal(mockProfile.UserId, result.UserId);
//         Assert.Equal(mockProfile.Name, result.Name);
//         Assert.Equal(mockProfile.Surname, result.Surname);
//     }
//
//     [Fact]
//     public async Task GetProfileById_NonExistingId_ShouldThrowException()
//     {
//         // Arrange
//         var nonExistingId = Guid.NewGuid();
//         _repositoryMock.Setup(repo => repo.GetByIdAsync<CandidateProfile>(nonExistingId)).ReturnsAsync((CandidateProfile)null!);
//
//         // Act and Assert
//         await Assert.ThrowsAsync<ExceptionWithStatusCode>(() => _profileService.GetProfileById(nonExistingId));
//     }
//
//     [Fact]
//     public async Task CreateProfile_ValidDto_ShouldCreateAndReturnProfile()
//     {
//         // Arrange
//         var profileCreateDto = new ProfileCreateDto
//         {
//             UserId = Guid.NewGuid(),
//             Name = "New",
//             Surname = "Profile",
//             PositionTitle = "Developer",
//             // ... other properties
//         };
//         var mockProfile = new CandidateProfile { Id = Guid.NewGuid(), UserId = profileCreateDto.UserId, Name = profileCreateDto.Name, Surname = profileCreateDto.Surname };
//         _createValidatorMock.Setup(validator => validator.ValidateAsync(profileCreateDto, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
//
//         _mapperMock.Setup(mapper => mapper.Map<CandidateProfile>(profileCreateDto)).Returns(mockProfile);
//         _repositoryMock.Setup(repo => repo.CreateAsync(mockProfile)).ReturnsAsync(mockProfile);
//
//         // Act
//         var result = await _profileService.CreateProfile(profileCreateDto);
//
//         // Assert
//         Assert.NotNull(result);
//         Assert.Equal(profileCreateDto.UserId, result.UserId);
//         Assert.Equal(profileCreateDto.Name, result.Name);
//         Assert.Equal(profileCreateDto.Surname, result.Surname);
//     }
//
//     [Fact]
//     public async Task CreateProfile_InvalidDto_ShouldThrowValidationException()
//     {
//         // Arrange
//         var profileCreateDto = new ProfileCreateDto
//         {
//             UserId = Guid.NewGuid(),
//             Name = "New",
//             Surname = "Profile",
//             PositionTitle = "Developer",
//             Email = null, // Email is required
//         };
//         var validationFailures = new List<FluentValidation.Results.ValidationFailure> { new FluentValidation.Results.ValidationFailure("Email", "Email is required") };
//         _createValidatorMock.Setup(validator => validator.ValidateAsync(profileCreateDto, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult(validationFailures));
//
//         // Act and Assert
//         await Assert.ThrowsAsync<ValidationException>(() => _profileService.CreateProfile(profileCreateDto));
//     }
//
//         [Fact]
//     public async Task UpdateProfile_ValidDtoAndExistingProfile_ShouldUpdateAndReturnProfile()
//     {
//         // Arrange
//         var profileUpdateDto = new ProfileUpdateDto
//         {
//             Id = Guid.NewGuid(),
//             Name = "Updated",
//             Surname = "Profile",
//             PositionTitle = "Senior Developer",
//             // ... other properties
//         };
//         
//         var mappedProfile = new CandidateProfile {
//             Id = profileUpdateDto.Id,
//             Name = "Updated",
//             Surname = "Profile",
//             PositionTitle = "Senior Developer",
//             // ... other properties
//         };
//         _updateValidatorMock.Setup(validator => validator.ValidateAsync(profileUpdateDto, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
//
//         _mapperMock.Setup(mapper => mapper.Map<CandidateProfile>(profileUpdateDto)).Returns(mappedProfile);
//         _repositoryMock.Setup(repo => repo.AnyAsync<CandidateProfile>(x => x.Id == mappedProfile.Id)).ReturnsAsync(true);
//         _repositoryMock.Setup(repo => repo.Update(mappedProfile)).Returns(mappedProfile);
//
//         // Act
//         var result = await _profileService.UpdateProfile(profileUpdateDto);
//
//         // Assert
//         Assert.NotNull(result);
//         Assert.Equal(profileUpdateDto.Id, result.Id);
//         Assert.Equal(profileUpdateDto.Name, result.Name);
//         Assert.Equal(profileUpdateDto.Surname, result.Surname);
//     }
//
//     [Fact]
//     public async Task UpdateProfile_InvalidDto_ShouldThrowValidationException()
//     {
//         // Arrange
//         var profileUpdateDto = new ProfileUpdateDto
//         {
//             Id = Guid.NewGuid(),
//             Name = "Updated",
//             Surname = "Profile",
//             PositionTitle = "Senior Developer",
//             Email = null, // Email is required
//             // ... other properties
//         };
//         var validationFailures = new List<FluentValidation.Results.ValidationFailure> { new FluentValidation.Results.ValidationFailure("Email", "Email is required") };
//         _updateValidatorMock.Setup(validator => validator.ValidateAsync(profileUpdateDto, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult(validationFailures));
//
//         // Act and Assert
//         await Assert.ThrowsAsync<ValidationException>(() => _profileService.UpdateProfile(profileUpdateDto));
//     }
//
//     [Fact]
//     public async Task UpdateProfile_NonExistingProfile_ShouldThrowException()
//     {
//         // Arrange
//         var profileUpdateDto = new ProfileUpdateDto
//         {
//             Id = Guid.NewGuid(),
//             Name = "Updated",
//             Surname = "Profile",
//             PositionTitle = "Senior Developer",
//             // ... other properties
//         };
//         _updateValidatorMock.Setup(validator => validator.ValidateAsync(profileUpdateDto, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
//         _repositoryMock.Setup(repo => repo.AnyAsync<CandidateProfile>(x => x.Id == profileUpdateDto.Id)).ReturnsAsync(false);
//
//         // Act and Assert
//         await Assert.ThrowsAsync<ExceptionWithStatusCode>(() => _profileService.UpdateProfile(profileUpdateDto));
//     }
//
//     [Fact]
//     public async Task DeleteProfile_ExistingId_ShouldDeleteProfile()
//     {
//         // Arrange
//         var existingId = Guid.NewGuid();
//         var mockProfile = new CandidateProfile { Id = existingId, UserId = Guid.NewGuid(), Name = "Test", Surname = "Profile" };
//         _repositoryMock.Setup(repo => repo.GetByIdAsync<CandidateProfile>(existingId)).ReturnsAsync(mockProfile);
//
//         // Act
//         await _profileService.DeleteProfile(existingId);
//
//         // Assert
//         _repositoryMock.Verify(repo => repo.Delete(mockProfile), Times.Once);
//         _repositoryMock.Verify(repo => repo.SaveChangesAsync(CancellationToken.None), Times.Once);
//     }
//
//     [Fact]
//     public async Task DeleteProfile_NonExistingId_ShouldThrowException()
//     {
//         // Arrange
//         var nonExistingId = Guid.NewGuid();
//         _repositoryMock.Setup(repo => repo.GetByIdAsync<CandidateProfile>(nonExistingId)).ReturnsAsync((CandidateProfile)null);
//
//         // Act and Assert
//         await Assert.ThrowsAsync<ExceptionWithStatusCode>(() => _profileService.DeleteProfile(nonExistingId));
//     }
// }