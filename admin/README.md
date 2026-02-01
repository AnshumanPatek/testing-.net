# Gainateliê - Admin Panel

Next.js 14 admin interface for content management.

## Setup

```bash
npm install
cp .env.local.example .env.local
```

Update `.env.local` with your API URL.

## Development

```bash
npm run dev
```

Open http://localhost:3001

## Features

- Role-based access control (Admin, CRM, Editor)
- Google Analytics dashboard
- Real-time content editor
- Media library with S3 upload
- Project management with draft/publish workflow
- Block-based page constructor

## User Roles

- **Admin**: Full access to all features
- **CRM**: Access to analytics and dashboard
- **Editor**: Access to content editing and projects
