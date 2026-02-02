using GainatelieCMS.API.DTOs;

namespace GainatelieCMS.API.Services;

public interface IContentService
{
    Task<HomeContentDto> GetPublishedHomeContentAsync();
    Task<NavbarDto> GetNavbarAsync();
    Task<FooterDto> GetFooterAsync();
    Task<HeroSectionDto> GetHeroSectionAsync();
    Task<AboutSectionDto> GetAboutSectionAsync();
    Task<YouTubeSectionDto> GetYouTubeSectionAsync();
    Task UpdateNavbarAsync(NavbarDto dto);
    Task UpdateHeroSectionAsync(HeroSectionDto dto);
    Task UpdateAboutSectionAsync(AboutSectionDto dto);
    Task UpdateYouTubeSectionAsync(YouTubeSectionDto dto);
    Task UpdateFooterAsync(FooterDto dto);
    Task PublishSectionAsync(string section);
}
