# 📚 ReadFlow — Reading Tracker API

🚧 **Status: Work in Progress (Active Development)**  
🌍 English | 🇧🇷 [Versão em Português](README.md)

ReadFlow is a **RESTful API for tracking book reading progress**, inspired by platforms such as Skoob and Goodreads.  
It is built with a strong focus on **software engineering principles, clean architecture, scalability, and observability**.

The project is designed as an **evolving MVP**, with well-documented technical decisions and a long-term vision.

---

## 🎯 Goal

Enable users to:
- register books
- track reading progress
- organize books by reading status
- write reviews and ratings

All supported by:
- a **rich domain model**
- **explicit business rules**
- **infrastructure ready for scale**

---

## 🧠 Core Features

- 📖 Reading status management:
  - To Read
  - Reading
  - Re-reading
  - Read
  - Abandoned
- ⭐ Book ratings (0–5, 0.5 increments)
- ✍️ Public reviews
- 📚 User bookshelves
- 🔍 Book search by title or author
- 🔥 Popular books listing
- 📊 Built-in observability

---

## 🏗️ Architecture & Design

The project follows:
- **Clean Architecture**
- **Domain-Driven Design (DDD)**
- Clear separation of concerns

📄 Technical decisions includes:
- domain modeling
- database choices
- caching strategy
- observability
- authentication and authorization

---

## ⚙️ Tech Stack

- **.NET**
- **Entity Framework Core**
- **PostgreSQL**
- **Redis**
- **JWT Authentication** (dev environment)
- **OpenTelemetry**
- **Prometheus**
- **Grafana**

---

## 🔐 Authentication & Authorization

- JWT-based authentication
- Role-based and resource-based authorization
- Secure access control for user-specific resources

---

## 📊 Observability

The API is instrumented with **OpenTelemetry**, providing:

- Distributed tracing
- HTTP metrics (latency, throughput, error rate)
- Cache metrics (hit/miss)
- Structured logs correlated with traces

Planned **Grafana dashboards** for system visibility and performance analysis.

---

## 🧪 Tests (in progress)

- Unit tests (domain and use cases)
- Integration tests (database and cache)
- Authorization tests
- CI pipeline planned with GitHub Actions

---

## 🚀 Roadmap

- [ ] Complete core endpoints
- [ ] Increase test coverage
- [ ] OpenAPI documentation
- [x] Initial data seeding
- [ ] Full Grafana dashboards
- [ ] CI/CD automation

---

## 🎓 Context

This project is developed as **advanced hands-on practice** in:
- backend engineering
- system design
- observability
- scalable API development
