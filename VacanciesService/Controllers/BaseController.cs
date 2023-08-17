using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VacanciesService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    [HttpGet("healthCheck")]
    public IActionResult HealthCheck()
    {
        return Ok();
    }
}