# Grovly - E-Commerce API

A production-minded REST API for an online toy store, built with C# / ASP.NET Core and Entity Framework Core (Code-First). This repository is the backend of a full-stack portfolio project; the frontend (Next.js + TypeScript) lives in a separate repository: [sklepgrovly-frontend](https://github.com/Konradiii/sklepgrovly-frontend).

The goal of this project was not just to make an e-commerce API *work*, but to make deliberate architectural decisions and be able to explain the reasoning behind each one. The sections below focus on **why** things were built the way they were, not only **what** they do.

---

## Tech Stack

- **Language / Framework:** C# / ASP.NET Core (.NET 10)
- **ORM / Database:** Entity Framework Core (Code-First) with SQL Server (LocalDB)
- **Authentication:** JWT (access + refresh tokens) with BCrypt password hashing
- **API Documentation:** Swagger / OpenAPI (Swashbuckle) with Bearer auth support
- **Architecture:** Layered - Controllers (HTTP) / Services (business logic) / EF Core (persistence), DTOs separating the API contract from entities

---

## Key Architectural Decisions

This is the part I care about most. Each decision below solves a concrete problem, and each has a trade-off I can defend.

### Identity always comes from the JWT, never from the request body

Every endpoint that acts on a user's own resource reads the caller's identity from the validated JWT claims, **not** from an ID sent in the request body. Trusting an ID from the body would allow a user to act on another user's data by simply changing a number - a classic IDOR (Insecure Direct Object Reference) vulnerability. The token is the single source of truth for *who* is making the request.

### Ownership checks return "Not Found", not "Forbidden"

When a user requests a resource that exists but isn't theirs, the API responds as if it doesn't exist rather than returning "Forbidden". Returning "Forbidden" would confirm that the resource *exists*, leaking information. The ownership check and the existence check produce the same response, so an attacker can't distinguish "not yours" from "doesn't exist".

### Purchase prices are frozen at order time

Order line items store a `CenaZakupu` (purchase price) copied from the product **at the moment the order is placed**, rather than referencing the product's current price. Product prices change over time; an order must preserve what the customer actually agreed to pay. Reading the live price later would rewrite history and could show a different amount than what was charged.

### Refresh-token rotation

Logging in issues a short-lived access token (15 min) and a long-lived refresh token (15 days). Each time the refresh token is used, the old one is revoked and a new pair is issued (rotation). If a refresh token is ever stolen and used, the legitimate rotation invalidates it, limiting the window of misuse. Refresh tokens are stored **hashed** (SHA-256) in the database - a database leak doesn't expose usable tokens.

### Password change invalidates all active sessions

Changing a password verifies the old password (BCrypt) and then revokes **all** of the user's active refresh tokens in the same operation. If an account was compromised, changing the password should log the attacker out everywhere - not leave their session alive. Access tokens are short-lived and expire on their own; refresh tokens are the long-lived risk, so they are the ones invalidated.

### Payment status is driven by the webhook, not by the client

Payment initiation only creates a pending payment and returns a link; the order is marked as paid **only** when the payment gateway calls back via webhook. The client's return redirect is treated as cosmetic (it can be faked, or never fire if the user closes the browser). The webhook - a server-to-server notification - is the trusted source of truth about whether money actually moved. The webhook handler is also **idempotent**: a duplicated notification (gateways retry) won't process the payment twice.

### Business rules live in the service layer

Rules like "a review requires a delivered order containing that product" or "you can't pay for an already-paid order" are enforced in the service layer - not in controllers and not in the database. Controllers stay thin (HTTP concerns only), the database stays focused on persistence, and business logic lives in one testable, reusable place.

### Atomic persistence

Operations that change multiple pieces of state - placing an order (create order + decrement stock), cancelling an order (restore stock + change status), changing a password (update hash + revoke tokens) - commit everything in a **single** `SaveChanges` call. Either the whole operation succeeds or none of it does, preventing inconsistent states like decremented stock on an order that failed to save.

### Soft-delete for products, restrict-delete for history

Products are never hard-deleted; they're archived via a `CzyAktywny` (is-active) flag. A deleted product still needs to appear in past orders. Relationships that carry history (orders, payments) use `OnDelete(Restrict)` so history can't be silently destroyed, while true compositions use `Cascade`.

---

## Features

- **Authentication & Authorization** - registration, login, token refresh with rotation, logout, role-based access (Customer / Administrator)
- **Products** - listing (with category filtering), details, create/edit/archive (admin), archived-product listing
- **Categories** - full CRUD with delete-guard against categories still holding products
- **Orders** - placement with per-item stock and availability validation, order history (own / admin-wide), status transitions, cancellation with stock restoration
- **Reviews** - creation gated by a verified-purchase rule, editing and deletion with ownership verification
- **Payments** - a mock payment service modelling the full gateway flow: initiation, webhook-driven status updates, idempotency, and double-payment protection

---

## Security Highlights

- **JWT** with issuer/audience/lifetime/signature validation
- **BCrypt** password hashing (work factor 12)
- **IDOR protection** - identity from token, ownership checks that don't leak existence
- **Refresh-token rotation** with hashed storage and revocation on password change
- **Idempotency guards** on logout, order cancellation, and payment webhooks
- **Global exception handling** with consistent Problem Details responses

---

## Getting Started

### Prerequisites

- [.NET SDK 10](https://dotnet.microsoft.com/download)
- SQL Server LocalDB (ships with Visual Studio, or install SQL Server Express)

### Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/Konradiii/Sklep-Internetowy---Code-First.git
   cd Sklep-Internetowy---Code-First
   ```

2. Configure your secrets (JWT key/issuer and connection string). Using .NET user-secrets:
   ```bash
   dotnet user-secrets set "Jwt:Key" "your-secret-signing-key"
   dotnet user-secrets set "Jwt:Issuer" "your-issuer"
   dotnet user-secrets set "ConnectionStrings:Default" "your-connection-string"
   ```

3. Apply migrations to create the database:
   ```bash
   dotnet ef database update
   ```

4. Run the API:
   ```bash
   dotnet run
   ```

5. Open Swagger UI at `https://localhost:<port>/swagger` to explore and test the endpoints. Use the **Authorize** button to supply a JWT for protected routes.

---

## Project Status

The backend is feature-complete for an MVP: authentication, catalog, orders, reviews, and a mock payment flow all work end-to-end and have been tested via Swagger. The mock `PaymentService` is intentionally swappable behind `IPaymentService` for a future real gateway integration (e.g. HotPay).

Planned next steps: selective unit tests (xUnit) targeting the order and payment services, and deployment.

---

## Related Repositories

- **Frontend** (Next.js + TypeScript, SSR for SEO): [sklepgrovly-frontend](https://github.com/Konradiii/sklepgrovly-frontend)