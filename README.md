# TaskHub.Api

ASP.NET Core 6 Web API + Vite/Vue 3 front-end for a task management app (JWT auth + roles, tasks, projects, dashboard).

## Features (v2 highlights)

- User auth with JWT, roles: Admin / CEO / Manager / Worker
- Admin can create/manage users
- Projects: CEOs/Managers/Admin can create projects and assign multiple members; only members/creator/Admin can view
- Tasks: CRUD with status/priority/assignee/due date, optional project association; only creator/Admin can delete
- Visibility: tasks/projects filtered to assignees/members/creators (Admin sees all)
- Dashboard API: status counts plus top 5 highest-priority / soonest-due tasks
- Meta enums endpoint for front-end (roles/status/priority)
- Role-based access control on task, user, project endpoints
- Pagination & filtering on task list

## Tech Stack

- .NET 6, ASP.NET Core Web API
- Entity Framework Core (SQL Server Express)
- JWT (Microsoft.IdentityModel.Tokens)
- Swagger/OpenAPI
- Frontend: Vite + Vue 3 + Pinia + Vue Router

## Project Structure

```
TaskHub.Api/           # ASP.NET Core API
  Controllers/
  Models/
  Dtos/
  DbContext/
frontend/              # Vite/Vue front-end
  src/
```

## Frontend Dev

```bash
cd frontend
npm install
npm run dev   # default http://localhost:5173
```

API base URL is configured via `.env.development` (defaults to `http://localhost:5000/api`).

## Backend Dev

```bash
dotnet restore
dotnet run
```

Swagger: `http://localhost:5000/swagger`

## Run Order

1. backend: `dotnet build` && `dotnet run --urls "http://localhost:5000"`
2. frontend: `npm run dev`
