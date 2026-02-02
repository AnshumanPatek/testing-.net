-- Create Admin User
-- Note: Replace the PasswordHash with a BCrypt hash of your desired password
-- You can generate one at: https://bcrypt-generator.com/
-- Default password 'admin123' hash: $2a$11$8Z8Z8Z8Z8Z8Z8Z8Z8Z8Z8uK1J1J1J1J1J1J1J1J1J1J1J1J1J1J1J

INSERT INTO Users (Id, Email, PasswordHash, FirstName, LastName, Role, IsActive, CreatedAt)
VALUES (
    NEWID(),
    'admin@gainatelie.com',
    '$2a$11$8Z8Z8Z8Z8Z8Z8Z8Z8Z8Z8uK1J1J1J1J1J1J1J1J1J1J1J1J1J1J1J', -- Change this!
    'Admin',
    'User',
    'Admin',
    1,
    GETUTCDATE()
);

-- Create Editor User
INSERT INTO Users (Id, Email, PasswordHash, FirstName, LastName, Role, IsActive, CreatedAt)
VALUES (
    NEWID(),
    'editor@gainatelie.com',
    '$2a$11$8Z8Z8Z8Z8Z8Z8Z8Z8Z8Z8uK1J1J1J1J1J1J1J1J1J1J1J1J1J1J1J', -- Change this!
    'Editor',
    'User',
    'Editor',
    1,
    GETUTCDATE()
);

-- Initialize single-row tables
INSERT INTO Navbar (Id, CTAText, CTAUrl) VALUES (1, 'Schedule a call', 'https://calendly.com');
INSERT INTO HeroSection (Id, Headline, Tagline, BackgroundType, IsDraft) 
VALUES (1, 'Welcome to Gainateliê', 'Architecture & Design Studio', 'Image', 0);
INSERT INTO YouTubeSection (Id, IsDraft) VALUES (1, 0);
INSERT INTO AboutSection (Id, Title, Content, IsDraft) 
VALUES (1, 'About Us', 'We are a creative architecture and design studio.', 0);
INSERT INTO WhyChooseUsSection (Id, Title, IsDraft) VALUES (1, 'Why Choose Us', 0);
INSERT INTO WhatWeDoSection (Id, Title, IsDraft) VALUES (1, 'What We Do', 0);
INSERT INTO Footer (Id, Email, Phone, Copyright) 
VALUES (1, 'contact@gainatelie.com', '+1 234 567 8900', '© 2026 Gainateliê');
