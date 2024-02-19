using ProfilesService.Domain.Enums;

namespace ProfilesService.Domain.DTO;

public record CandidateFilterParameters
{
    public string? searchTerm { get; set; }
    public int page { get; set; }
    public int pageSize { get; set; }
    public Experience? experience { get; set; }
    public AttendanceMode? attendanceMode { get; set; }
    public Guid? skill { get; set; }
    public Guid? location { get; set; }
}