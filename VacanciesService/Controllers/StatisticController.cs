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
        var statistic = await _statisticService.GetStatisticAsync(encode);
        return Ok(statistic);
    }
    
    [HttpGet("mocked")]
    public async Task<IActionResult> GetMockedStatistic()
    {
        var statistic = await _statisticService.GetMockedStatisticAsync();
        return Ok(statistic);
    }
    
}