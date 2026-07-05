# LeadPilot

![.NET 8](https://img.shields.io/badge/.NET-8-512BD4?style=flat&logo=dotnet&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-4169E1?style=flat&logo=postgresql&logoColor=white)
![n8n](https://img.shields.io/badge/n8n-FF6C37?style=flat&logo=n8n&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-7952B3?style=flat&logo=bootstrap&logoColor=white)
![jQuery](https://img.shields.io/badge/jQuery-0769AD?style=flat&logo=jquery&logoColor=white)

LeadPilot is a modern, secure, and mobile-responsive CRM platform designed to streamline lead acquisition, pipeline tracking, and automated communications. Built using ASP.NET Core 8.0 MVC, Entity Framework Core, and powered by an automated **n8n** orchestration engine, it features a glassmorphic dark-theme interface tailored for both desktop grids and mobile card viewports.

---

## 🚀 Key Features

* **Pipeline Stages Management**: Track leads through customizable statuses (`New`, `Initial Sent`, `Follow-up Sent`, `Replied`, `Closed`, `Do Not Contact`).
* **Interactive Analytics Dashboard**: Real-time KPI counters (Total leads, weekly conversion velocity) and graphical data distributions (status and source breakdowns via Chart.js).
* **Dual Viewport Layout**: Traditional jQuery DataTables on desktop; auto-renders into glassmorphic swipable mobile cards on small viewports (<768px).
* **Automated Drip Follow-ups**: Zero-maintenance lead nurturing loops driven by n8n webhooks.
* **Timing-Attack Immune Auth**: Secure login and registration powered by PBKDF2 (SHA-256) with constant-time cryptographic verification.

---

## 📐 Architecture & Workflow Diagram

```mermaid
sequenceDiagram
    autonumber
    actor User as Sales Agent
    participant App as LeadPilot Web App
    participant DB as PostgreSQL
    participant SMTP as SMTP Relay
    participant n8n as n8n Automation Engine

    User->>App: Creates Lead (Form Submit)
    App->>DB: Save Lead record (Status: New)
    App->>SMTP: Send initial outreach email template
    SMTP-->>App: Success
    App->>n8n: Trigger Webhook (Payload: Lead ID)
    activate n8n
    Note over n8n: n8n workflow initiates 5-day wait
    deactivate n8n
    
    Note over App, n8n: 5 Days Pass
    
    n8n->>App: POST /Lead/TriggerFollowup?Id={id} (Secret Auth)
    activate App
    App->>SMTP: Send follow-up email template
    App-->>n8n: Success status
    deactivate App
    
    activate n8n
    Note over n8n: n8n workflow initiates second 5-day wait
    deactivate n8n
    
    Note over App, n8n: 5 Days Pass
    
    n8n->>App: POST /Lead/GetLeadStatus?Id={id} (Secret Auth)
    App-->>n8n: Returns status (e.g. New / InitialSent)
    
    alt Status is NOT Replied or Closed
        n8n->>App: POST /Lead/MarkAsNotInterested?Id={id} (Secret Auth)
        App->>DB: Update status to Not Interested
        App-->>n8n: Success status
    else Status is Replied or Closed
        Note over n8n: Do nothing (Lead engaged)
    end
```

---

## 🛠 Tech Stack

* **Backend**: ASP.NET Core 8.0 (MVC pattern), Entity Framework Core (Database-first mapping).
* **Database**: PostgreSQL.
* **Frontend**: Vanilla CSS (Custom Glassmorphism Design System), Bootstrap 5, jQuery, DataTables.net, Chart.js.
* **Integrations & Alerts**: SweetAlert2 (Dark theme popups), SMTP mail relay, n8n Webhooks.

---

## 📁 Repository Directory Structure

* [docs/Backend.md](docs/Backend.md): Database architecture, custom PBKDF2 authentication, and API endpoints map.
* [docs/Automation.md](docs/Automation.md): Detailed n8n workflow parameters, webhook security, and retry policies.
* [docs/DecisionLog.md](docs/DecisionLog.md): Architectural decisions (e.g., n8n vs. Hangfire).
* [docs/Challenges.md](docs/Challenges.md): Web design and scroll layout challenges, responsive card renderings, and webhook idempotency.
