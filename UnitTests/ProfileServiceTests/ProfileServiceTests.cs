using System.Linq.Expressions;
using AutoMapper;
using Core.Database;
using Core.Exceptions;
using Microsoft.EntityFrameworkCore;
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
        profileService = new ProfileService(mockRepository.Object, mockMapper.Object);
    }

    [Fact]
    public async Task GetProfileById_ExistingId_ShouldReturnProfile()
    {
        // Arrange
        var existingId = Guid.NewGuid();
        var profile = new CandidateProfile { Id = existingId, Name = "John" };

        mockRepository.Setup(r => r.GetByIdAsync<CandidateProfile>(existingId))
                      .ReturnsAsync(profile);

        // Act
        var result = await profileService.GetProfileById(existingId);

        // Assert
        Assert.Equal(profile, result);
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
        var candidateProfileUpdateDto = new CandidateProfileUpdateDto();
        var existingProfile = new CandidateProfile { Id = Guid.NewGuid() };

        mockRepository.Setup(r => r.AnyAsync<CandidateProfile>(It.IsAny<Expression<Func<CandidateProfile, bool>>>()))
                      .ReturnsAsync(true);
        mockRepository.Setup(r => r.Update(It.IsAny<CandidateProfile>()))
                      .Returns(existingProfile);

        // Act
        var result = await profileService.UpdateCandidateProfile(candidateProfileUpdateDto);

        // Assert
        Assert.Equal(existingProfile, result);
        mockRepository.Verify(r => r.Update(It.IsAny<CandidateProfile>()), Times.Once);
        mockRepository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateRecruiterProfile_ExistingProfile_ShouldUpdateProfile()
    {
        // Arrange
        var recruiterProfileUpdateDto = new RecruiterProfileUpdateDto();
        var existingProfile = new RecruiterProfile { Id = Guid.NewGuid() };

        mockRepository.Setup(r => r.GetByIdAsync<RecruiterProfile>(It.IsAny<Guid>()))
                      .ReturnsAsync(existingProfile);
        mockRepository.Setup(r => r.Update(It.IsAny<RecruiterProfile>()))
                      .Returns(existingProfile);

        // Act
        var result = await profileService.UpdateRecruiterProfile(recruiterProfileUpdateDto);

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
        await profileService.DeleteProfile(existingId);

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
        await profileService.ActivateDisactivateProfile(existingId);

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
        await Assert.ThrowsAsync<ExceptionWithStatusCode>(() => profileService.UpdateCandidateProfile(candidateProfileUpdateDto));
    }

    [Fact]
    public async Task UpdateRecruiterProfile_NonExistingProfile_ShouldThrowException()
    {
        // Arrange
        var recruiterProfileUpdateDto = new RecruiterProfileUpdateDto();

        mockRepository.Setup(r => r.GetByIdAsync<RecruiterProfile>(It.IsAny<Guid>()))
                      .ReturnsAsync((RecruiterProfile)null);

        // Act & Assert
        await Assert.ThrowsAsync<ExceptionWithStatusCode>(() => profileService.UpdateRecruiterProfile(recruiterProfileUpdateDto));
    }

    [Fact]
    public async Task DeleteProfile_NonExistingId_ShouldThrowException()
    {
        // Arrange
        var nonExistingId = Guid.NewGuid();

        mockRepository.Setup(r => r.GetByIdAsync<CandidateProfile>(nonExistingId))
                      .ReturnsAsync((CandidateProfile)null);

        // Act & Assert
        await Assert.ThrowsAsync<ExceptionWithStatusCode>(() => profileService.DeleteProfile(nonExistingId));
    }

    [Fact]
    public async Task ActivateDisactivateProfile_NonExistingId_ShouldThrowException()
    {
        // Arrange
        var nonExistingId = Guid.NewGuid();

        mockRepository.Setup(r => r.GetByIdAsync<CandidateProfile>(nonExistingId))
                      .ReturnsAsync((CandidateProfile)null);

        // Act & Assert
        await Assert.ThrowsAsync<ExceptionWithStatusCode>(() => profileService.ActivateDisactivateProfile(nonExistingId));
    }
}