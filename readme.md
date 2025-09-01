# GraphQL Aspire Demo

This project is a **.NET 9 Aspire application** demonstrating a **GraphQL API** using [Hot Chocolate](https://chillicream.com/docs/hotchocolate/). It includes features such as queries with paging/filtering/sorting, mutations with validation, subscriptions, and in-memory data storage with seed data.

The project is structured to run under **Aspire AppHost** for orchestration, but the GraphQL API can also run standalone for debugging.

---

## 📂 Project Structure

```
GraphQLAspireDemo/
├─ src/
│  ├─ AppHost/             # Aspire orchestrator
│  ├─ ServiceDefaults/     # Cross-cutting concerns (health, logging, telemetry)
│  └─ GraphQLApi/          # GraphQL service with queries, mutations & subscriptions
```

---

## 🚀 Features

* **GraphQL API with Hot Chocolate**

  * Queries with `[UsePaging]`, `[UseFiltering]`, `[UseSorting]`, `[UseProjection]`
  * Mutations for creating books & reviews with validation
  * Subscriptions (in-memory) to stream new reviews
* **Data access**

  * In-memory store with seeded authors, books, and reviews
  * Example of `DataLoader` for efficient batching
* **Aspire integration**

  * AppHost orchestrates services and exposes endpoints
  * ServiceDefaults adds health checks and observability
* **Banana Cake Pop UI** at `/graphql` for interactive queries
* **Voyager UI** at `/voyager` for schema visualization

---

## 🛠️ Requirements

* [.NET 9 SDK](https://dotnet.microsoft.com/)
* (Optional) [Rider](https://www.jetbrains.com/rider/) or VS Code with .NET plugins
* Aspire templates installed:

  ```bash
  dotnet new install Aspire.ProjectTemplates::9.4.0
  ```

---

## ▶️ Running the App

### 1. Restore dependencies

```bash
dotnet restore
```

### 2. Run via Aspire

```bash
dotnet run --project src/AppHost
```

### 3. Run GraphQLApi standalone (debugging)

```bash
dotnet run --project src/GraphQLApi
```

* GraphQL endpoint: **[http://localhost:5228/graphql](http://localhost:5228/graphql)**
* Voyager schema explorer: **[http://localhost:5228/voyager](http://localhost:5228/voyager)**

---

## 🔍 Example Queries

### Query books with filtering, sorting, and projection

```graphql
query {
  books(first: 5, order: { publishedAt: DESC }, where: { title: { contains: "e" } }) {
    totalCount
    nodes {
      id title publishedAt
      author { id name }
    }
  }
}
```

### Create a new book

```graphql
mutation {
  createBook(input: { title: "Code Breakers", publishedAt: "2024-01-01T00:00:00Z", authorId: "PUT_AUTHOR_ID_HERE" }) {
    id title
  }
}
```

### Subscribe to new reviews

```graphql
subscription {
  onReviewAdded {
    id bookId reviewer rating comment createdAt
  }
}
```

---

## ✅ Next Steps

* Swap the in-memory store for EF Core + a real database
* Add authentication/authorization with `HotChocolate.AspNetCore.Authorization`
* Integrate OpenTelemetry exporters in `ServiceDefaults` for end-to-end tracing
