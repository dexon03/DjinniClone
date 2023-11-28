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
    public async Task<IActionResult> GetProfile(Guid userId, ProfileRole role)
    {
        if (role == ProfileRole.Candidate)
        {
            var result = await _profileService.GetProfile<CandidateProfile>(userId);
            return Ok(result);
        }
        return Ok(await _profileService.GetProfile<RecruiterProfile>(userId));
    }

    [HttpGet]
    public async Task<IActionResult> GetProfiles()
    {
        var result = await _profileService.GetAllProfiles();
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
    public async Task<IActionResult> UpdateCandidateProfile(CandidateProfileUpdateDto profile)
    {
        var updatedProfile = await _profileService.UpdateProfile(profile);
        return Ok(updatedProfile);
    }
    
    [HttpPut("updateRecruiter")]
    public async Task<IActionResult> UpdateRecruiterProfile(RecruiterProfileUpdateDto profile)
    {
        var updatedProfile = await _profileService.UpdateProfile(profile);
        return Ok(updatedProfile);
    }
}