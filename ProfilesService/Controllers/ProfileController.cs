using Microsoft.AspNetCore.Mvc;
using ProfilesService.Domain.Contracts;
using ProfilesService.Domain.DTO;

namespace ProfilesService.Controllers;

public class ProfileController : BaseController
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProfileById(Guid id)
    {
        var result = await _profileService.GetProfileById(id);
        return Ok(result);
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

    [HttpPost]
    public async Task<IActionResult> CreateProfile(ProfileCreateDto profile)
    {
        var createdProfile = await _profileService.CreateProfile(profile);
        return Ok(createdProfile);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProfile(ProfileUpdateDto profile)
    {
        var updatedProfile = await _profileService.UpdateProfile(profile);
        return Ok(updatedProfile);
    }
}