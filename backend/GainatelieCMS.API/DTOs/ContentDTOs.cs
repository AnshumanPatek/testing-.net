namespace GainatelieCMS.API.DTOs;

public class HomeContentDto
{
    public HeroSectionDto? Hero { get; set; }
    public YouTubeSectionDto? YouTube { get; set; }
    public AboutSectionDto? About { get; set; }
}

public class NavbarDto
{
    public string? LogoUrl { get; set; }
    public string CTAText { get; set; } = "Schedule a call";
    public string? CTAUrl { get; set; }
}

public class HeroSectionDto
{
    public string? Headline { get; set; }
    public string? Tagline { get; set; }
    public string? BackgroundType { get; set; }
    public string? BackgroundUrl { get; set; }
}

public class YouTubeSectionDto
{
    public string? VideoUrl { get; set; }
    public string? Title { get; set; }
}

public class AboutSectionDto
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? ImageUrl { get; set; }
}

public class FooterDto
{
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? Copyright { get; set; }
    public string? Instagram { get; set; }
    public string? LinkedIn { get; set; }
    public string? Behance { get; set; }
}
