using GainatelieCMS.API.DTOs;

namespace GainatelieCMS.API.Services;

public interface IAnalyticsService
{
    Task<DashboardMetricsDto> GetDashboardMetricsAsync();
}
