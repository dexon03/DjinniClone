using System.Web;
using Microsoft.AspNetCore.Mvc;
using VacanciesService.Domain.Contacts;

namespace VacanciesService.Controllers;

public class StatisticController : BaseController
{
    private readonly IStatisticService _statisticService;

    public StatisticController(IStatisticService statisticService)
    {
        _statisticService = statisticService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetStatistic([FromQuery]string? skillName)
    {
        var encode = HttpUtility.UrlEncode(skillName);
        return Ok(await _statisticService.GetStatisticAsync(encode));
    }
    
    [HttpGet("mocked")]
    public async Task<IActionResult> GetMockedStatistic()
    {
        return Ok(await _statisticService.GetMockedStatisticAsync());
    }
    
}