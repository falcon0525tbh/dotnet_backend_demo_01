# TaskHub.Api

ASP.NET Core 6 Web API for a task management app (JWT auth + roles, task CRUD, dashboard metrics).

## Features
- User auth with JWT, roles: CEO/Manager/Worker
- Task CRUD, status/priority, assignee, due date
- Dashboard: status counts, due-soon list
- Meta enums endpoint for front-end (roles/status/priority)
- Role-based access control on task and user endpoints
- Pagination & filtering on task list

## Tech Stack
- .NET 6, ASP.NET Core Web API
- Entity Framework Core (SQL Server Express)
- JWT (Microsoft.IdentityModel.Tokens)
- Swagger/OpenAPI

## Quick Start
