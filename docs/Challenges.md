# Technical Challenges & Solutions

![CSS](https://img.shields.io/badge/CSS-Glassmorphism-blue?style=flat&logo=css3&logoColor=white)
![Chart.js](https://img.shields.io/badge/Chart.js-FF6384?style=flat&logo=chartdotjs&logoColor=white)
![Performance](https://img.shields.io/badge/Performance-Optimized-brightgreen?style=flat)

This document details the engineering challenges encountered during the development of LeadPilot and the solutions implemented.

---

## 1. Mobile Scrolling Interference (Chart.js Interception)

### Challenge
On the analytical dashboard, when testing on physical mobile devices, vertical scrolling would get "stuck" when the user dragged their finger over the Chart.js canvases. Chart.js was binding touch event listeners to the canvas parent nodes to handle hover tooltips, intercepting touch gestures and calling `preventDefault()` on vertical scroll gestures.

### Solution
* **Event Disabling**: We configured the charts with `events: []` inside the JS options block to prevent Chart.js from attaching touch event listeners to the DOM container.
* **CSS Passthrough**: We disabled pointer events on the chart canvases and restricted touch-action to vertical panning, so gestures pass through to the page's scroll container instead of being captured by Chart.js. This forced the browser to pass touch events directly through the canvases to the root body scroll container, restoring smooth vertical scrolling.

---

## 2. Horizontal Layout Overflows (Double Scrollbars)

### Challenge
Background glassmorphism glow spheres were positioned absolutely with negative horizontal values (`left: -15%`, `right: -15%`). Setting `overflow-x: hidden` on the `html` and `body` elements broke mobile native momentum scrolling (inertia), making page scrolling feel heavy and stuttering. Removing the overflow rule caused the page to scroll horizontally, leaving empty gaps on the right.

### Solution
We isolated the floating decoration elements into a dedicated fixed-position background layer sized to the viewport, so the browser clips them at the boundary without affecting page layout. Because the container matches viewport dimensions, the browser clips the spheres perfectly at the viewport boundaries, preventing any layout stretching. Removing horizontal overflows from the root layout while maintaining standard body limits preserved smooth iOS/Android vertical momentum scrolling.

---

## 3. Webhook Idempotency & Re-delivery

### Challenge
Network glitches or server timeouts can cause n8n to retry a webhook callback (e.g. `/Lead/UpdateLeadStatus` or `/Lead/MarkAsNotInterested`) even though the lead's status has already been updated. If the endpoint is not idempotent, re-deliveries could cause database errors or trigger unintended side-effects.

### Solution
We designed the controller actions and service methods to perform safe, state-independent updates:
* **Validation checks**: The endpoints fetch the target lead, check if the status matches the requested transition, and execute updates only if the status differs.
* **Safe returns**: If the status is already updated, the endpoint returns a `200 OK` success response immediately instead of throwing an error or repeating side-effects, ensuring idempotency.

---

## 4. Large-Scale DataTable Server-Side Pagination

### Challenge
Displaying thousands of leads on a web page degrades browser rendering performance. Returning all records from SQL Server also impacts database memory and query response times.

### Solution
* **Server-Side Processing**: We configured jQuery DataTables to run in `serverSide: true` mode. The grid sends pagination coordinates (`PageIndex`, `PageSize`, `SearchText`, `StatusId`) to the server.
* **SQL Query Indexing**: In the lead repository's query layer, the query executes pagination offsets at the database level, fetching only the active page size (e.g. 10 or 50 records). The database queries are optimized via indexing on user identifiers and status identifiers to ensure efficient data retrieval and maintain fast page load times for sales agents.
