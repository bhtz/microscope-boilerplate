---
apply: always
---

# Data API Builder (DAB) Coding Rules

Spec-driven development rules for Data API Builder configuration, schema setup, and database integration.

## Architecture Principles

- **Database as a Service (DaaS)**: DAB provides a REST/GraphQL API over PostgreSQL
- **Configuration-Driven**: All behavior defined in `dab-config.json`
- **Auto-Generated APIs**: REST and GraphQL endpoints automatically generated from schema
- **Security First**: Built-in authorization and authentication policies
- **Reverse Proxy Target**: BFF accesses DAB via YARP

## Project Structure

```
src/IAC/Docker/
├── Dab/
│   ├── dab-config.json          # DAB configuration
│   └── docker-compose.yml       # DAB container setup
├── Postgres/
│   ├── init.sql                 # Schema and seed data
│   └── docker-compose.yml       # PostgreSQL container
```

## Database Schema Setup

Location: `src/IAC/Docker/Postgres/init.sql`

**Schema Pattern**:
```sql
-- Create schema
CREATE SCHEMA IF NOT EXISTS dab;

-- Create tables in DAB schema
CREATE TABLE IF NOT EXISTS dab.leads (
    id BIGSERIAL PRIMARY KEY,
    firstname TEXT NOT NULL,
    lastname TEXT NOT NULL,
    email TEXT UNIQUE NOT NULL,
    phone VARCHAR(20),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create indexes for performance
CREATE INDEX idx_leads_email ON dab.leads(email);
CREATE INDEX idx_leads_created_at ON dab.leads(created_at);

-- Insert seed data (optional)
INSERT INTO dab.leads (firstname, lastname, email, phone) VALUES
('Alice', 'Johnson', 'alice@example.com', '123-456-7890')
ON CONFLICT (email) DO NOTHING;
```

**Best Practices**:
- Always prefix tables with schema name (e.g., `dab.table_name`)
- Use BIGSERIAL for auto-incrementing primary keys
- Add meaningful indexes for frequently queried columns
- Use constraints (NOT NULL, UNIQUE) for data integrity
- Include timestamp fields for audit trails

## DAB Configuration

Location: `src/IAC/Docker/Dab/dab-config.json`

**Complete Configuration Pattern**:
```json
{
  "$schema": "https://dataapibuilder.azureedge.net/schemas/v1/dab.draft.schema.json",
  "api": {
    "version": "v1",
    "servers": {
      "Https": {
        "url": "https://localhost:5001",
        "port": 5001
      },
      "Http": {
        "url": "http://localhost:4700",
        "port": 4700
      }
    }
  },
  "runtime": {
    "rest": {
      "enabled": true
    },
    "graphql": {
      "enabled": true,
      "path": "/graphql"
    }
  },
  "entities": {
    "Leads": {
      "source": {
        "object": "dab.leads",
        "type": "table"
      },
      "permissions": [
        {
          "role": "anonymous",
          "actions": [
            {
              "action": "read",
              "fields": {
                "include": ["id", "firstname", "lastname", "email", "phone", "created_at"]
              }
            }
          ]
        },
        {
          "role": "authenticated",
          "actions": [
            {
              "action": "read"
            },
            {
              "action": "create"
            },
            {
              "action": "update"
            },
            {
              "action": "delete"
            }
          ]
        }
      ],
      "graphql": {
        "type": {
          "singular": "lead",
          "plural": "leads"
        }
      },
      "rest": true,
      "cache": {
        "enabled": true,
        "ttl": 600
      }
    }
  },
  "authentication": {
    "provider": "StaticWebApps"
  },
  "authorization": {
    "enforcePolicy": "true"
  }
}
```

## Entity Configuration

### REST Endpoints

DAB automatically generates REST endpoints based on entity configuration:

- `GET /api/Leads` - List all leads
- `GET /api/Leads/{id}` - Get specific lead
- `POST /api/Leads` - Create new lead
- `PUT /api/Leads/{id}` - Update lead
- `DELETE /api/Leads/{id}` - Delete lead

**Query Parameters**:
- `$filter` - OData filter expressions
- `$orderby` - Sorting by columns
- `$skip` - Pagination offset
- `$top` - Pagination limit
- `$select` - Column selection

**Example Request**:
```
GET /api/Leads?$filter=firstname eq 'Alice'&$orderby=created_at desc&$top=10
```

### GraphQL Queries

**Auto-Generated Pattern**:
```graphql
query GetLeads {
  leads(first: 10, after: null) {
    edges {
      node {
        id
        firstname
        lastname
        email
        phone
        createdAt
      }
    }
    pageInfo {
      hasNextPage
      endCursor
    }
  }
}

query GetLead($id: ID!) {
  leadById(id: $id) {
    id
    firstname
    lastname
    email
    phone
    createdAt
  }
}
```

## Relationships & Joins

**Configuration Pattern** (for related entities):
```json
{
  "entities": {
    "LeadContacts": {
      "source": {
        "object": "dab.lead_contacts",
        "type": "table"
      },
      "relationships": [
        {
          "cardinality": "one",
          "relationship": "lead",
          "target.entity": "Leads",
          "source.fields": ["lead_id"],
          "target.fields": ["id"]
        }
      ]
    }
  }
}
```

## Authorization & Security

### Role-Based Access Control

```json
{
  "permissions": [
    {
      "role": "anonymous",
      "actions": [
        {
          "action": "read",
          "fields": {
            "include": ["id", "firstname", "lastname", "email"]
          }
        }
      ]
    },
    {
      "role": "authenticated",
      "actions": [
        {
          "action": "read"
        },
        {
          "action": "create",
          "policy": {
            "database": "@claims.uid = @item.created_by"
          }
        },
        {
          "action": "update",
          "policy": {
            "database": "@claims.uid = @item.updated_by"
          }
        },
        {
          "action": "delete",
          "policy": {
            "database": "@claims.uid = @item.created_by"
          }
        }
      ]
    }
  ]
}
```

## Caching Strategy

```json
{
  "cache": {
    "enabled": true,
    "ttl": 600,
    "types": ["redis", "inmemory"]
  }
}
```

- **TTL**: Time-to-live in seconds (300-3600 recommended)
- **Types**: 
  - `inmemory`: Process memory (default)
  - `redis`: Distributed cache for scaling

## Performance Best Practices

1. **Indexes**: Create indexes on foreign keys and frequently queried columns
2. **Pagination**: Always use `$skip` and `$top` for large datasets
3. **Caching**: Enable caching for read-heavy entities
4. **Filtering**: Use `$filter` to reduce data transfer
5. **Column Selection**: Use `$select` to limit returned fields

## Data Validation

**Database Constraints**:
```sql
-- Check constraints
ALTER TABLE dab.leads 
ADD CONSTRAINT check_email_format 
CHECK (email ~* '^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}$');

-- Foreign key constraints
ALTER TABLE dab.lead_contacts 
ADD CONSTRAINT fk_lead_contacts_lead 
FOREIGN KEY (lead_id) REFERENCES dab.leads(id) ON DELETE CASCADE;
```

## Monitoring & Logging

```json
{
  "logging": {
    "level": "information",
    "enabled": true
  }
}
```

## Testing

**Integration Testing Pattern** (with test containers):
```csharp
public class DabIntegrationTests : IAsyncLifetime
{
    private PostgreSqlContainer _container = null!;
    private HttpClient _client = null!;

    public async Task InitializeAsync()
    {
        _container = new PostgreSqlBuilder()
            .WithImage("postgres:15")
            .WithDatabase("mcsp_app")
            .Build();
        
        await _container.StartAsync();
        _client = new HttpClient { BaseAddress = new Uri("http://localhost:4700") };
    }

    [Fact]
    public async Task GetLeads_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/Leads");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    public async Task DisposeAsync()
    {
        await _container.StopAsync();
    }
}
```

## Docker Deployment

**docker-compose.yml Pattern**:
```yaml
services:
  postgres:
    image: postgres:15
    environment:
      POSTGRES_DB: mcsp_app
      POSTGRES_PASSWORD: postgres
    volumes:
      - ./Postgres/init.sql:/docker-entrypoint-initdb.d/init.sql
    ports:
      - "5432:5432"

  dab:
    image: mcr.microsoft.com/azure-databases/data-api-builder:latest
    environment:
      DATABASE_CONNECTION_STRING: "Server=postgres;Database=mcsp_app;User Id=postgres;Password=postgres;"
    volumes:
      - ./Dab/dab-config.json:/etc/dab/dab-config.json
    ports:
      - "4700:4700"
    depends_on:
      - postgres
```

## Code Quality Standards

- **Configuration Validation**: Validate `dab-config.json` against schema
- **Naming Conventions**: Use snake_case for database objects (dab.lead_contacts)
- **Documentation**: Add comments for complex configurations
- **Security**: Always use role-based permissions, never allow anonymous write
- **Monitoring**: Enable logging for debugging and auditing
