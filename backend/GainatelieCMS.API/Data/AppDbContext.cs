using Microsoft.EntityFrameworkCore;
using GainatelieCMS.API.Models;

namespace GainatelieCMS.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<MediaAsset> MediaAssets { get; set; }
    public DbSet<Navbar> Navbar { get; set; }
    public DbSet<HeroSection> HeroSection { get; set; }
    public DbSet<YouTubeSection> YouTubeSection { get; set; }
    public DbSet<AboutSection> AboutSection { get; set; }
    public DbSet<WhyChooseUsSection> WhyChooseUsSection { get; set; }
    public DbSet<WhyChooseUsItem> WhyChooseUsItems { get; set; }
    public DbSet<WhatWeDoSection> WhatWeDoSection { get; set; }
    public DbSet<WhatWeDoItem> WhatWeDoItems { get; set; }
    public DbSet<Footer> Footer { get; set; }
    public DbSet<Page> Pages { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectImage> ProjectImages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Single-row tables
        modelBuilder.Entity<Navbar>().HasCheckConstraint("CK_Navbar_SingleRow", "Id = 1");
        modelBuilder.Entity<HeroSection>().HasCheckConstraint("CK_HeroSection_SingleRow", "Id = 1");
        modelBuilder.Entity<YouTubeSection>().HasCheckConstraint("CK_YouTubeSection_SingleRow", "Id = 1");
        modelBuilder.Entity<AboutSection>().HasCheckConstraint("CK_AboutSection_SingleRow", "Id = 1");
        modelBuilder.Entity<WhyChooseUsSection>().HasCheckConstraint("CK_WhyChooseUsSection_SingleRow", "Id = 1");
        modelBuilder.Entity<WhatWeDoSection>().HasCheckConstraint("CK_WhatWeDoSection_SingleRow", "Id = 1");
        modelBuilder.Entity<Footer>().HasCheckConstraint("CK_Footer_SingleRow", "Id = 1");

        // Indexes
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<Page>().HasIndex(p => p.Slug).IsUnique();
        modelBuilder.Entity<Project>().HasIndex(p => p.Slug).IsUnique();
        modelBuilder.Entity<Project>().HasIndex(p => new { p.IsFeatured, p.FeaturedOrder });
    }
}
