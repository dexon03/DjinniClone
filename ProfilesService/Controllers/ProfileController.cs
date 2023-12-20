using Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfilesService.Domain;
using ProfilesService.Domain.Contracts;
using ProfilesService.Domain.DTO;
using ProfilesService.Domain.Models;

namespace ProfilesService.Controllers;

[Authorize]
public class ProfileController : BaseController
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpGet("{role}/{userId}")]
    public async Task<IActionResult> GetUserProfile(Guid userId, Role role, CancellationToken cancellationToken)
    {
        if (role == Role.Candidate)
        {
            var result = await _profileService.GetCandidateProfileByUserId(userId,cancellationToken);
            return Ok(result);
        }
        if (role == Role.Recruiter)
        {
            var result = await _profileService.GetRecruiterProfile(userId,cancellationToken);
            return Ok(result);
        }
        return BadRequest();
    }
    
    [HttpGet("getCandidate/{profileId}")]
    public async Task<IActionResult> GetCandidateProfile(Guid profileId, CancellationToken cancellationToken)
    {
        var result = await _profileService.GetCandidateProfile(profileId,cancellationToken);
        return Ok(result);
    }

    [HttpGet("getCandidatesProfile")]
    public async Task<IActionResult> GetCandidatesProfiles()
    {
        var result = await _profileService.GetAllCandidatesProfiles();
        return Ok(result);
    }

    // [HttpDelete("{}{id}")]
    // public async Task<IActionResult> DeleteProfile(Guid id)
    // {
    //     await _profileService.DeleteProfile(id);
    //     return Ok();
    // }

    // [HttpPost]
    // public async Task<IActionResult> CreateProfile(ProfileCreateDto profile)
    // {
    //     var createdProfile = await _profileService.CreateProfile(profile);
    //     return Ok(createdProfile);
    // }

    [HttpPut("updateCandidate")]
    public async Task<IActionResult> UpdateCandidateProfile(CandidateProfileUpdateDto profile, CancellationToken cancellationToken)
    {
        var updatedProfile = await _profileService.UpdateCandidateProfile(profile, cancellationToken);
        return Ok(updatedProfile);
    }
    
    [HttpPut]
    public async Task<IActionResult> UploadResume([FromForm] ResumeUploadDto resume, CancellationToken cancellationToken)
    {
        await _profileService.UploadResume(resume, cancellationToken);
        return Ok();
    }
    
    [HttpPut("updateRecruiter")]
    public async Task<IActionResult> UpdateRecruiterProfile(RecruiterProfileUpdateDto profile, CancellationToken cancellationToken)
    {
        var updatedProfile = await _profileService.UpdateRecruiterProfile(profile, cancellationToken);
        return Ok(updatedProfile);
    }
}