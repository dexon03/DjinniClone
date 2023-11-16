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

    public ProfileController(IProfileService profileService, HttpContextAccessor httpContextAccessor)
    {
        _profileService = profileService;
    }

    [HttpGet("{role}/{id}")]
    public async Task<IActionResult> GetProfile(Guid id, ProfileRole role)
    {
        if (role == ProfileRole.Candidate)
        {
            return Ok(await _profileService.GetProfile<CandidateProfile>(id));
        }
        return Ok(await _profileService.GetProfile<RecruiterProfile>(id));
    }

    [HttpGet]
    public async Task<IActionResult> GetProfiles()
    {
        var result = await _profileService.GetAllProfiles();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProfile(Guid id)
    {
        await _profileService.DeleteProfile(id);
        return Ok();
    }

    // [HttpPost]
    // public async Task<IActionResult> CreateProfile(ProfileCreateDto profile)
    // {
    //     var createdProfile = await _profileService.CreateProfile(profile);
    //     return Ok(createdProfile);
    // }

    [HttpPut]
    public async Task<IActionResult> UpdateCandidateProfile(CandidateProfileUpdateDto profile)
    {
        var updatedProfile = await _profileService.UpdateCandidateProfile(profile);
        return Ok(updatedProfile);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateRecruiterProfile(RecruiterProfileUpdateDto profile)
    {
        var updatedProfile = await _profileService.UpdateRecruiterProfile(profile);
        return Ok(updatedProfile);
    }
}