using Microsoft.EntityFrameworkCore;
using GainatelieCMS.API.Data;
using GainatelieCMS.API.DTOs;

namespace GainatelieCMS.API.Services;

public class ContentService : IContentService
{
    private readonly AppDbContext _context;

    public ContentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<HomeContentDto> GetPublishedHomeContentAsync()
    {
        var hero = await _context.HeroSection.Include(h => h.Background).FirstOrDefaultAsync(h => h.Id == 1 && !h.IsDraft);
        var youtube = await _context.YouTubeSection.FirstOrDefaultAsync(y => y.Id == 1 && !y.IsDraft);
        var about = await _context.AboutSection.Include(a => a.Image).FirstOrDefaultAsync(a => a.Id == 1 && !a.IsDraft);
        var whyChooseUs = await _context.WhyChooseUsSection.FirstOrDefaultAsync(w => w.Id == 1 && !w.IsDraft);
        var whyChooseUsItems = await _context.WhyChooseUsItems.Include(i => i.Icon).OrderBy(i => i.SortOrder).ToListAsync();
        var whatWeDo = await _context.WhatWeDoSection.FirstOrDefaultAsync(w => w.Id == 1 && !w.IsDraft);
        var whatWeDoItems = await _context.WhatWeDoItems.Include(i => i.Media).OrderBy(i => i.SortOrder).ToListAsync();

        return new HomeContentDto
        {
            Hero = hero != null ? new HeroSectionDto
            {
                Headline = hero.Headline,
                Tagline = hero.Tagline,
                BackgroundType = hero.BackgroundType,
                BackgroundUrl = hero.Background?.S3Url
            } : null,
            YouTube = youtube != null ? new YouTubeSectionDto
            {
                VideoUrl = youtube.VideoUrl,
                Title = youtube.Title
            } : null,
            About = about != null ? new AboutSectionDto
            {
                Title = about.Title,
                Content = about.Content,
                ImageUrl = about.Image?.S3Url
            } : null
        };
    }

    public async Task<NavbarDto> GetNavbarAsync()
    {
        var navbar = await _context.Navbar.Include(n => n.Logo).FirstOrDefaultAsync(n => n.Id == 1);
        
        return new NavbarDto
        {
            LogoUrl = navbar?.Logo?.S3Url,
            CTAText = navbar?.CTAText ?? "Schedule a call",
            CTAUrl = navbar?.CTAUrl
        };
    }

    public async Task<FooterDto> GetFooterAsync()
    {
        var footer = await _context.Footer.FirstOrDefaultAsync(f => f.Id == 1);
        
        return new FooterDto
        {
            Email = footer?.Email,
            Phone = footer?.Phone,
            Address = footer?.Address,
            Copyright = footer?.Copyright?.Replace("{year}", DateTime.UtcNow.Year.ToString()),
            Instagram = footer?.Instagram,
            LinkedIn = footer?.LinkedIn,
            Behance = footer?.Behance
        };
    }

    public async Task UpdateHeroSectionAsync(HeroSectionDto dto)
    {
        var hero = await _context.HeroSection.FirstOrDefaultAsync(h => h.Id == 1);
        
        if (hero == null)
        {
            hero = new Models.HeroSection { Id = 1 };
            _context.HeroSection.Add(hero);
        }

        hero.Headline = dto.Headline;
        hero.Tagline = dto.Tagline;
        hero.BackgroundType = dto.BackgroundType ?? "Image";
        hero.IsDraft = true;
        hero.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAboutSectionAsync(AboutSectionDto dto)
    {
        var about = await _context.AboutSection.FirstOrDefaultAsync(a => a.Id == 1);
        
        if (about == null)
        {
            about = new Models.AboutSection { Id = 1 };
            _context.AboutSection.Add(about);
        }

        about.Title = dto.Title;
        about.Content = dto.Content;
        about.IsDraft = true;
        about.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }

    public async Task PublishSectionAsync(string section)
    {
        switch (section.ToLower())
        {
            case "hero":
                var hero = await _context.HeroSection.FirstOrDefaultAsync(h => h.Id == 1);
                if (hero != null)
                {
                    hero.IsDraft = false;
                    hero.UpdatedAt = DateTime.UtcNow;
                }
                break;
            case "about":
                var about = await _context.AboutSection.FirstOrDefaultAsync(a => a.Id == 1);
                if (about != null)
                {
                    about.IsDraft = false;
                    about.UpdatedAt = DateTime.UtcNow;
                }
                break;
        }

        await _context.SaveChangesAsync();
    }
}
