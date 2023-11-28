using System.Linq.Expressions;
using AutoMapper;
using Core.Database;
using Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProfilesService.Application.Services;
using ProfilesService.Domain;
using ProfilesService.Domain.DTO;
using ProfilesService.Domain.Models;
using ValidationException = Core.Exceptions.ValidationException;

namespace UnitTests.ProfileServiceTests;

public class ProfileServiceTests
{
    private readonly Mock<IRepository> mockRepository;
    private readonly Mock<IMapper> mockMapper;
    private readonly ProfileService profileService;

    public ProfileServiceTests()
    {
        mockRepository = new Mock<IRepository>();
        mockMapper = new Mock<IMapper>();
        profileService = new ProfileService(mockRepository.Object);
    }

    [Fact]
    public async Task GetProfileById_ExistingId_ShouldReturnProfile()
    {
        // Arrange
        var existingId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var profile = new CandidateProfile
        {
            Id = existingId, 
            UserId = userId,
            Name = "John",
            Surname = "Doe",
            DateBirth = new DateOnly(1988,12,14),
        };

        mockRepository.Setup(r => r.FirstOrDefaultAsync<CandidateProfile>(It.IsAny<Expression<Func<CandidateProfile, bool>>>()))
                      .ReturnsAsync(profile);

        // Act
        var result = await profileService.GetProfile<CandidateProfile>(userId);
        var expected = profile.ToDto();

        var expectedStr = JsonConvert.SerializeObject(expected);
        var resultStr = JsonConvert.SerializeObject(result);
        Assert.Equal(expectedStr, resultStr);
        // Assert
        
    }

    [Fact]
    public async Task CreateProfile_CandidateRole_ShouldCreateCandidateProfile()
    {
        // Arrange
        var profileCreateDto = new ProfileCreateDto { Role = ProfileRole.Candidate };
        var createdProfile = new CandidateProfile();

        mockRepository.Setup(r => r.CreateAsync(It.IsAny<CandidateProfile>()))
                      .ReturnsAsync(createdProfile);

        // Act
        await profileService.CreateProfile(profileCreateDto);

        // Assert
        mockRepository.Verify(r => r.CreateAsync(It.IsAny<CandidateProfile>()), Times.Once);
        mockRepository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CreateProfile_RecruiterRole_ShouldCreateRecruiterProfile()
    {
        // Arrange
        var profileCreateDto = new ProfileCreateDto { Role = ProfileRole.Recruiter };
        var createdProfile = new RecruiterProfile();

        mockRepository.Setup(r => r.CreateAsync(It.IsAny<RecruiterProfile>()))
                      .ReturnsAsync(createdProfile);

        // Act
        await profileService.CreateProfile(profileCreateDto);

        // Assert
        mockRepository.Verify(r => r.CreateAsync(It.IsAny<RecruiterProfile>()), Times.Once);
        mockRepository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateCandidateProfile_ExistingProfile_ShouldUpdateProfile()
    {
        // Arrange
        var existingProfileId = Guid.NewGuid();
        var candidateProfileUpdateDto = new CandidateProfileUpdateDto{ Id = existingProfileId };
        var existingProfile = new CandidateProfile { Id = existingProfileId };

        mockRepository.Setup(r => r.GetByIdAsync<CandidateProfile>(existingProfileId))
                      .ReturnsAsync(existingProfile);
        mockRepository.Setup(r => r.Update(It.IsAny<CandidateProfile>()))
                      .Returns(existingProfile);

        // Act
        var result = await profileService.UpdateProfile(candidateProfileUpdateDto);

        // Assert
        Assert.Equal(existingProfile, result);
        mockRepository.Verify(r => r.Update(It.IsAny<CandidateProfile>()), Times.Once);
        mockRepository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateRecruiterProfile_ExistingProfile_ShouldUpdateProfile()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var profileId = Guid.NewGuid();
        var recruiterProfileUpdateDto = new RecruiterProfileUpdateDto { Id = profileId };
        var existingProfile = new RecruiterProfile { Id = Guid.NewGuid(), UserId = userId};

        mockRepository.Setup(r => r.GetByIdAsync<RecruiterProfile>(profileId))
            .ReturnsAsync(existingProfile);
        mockRepository.Setup(r => r.Update(It.IsAny<RecruiterProfile>()))
                      .Returns(existingProfile);

        // Act
        var result = await profileService.UpdateProfile(recruiterProfileUpdateDto);

        // Assert
        Assert.Equal(existingProfile, result);
        mockRepository.Verify(r => r.Update(It.IsAny<RecruiterProfile>()), Times.Once);
        mockRepository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteProfile_ExistingId_ShouldDeleteProfile()
    {
        // Arrange
        var existingId = Guid.NewGuid();
        var existingProfile = new CandidateProfile { Id = existingId };

        mockRepository.Setup(r => r.GetByIdAsync<CandidateProfile>(existingId))
                      .ReturnsAsync(existingProfile);

        // Act
        await profileService.DeleteProfile<CandidateProfile>(existingId);

        // Assert
        mockRepository.Verify(r => r.Delete(existingProfile), Times.Once);
        mockRepository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ActivateDisactivateProfile_ExistingId_ShouldToggleIsActive()
    {
        // Arrange
        var existingId = Guid.NewGuid();
        var profile = new CandidateProfile { Id = existingId, IsActive = true };

        mockRepository.Setup(r => r.GetByIdAsync<CandidateProfile>(existingId))
                      .ReturnsAsync(profile);

        // Act
        await profileService.ActivateDisactivateProfile<CandidateProfile>(existingId);

        // Assert
        Assert.False(profile.IsActive);
        mockRepository.Verify(r => r.Update(profile), Times.Once);
        mockRepository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
     [Fact]
    public async Task UpdateCandidateProfile_NonExistingProfile_ShouldThrowException()
    {
        // Arrange
        var candidateProfileUpdateDto = new CandidateProfileUpdateDto();

        mockRepository.Setup(r => r.AnyAsync<CandidateProfile>(It.IsAny<Expression<Func<CandidateProfile, bool>>>()))
                      .ReturnsAsync(false);

        // Act & Assert
        await Assert.ThrowsAsync<ExceptionWithStatusCode>(() => profileService.UpdateProfile(candidateProfileUpdateDto));
    }

    [Fact]
    public async Task UpdateRecruiterProfile_NonExistingProfile_ShouldThrowException()
    {
        // Arrange
        var recruiterProfileUpdateDto = new RecruiterProfileUpdateDto();

        mockRepository.Setup(r => r.GetByIdAsync<RecruiterProfile>(It.IsAny<Guid>()))
                      .ReturnsAsync((RecruiterProfile)null);

        // Act & Assert
        await Assert.ThrowsAsync<ExceptionWithStatusCode>(() => profileService.UpdateProfile(recruiterProfileUpdateDto));
    }

    [Fact]
    public async Task DeleteProfile_NonExistingId_ShouldThrowException()
    {
        // Arrange
        var nonExistingId = Guid.NewGuid();

        mockRepository.Setup(r => r.GetByIdAsync<CandidateProfile>(nonExistingId))
                      .ReturnsAsync((CandidateProfile)null);

        // Act & Assert
        await Assert.ThrowsAsync<ExceptionWithStatusCode>(() => profileService.DeleteProfile<CandidateProfile>(nonExistingId));
    }

    [Fact]
    public async Task ActivateDisactivateProfile_NonExistingId_ShouldThrowException()
    {
        // Arrange
        var nonExistingId = Guid.NewGuid();

        mockRepository.Setup(r => r.GetByIdAsync<CandidateProfile>(nonExistingId))
                      .ReturnsAsync((CandidateProfile)null);

        // Act & Assert
        await Assert.ThrowsAsync<ExceptionWithStatusCode>(() => profileService.ActivateDisactivateProfile<CandidateProfile>(nonExistingId));
    }
}