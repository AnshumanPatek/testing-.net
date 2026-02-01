using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GainatelieCMS.API.Services;

namespace GainatelieCMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,CRM")]
public class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsService _analyticsService;

    public AnalyticsController(IAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboardMetrics()
    {
        var metrics = await _analyticsService.GetDashboardMetricsAsync();
        return Ok(metrics);
    }
}
