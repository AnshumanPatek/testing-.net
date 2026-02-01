namespace GainatelieCMS.API.DTOs;

public class DashboardMetricsDto
{
    public int TrafficPerMonth { get; set; }
    public TrafficSourcesDto TrafficSources { get; set; } = new();
    public decimal BounceRate { get; set; }
    public decimal ConversionRate { get; set; }
    public int AverageSessionTime { get; set; }
    public DevicesAndBrowsersDto DevicesAndBrowsers { get; set; } = new();
}

public class TrafficSourcesDto
{
    public int SEO { get; set; }
    public int SocialMedia { get; set; }
    public int DirectTraffic { get; set; }
    public int Referrals { get; set; }
}

public class DevicesAndBrowsersDto
{
    public Dictionary<string, int> Devices { get; set; } = new();
    public Dictionary<string, int> Browsers { get; set; } = new();
}
