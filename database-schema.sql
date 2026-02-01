-- =============================================
-- Gainateliê CMS - Database Schema
-- Tech Stack: .NET + SQL Server + S3
-- =============================================

-- =============================================
-- 1. Users (with inline Role)
-- =============================================
CREATE TABLE Users (
    Id              UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
    Email           NVARCHAR(255) NOT NULL UNIQUE,
    PasswordHash    NVARCHAR(512) NOT NULL,
    FirstName       NVARCHAR(100),
    LastName        NVARCHAR(100),
    Role            NVARCHAR(20) NOT NULL DEFAULT 'Editor',  -- 'Admin', 'CRM', 'Editor'
    IsActive        BIT DEFAULT 1,
    CreatedAt       DATETIME2 DEFAULT GETUTCDATE()
);

-- =============================================
-- 2. Media Assets (S3)
-- =============================================
CREATE TABLE MediaAssets (
    Id          UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
    FileName    NVARCHAR(255) NOT NULL,
    S3Key       NVARCHAR(500) NOT NULL,
    S3Url       NVARCHAR(500) NOT NULL,
    MimeType    NVARCHAR(100) NOT NULL,
    AssetType   NVARCHAR(20) NOT NULL,  -- 'Image', 'Video', 'GIF'
    CreatedAt   DATETIME2 DEFAULT GETUTCDATE()
);

-- =============================================
-- 3. Fixed Content Tables
-- =============================================

-- Navbar
CREATE TABLE Navbar (
    Id              INT PRIMARY KEY DEFAULT 1,  -- Single row
    LogoId          UNIQUEIDENTIFIER REFERENCES MediaAssets(Id),
    CTAText         NVARCHAR(100) DEFAULT 'Schedule a call',
    CTAUrl          NVARCHAR(500),
    UpdatedAt       DATETIME2 DEFAULT GETUTCDATE(),
    CHECK (Id = 1)  -- Ensures only one row
);

-- Hero Section
CREATE TABLE HeroSection (
    Id              INT PRIMARY KEY DEFAULT 1,
    Headline        NVARCHAR(255),
    Tagline         NVARCHAR(500),
    BackgroundType  NVARCHAR(10) DEFAULT 'Image',  -- 'Image' or 'Video'
    BackgroundId    UNIQUEIDENTIFIER REFERENCES MediaAssets(Id),
    IsDraft         BIT DEFAULT 1,
    UpdatedAt       DATETIME2 DEFAULT GETUTCDATE(),
    CHECK (Id = 1)
);

-- YouTube Section
CREATE TABLE YouTubeSection (
    Id          INT PRIMARY KEY DEFAULT 1,
    VideoUrl    NVARCHAR(500),
    Title       NVARCHAR(255),
    IsDraft     BIT DEFAULT 1,
    UpdatedAt   DATETIME2 DEFAULT GETUTCDATE(),
    CHECK (Id = 1)
);

-- About Section
CREATE TABLE AboutSection (
    Id          INT PRIMARY KEY DEFAULT 1,
    Title       NVARCHAR(255),
    Content     NVARCHAR(MAX),
    ImageId     UNIQUEIDENTIFIER REFERENCES MediaAssets(Id),
    IsDraft     BIT DEFAULT 1,
    UpdatedAt   DATETIME2 DEFAULT GETUTCDATE(),
    CHECK (Id = 1)
);

-- Why Choose Us Section
CREATE TABLE WhyChooseUsSection (
    Id          INT PRIMARY KEY DEFAULT 1,
    Title       NVARCHAR(255),
    IsDraft     BIT DEFAULT 1,
    UpdatedAt   DATETIME2 DEFAULT GETUTCDATE(),
    CHECK (Id = 1)
);

CREATE TABLE WhyChooseUsItems (
    Id          UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
    Title       NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    IconId      UNIQUEIDENTIFIER REFERENCES MediaAssets(Id),
    SortOrder   INT DEFAULT 0
);

-- What We Do Section
CREATE TABLE WhatWeDoSection (
    Id          INT PRIMARY KEY DEFAULT 1,
    Title       NVARCHAR(255),
    IsDraft     BIT DEFAULT 1,
    UpdatedAt   DATETIME2 DEFAULT GETUTCDATE(),
    CHECK (Id = 1)
);

CREATE TABLE WhatWeDoItems (
    Id          UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
    Title       NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    MediaId     UNIQUEIDENTIFIER REFERENCES MediaAssets(Id),
    MediaType   NVARCHAR(20),  -- 'Image', 'Video', 'Icon'
    SortOrder   INT DEFAULT 0
);

-- Footer
CREATE TABLE Footer (
    Id              INT PRIMARY KEY DEFAULT 1,
    Email           NVARCHAR(255),
    Phone           NVARCHAR(50),
    Address         NVARCHAR(500),
    Copyright       NVARCHAR(255) DEFAULT '© {year} Gainateliê',
    Instagram       NVARCHAR(500),
    LinkedIn        NVARCHAR(500),
    Behance         NVARCHAR(500),
    UpdatedAt       DATETIME2 DEFAULT GETUTCDATE(),
    CHECK (Id = 1)
);

-- =============================================
-- 4. Static Pages (with versioning)
-- =============================================
CREATE TABLE Pages (
    Id                  UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
    Slug                NVARCHAR(200) NOT NULL UNIQUE,  -- '404', 'privacy-policy'
    Title               NVARCHAR(255) NOT NULL,
    MetaDescription     NVARCHAR(500),
    -- Draft version
    DraftContent        NVARCHAR(MAX),
    DraftUpdatedAt      DATETIME2,
    -- Published version
    PublishedContent    NVARCHAR(MAX),
    PublishedAt         DATETIME2,
    CreatedAt           DATETIME2 DEFAULT GETUTCDATE()
);

-- =============================================
-- 5. Projects (Case Studies)
-- =============================================
CREATE TABLE Projects (
    Id              UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
    Slug            NVARCHAR(200) NOT NULL UNIQUE,
    Title           NVARCHAR(255) NOT NULL,
    BrandName       NVARCHAR(100),
    BrandLogoId     UNIQUEIDENTIFIER REFERENCES MediaAssets(Id),
    ThumbnailId     UNIQUEIDENTIFIER REFERENCES MediaAssets(Id),
    IsFeatured      BIT DEFAULT 0,
    FeaturedOrder   INT,
    -- Details (Brand, Geography, Category, etc.)
    DetailsJson     NVARCHAR(MAX),  -- [{"label": "Brand", "value": "Erewhon"}]
    -- Highlights (bullet points)
    HighlightsJson  NVARCHAR(MAX),  -- ["Point 1", "Point 2"]
    -- Description paragraphs
    DescriptionHtml NVARCHAR(MAX),
    IsPublished     BIT DEFAULT 0,
    CreatedAt       DATETIME2 DEFAULT GETUTCDATE(),
    UpdatedAt       DATETIME2 DEFAULT GETUTCDATE()
);

CREATE TABLE ProjectImages (
    Id          UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
    ProjectId   UNIQUEIDENTIFIER REFERENCES Projects(Id) ON DELETE CASCADE,
    AssetId     UNIQUEIDENTIFIER REFERENCES MediaAssets(Id),
    Caption     NVARCHAR(255),
    SortOrder   INT DEFAULT 0
);

-- =============================================
-- Indexes for Performance
-- =============================================
CREATE INDEX IX_Users_Email ON Users(Email);
CREATE INDEX IX_Users_Role ON Users(Role);
CREATE INDEX IX_MediaAssets_AssetType ON MediaAssets(AssetType);
CREATE INDEX IX_Pages_Slug ON Pages(Slug);
CREATE INDEX IX_Projects_Slug ON Projects(Slug);
CREATE INDEX IX_Projects_IsFeatured ON Projects(IsFeatured, FeaturedOrder);
CREATE INDEX IX_Projects_IsPublished ON Projects(IsPublished);
CREATE INDEX IX_ProjectImages_ProjectId ON ProjectImages(ProjectId, SortOrder);
CREATE INDEX IX_WhyChooseUsItems_SortOrder ON WhyChooseUsItems(SortOrder);
CREATE INDEX IX_WhatWeDoItems_SortOrder ON WhatWeDoItems(SortOrder);
