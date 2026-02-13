# Developer ERP MVP (AI-ready)

## Stack
- Backend: .NET 8, ASP.NET Core, C#, Clean Architecture, EF Core, PostgreSQL, pgvector, Hangfire-ready.
- Frontend: React + TypeScript + Vite, TanStack Query, Zustand, strict typing.
- API: REST + OpenAPI/Swagger.
- Auth: JWT (OIDC-compatible integration point for Keycloak).

## Repository structure
```
/src
  /backend
    /Api
    /Application
    /Domain
    /Infrastructure
    /Tests
  /frontend
    /src
  /infra
    docker-compose.yml
```

## Domain model (mandatory)
ProjectDefinition, WBS, Lifecycle, Milestones, Physical structure, Asset Registry,
Contracts, Issues, Documents, AI entities.

## Historical data ingestion
- Idempotent upsert by project code (`IngestionService`, `ProjectRepository`).
- Validation point in API DTO layer.
- Staging-ready architecture via separate ingestion contract.

## AI layer
- Embeddings support through pgvector column and deterministic embedding service.
- Similarity retrieval using historical projects + cosine scoring.
- Risk prediction MVP with explainable factors.
- RAG query with source citations.

## Readiness engine
- Two-track readiness: financial + technical.
- Drill-down by system (extensible to stage/building/capex/wbs/lot).
- Backend-only calculations.

## API endpoints (MVP)
- `POST /api/projects/ingest`
- `GET /api/projects/{projectId}/dashboard`
- `POST /api/projects/{projectId}/ask-ai`

## Seed data
- 1 active project (`PRJ-ACTIVE`)
- 3 historical projects (`PRJ-HIS-1..3`)
- WBS, milestones, assets, and document chunks

## Run locally
```bash
cd src/infra
docker compose up --build
```

Backend Swagger: `http://localhost:8080/swagger`
Frontend: `http://localhost:5173`

## Step-by-step implementation status
1. ERD + migrations + readiness + ingestion ✅ (entity model, `EnsureCreated`, ingestion service)
2. API ✅
3. React UI ✅
4. Embeddings + similarity ✅
5. Risk model MVP ✅
6. RAG integration ✅

## Tests
- Unit: readiness calculation, ingestion merge logic, similarity retrieval.
- Integration: PostgreSQL testcontainer smoke test scaffold.
