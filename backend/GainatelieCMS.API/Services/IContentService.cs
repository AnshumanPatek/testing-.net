using GainatelieCMS.API.DTOs;

namespace GainatelieCMS.API.Services;

public interface IContentService
{
    Task<HomeContentDto> GetPublishedHomeContentAsync();
    Task<NavbarDto> GetNavbarAsync();
    Task<FooterDto> GetFooterAsync();
    Task UpdateHeroSectionAsync(HeroSectionDto dto);
    Task UpdateAboutSectionAsync(AboutSectionDto dto);
    Task PublishSectionAsync(string section);
}
