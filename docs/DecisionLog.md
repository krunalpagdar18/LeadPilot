# Architectural Decision Log

This document logs critical architectural decisions made during the design of LeadPilot.

---

## ADR 001: Choosing n8n over Hangfire / Custom Job Schedulers

### Context
LeadPilot requires delayed, multi-stage communication schedules (e.g. waiting 5 days, sending follow-up, waiting another 5 days, checking response). Implementing this requires a job scheduling mechanism that can handle long-running delay states.

### Options Considered
1. **Hangfire**: A popular C# background job scheduler that saves state in SQL Server/Redis.
2. **Custom Job Scheduler**: A background worker thread (`BackgroundService`) querying database timestamps.
3. **n8n Workflow Engine**: An external Node-based visual automation platform.

### Decision
We chose **n8n Workflow Engine** to handle long-running schedules and email drip logic. 

**Trade-offs**: This choice introduces a third-party runtime dependency, self-hosting configurations, and external network monitoring overhead compared to a purely in-process C# Hangfire solution. However, this trade-off was accepted because visual debugging transparency and long-term delay-state durability outweighed the maintenance costs.

### Rationale

#### 1. Zero Infrastructure Overhead
Hangfire requires database schema migrations, dedicated tables, and SQL/Redis locking mechanisms. A custom scheduler requires writing pooling logic. n8n operates independently, keeping the main LeadPilot database clean and free of scheduling overhead.

#### 2. Visual Debugging & Log Tracking
Nurturing cycles span several days. If a lead fails to get an email, debugging Hangfire requires querying serialized JSON metadata blobs. In n8n, developers can visually inspect active, waiting, or failed lead executions on the canvas, check exact payload transfers, and trigger manual retries.

#### 3. Protection Against Server Crashes
If the IIS / Kestrel server crashes, is restarted, or scales down in cloud environments:
* Custom background worker threads lose memory queues.
* Hangfire needs recovery queries.
* **n8n** stores wait states independently. The lead nurturing workflow remains active in n8n and will simply execute callbacks once the LeadPilot web app comes back online (incorporating HTTP retry backoffs).

#### 4. Separation of Concerns
The web application is responsible only for database CRUD actions and sending single email templates. It does not manage time state, scheduling offsets, or logic branches, allowing the C# codebase to remain clean and maintainable.
