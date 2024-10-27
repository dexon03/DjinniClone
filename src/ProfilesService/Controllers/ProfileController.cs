using Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfilesService.Domain.Contracts;
using ProfilesService.Domain.DTO;

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
            return Ok(await _profileService.GetCandidateProfileByUserId(userId,cancellationToken));
        }
        if (role == Role.Recruiter)
        {
            return Ok(await _profileService.GetRecruiterProfileByUserId(userId,cancellationToken));
        }
        return BadRequest();
    }
    
    [HttpGet("getRecruiter/{profileId}")]
    public async Task<IActionResult> GetRecruiterProfile(Guid profileId, CancellationToken cancellationToken)
    {
        return Ok(await _profileService.GetRecruiterProfile(profileId));
    }
    
    [HttpGet("getCandidate/{profileId}")]
    public async Task<IActionResult> GetCandidateProfile(Guid profileId, CancellationToken cancellationToken)
    {
        return Ok(await _profileService.GetCandidateProfile(profileId,cancellationToken));
    }

    [HttpGet("getCandidatesProfile")]
    public async Task<IActionResult> GetCandidatesProfiles([FromQuery]CandidateFilterParameters filter)
    {
        return Ok(await _profileService.GetAllCandidatesProfiles(filter));
    }
    
    [HttpPut("updateCandidate")]
    public async Task<IActionResult> UpdateCandidateProfile([FromBody]CandidateProfileUpdateDto profile, CancellationToken cancellationToken)
    {
        return Ok(await _profileService.UpdateCandidateProfile(profile, cancellationToken));
    }
    
    [HttpPut("uploadResume")]
    public async Task<IActionResult> UploadResume([FromForm] ResumeUploadDto resume, CancellationToken cancellationToken)
    {
        await _profileService.UploadResume(resume, cancellationToken);
        return Ok();
    }
    
    [HttpGet("downloadResume/{profileId}")]
    public async Task<IActionResult> DownloadResume(Guid profileId, CancellationToken cancellationToken)
    {
        var result = await _profileService.DownloadResume(profileId, cancellationToken);
        if (result is null)
        {
            return Ok();
        }
        string contentType = "application/pdf";
        return File(result, contentType);
    }
    
    [HttpPut("updateRecruiter")]
    public async Task<IActionResult> UpdateRecruiterProfile([FromBody]RecruiterProfileUpdateDto profile, CancellationToken cancellationToken)
    {
        return Ok(await _profileService.UpdateRecruiterProfile(profile, cancellationToken));
    }
}