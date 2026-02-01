# Gainateliê CMS - Backend API

.NET 8.0 Web API with SQL Server and S3 integration.

## Setup

1. Install .NET 8.0 SDK
2. Update connection string in `appsettings.json`
3. Run migrations:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

4. Configure AWS S3 credentials
5. Configure Google Analytics credentials

## Run

```bash
dotnet run
```

API will be available at `http://localhost:5000`

## Endpoints

### Public
- GET `/api/content/home` - Home page content
- GET `/api/content/navbar` - Navbar
- GET `/api/content/footer` - Footer
- GET `/api/projects` - Published projects
- GET `/api/projects/featured` - Featured projects
- GET `/api/projects/{slug}` - Project by slug

### Admin (requires authentication)
- POST `/api/auth/login` - Login
- GET `/api/analytics/dashboard` - Dashboard metrics
- GET `/api/projects/admin/all` - All projects
- POST `/api/projects` - Create project
- PUT `/api/projects/{id}` - Update project
- DELETE `/api/projects/{id}` - Delete project
- POST `/api/projects/{id}/publish` - Publish project
- POST `/api/media/upload` - Upload media
- PUT `/api/content/hero` - Update hero section
- POST `/api/content/publish/{section}` - Publish section
