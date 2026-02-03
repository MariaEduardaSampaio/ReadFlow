# 📚 ReadFlow — Reading Tracker API

🚧 **Status: Em desenvolvimento ativo (Work in Progress)**  
🇧🇷 Português | 🌍 [English version](README_EN.md)

ReadFlow é uma **API REST para gerenciamento de leitura de livros**, inspirada em plataformas como Skoob e Goodreads, desenvolvida com foco em **engenharia de software, arquitetura limpa, escalabilidade e observabilidade**.

O projeto foi pensado como um **MVP evolutivo**, com decisões técnicas documentadas, priorizando boas práticas desde o início.

---

## 🎯 Objetivo

Permitir que usuários:
- cadastrem livros
- acompanhem seu progresso de leitura
- organizem leituras por status
- registrem avaliações e resenhas

Tudo isso com:
- **domínio bem definido**
- **regras de negócio explícitas**
- **infraestrutura preparada para escala**

---

## 🧠 Principais Funcionalidades

- 📖 Controle de leitura por status:
  - Quero ler
  - Lendo
  - Relendo
  - Lido
  - Abandonado
- ⭐ Avaliação de livros (0 a 5, com incremento de 0.5)
- ✍️ Resenhas públicas
- 📚 Estante de livros por usuário
- 🔍 Busca de livros por título ou autor
- 🔥 Listagem de livros populares
- 📊 Métricas e observabilidade

---

## 🏗️ Arquitetura e Design

O projeto segue princípios de:
- **Clean Architecture**
- **Domain-Driven Design (DDD)**
- **Separação clara de responsabilidades**

📄 As decisões técnicas incluem:
- modelagem do domínio
- escolhas de banco de dados
- estratégia de cache
- observabilidade
- autenticação e autorização

---

## ⚙️ Tecnologias Utilizadas

- **.NET**
- **Entity Framework Core**
- **PostgreSQL**
- **Redis** (cache)
- **JWT Bearer Authentication** (ambiente de desenvolvimento)
- **OpenTelemetry**
- **Prometheus**
- **Grafana**

---

## 🔐 Autenticação e Autorização

- Autenticação via **JWT**
- Autorização baseada em:
  - usuário autenticado
  - roles (User, Author, Admin)
- Endpoints protegidos por regras de acesso resource-based

---

## 📊 Observabilidade

A API é instrumentada com **OpenTelemetry**, coletando:

- 🔍 Traces
- 📈 Métricas HTTP (latência, throughput, erros)
- 🧠 Métricas de cache (hit/miss)
- 🧾 Logs estruturados correlacionados por traceId

Dashboards planejados no **Grafana** para:
- latência por endpoint
- efetividade do cache
- taxa de erro
- gargalos de performance

---

## 🧪 Testes (em evolução)

- Testes de unidade (domínio e casos de uso)
- Testes de integração (PostgreSQL e Redis)
- Testes de autorização
- Pipeline CI planejado com GitHub Actions

---

## 🚀 Roadmap

- [ ] Finalizar endpoints principais
- [ ] Aumentar cobertura de testes
- [ ] Documentação OpenAPI (Swagger)
- [x] Seed inicial de dados
- [ ] Dashboards completos no Grafana
- [ ] CI/CD automatizado

---

## 🎓 Contexto

Projeto desenvolvido para **estudo avançado e prática real** de:
- engenharia de software
- arquitetura de sistemas
- backend moderno
- observabilidade e performance

