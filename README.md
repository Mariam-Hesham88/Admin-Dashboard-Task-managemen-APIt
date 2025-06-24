# Admin Dashboard - Task Management System

A task management web application built for admins to manage departments, employees, and tasks efficiently.

---

## ✅ Tech Stack

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- AutoMapper
- FluentValidation
- SQL Server
- Swagger (OpenAPI)
- JWT Authentication & Authorization
- Clean N-Tier Architecture (API / BLL / DAL)

---

## 📁 Project Structure
Admin-dashboard/
├── DAL # Data Access Layer (DbContext, Entities, Repositories)
├── BLL # Business Logic Layer (Services, DTOs, AutoMapper, Validators)
└── Admin-dashboard # Web API (Controllers, Program.cs, JWT Config)


---

## ✨ Features

### Department Management
- Add / Update / Delete / Get All / Get by ID

### Employee Management
- Add / Update / Delete / Get All / Get by ID

### Task Management
- Assign tasks to employees
- Set priority, status, deadlines
- Filter by department or date
- Track status: *In Progress*, *Completed*, *Overdue*

### Admin Authentication
- Register / Login for Admins
- JWT Token generation and validation
- Secured routes with `[Authorize]` attribute

### Dashboard Endpoint (Analytics)
- Count of departments & employees
- Task stats: Completed, In Progress, Overdue
- Top department based on task volume

---

## Security

- JWT Bearer Token Authentication
- Authorization middleware
- BCrypt for password hashing
- DTO input validation via FluentValidation

---

## Upcoming Enhancements

- 📩 Email notifications for assigned tasks  
- 🔍 Search & filter endpoints  
- 📈 Real-time dashboard with SignalR  
- 🌐 Angular 17 Frontend Integration  
- 👨‍💻 Role-based authorization  

--
 **Author
Mariam Hesham
Full-Stack Developer "Angular +17, .NET"
Email: mariam.hesham.ramadan@gmail.com
LinkedIn: https://www.linkedin.com/in/mariam-hesham-88m
=======

## License
MIT License © 2025 — Mariam Hesham
