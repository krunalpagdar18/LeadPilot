# Backend Architecture & Security

![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-8.0-512BD4?style=flat&logo=dotnet&logoColor=white)
![Entity Framework Core](https://img.shields.io/badge/EF%20Core-8.0-512BD4?style=flat&logo=dotnet&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-4169E1?style=flat&logo=postgresql&logoColor=white)
![Security](https://img.shields.io/badge/Security-PBKDF2--SHA256-success?style=flat)

This document covers the database configuration, authentication security details, and API endpoint structure of the LeadPilot backend.

---

## 1. Database Mapping (EF Core DB-First)
LeadPilot maps to a PostgreSQL database using EF Core Database-First generation. Major entities include:
* **User**: Represents CRM agents. Stores usernames, emails, and cryptographically hashed passwords.
* **Lead**: Main record holding the client company details, email addresses, source tracking, creation date, status flags, and ownership reference (`UserId`).
* **LeadSource**: Dictionary lookup containing the external platforms where the lead was discovered (e.g., Google Maps, Hotfrog, etc.).
* **LeadStatus**: Workflow status metadata.

All queries are scoped to the authenticated user ID (`UserId`) to prevent unauthorized cross-tenant data leaks.

---

## 2. Authentication & Cryptography
To prevent account compromise and timing attacks, LeadPilot does not use simple hashing or standard BCrypt defaults, but utilizes a secure, custom **PBKDF2** implementation:
* **Algorithm**: `Rfc2898DeriveBytes` using `SHA-256`.
* **Salt Size**: 128-bit random salt generated per-user.
* **Key Size**: 256-bit key output.
* **Work Factor**: 100,000 iterations.
* **Verification**: Verifies hashed passwords using `CryptographicOperations.FixedTimeEquals` for constant-time comparisons, eliminating timing attack vector vulnerabilities.

---

## 3. Endpoint Mapping

### `HomeController` (Authentication Guarded)
* `GET /Home/Index`: Renders the dashboard view.
* `GET /Home/GetDashboardStats`: Serves JSON stats mapping the active status counters, conversion rates, and weekly velocity.

### `LeadController`
* **User-Facing Endpoints (Authentication Guarded)**:
  * `GET /Lead/Index`: Leads pipeline management page.
  * `POST /Lead/GetAllLeads`: Paginated server-side DataTable data reload. Expects search terms and page limits.
  * `GET /Lead/GetStatusCounts`: Fetches status badges count.
  * `PUT /Lead/UpdateLeadStatus?id={id}&status={status}`: Updates a lead's status.
  * `DELETE /Lead/DeleteLead?id={id}`: Soft-deletes a lead.
* **Automation-Facing Webhook Endpoints (Secret Header Authenticated - `[AllowAnonymous]`)**:
  * `POST /Lead/TriggerFollowup?Id={id}`: Triggers automated follow-up communication intervals.
  * `POST /Lead/GetLeadStatus?Id={id}`: Queries status of the lead.
  * `POST /Lead/MarkAsNotInterested?Id={id}`: Transitions lead status to not interested.

### `AccountController` (Anonymous Access Allowed)
* `GET/POST /Account/Login`: Logs in a user. Detects AJAX calls to return JSON metadata or HTML views.
* `GET/POST /Account/Register`: Registers and signs in a new user. Supports AJAX.
* `POST /Account/Logout`: Signs out the user session.
