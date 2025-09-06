# Microscope.Boilerplate solution structure

> Add your own solution structure documentation

## Services

> Each service is a bounded context implemented by layered architecture solution

### Domain Layer:

> The Domain Layer is the core and most critical layer in Domain-Driven Design. It contains the heart of the business logic and represents the business domain. This layer focuses on encapsulating the business rules, behaviors, and concepts of the application. It consists of the following components:

- **Domain Entities:** Objects representing the core business concepts.
- **Value Objects:** Immutable objects representing descriptive aspects of the domain.
- **Aggregates:** Clusters of related objects treated as a single unit of consistency and transactional boundaries.
- **Domain Services:** Encapsulate domain logic that doesn't naturally fit within a single entity.
- **Repositories:** Provide an abstraction over data storage, allowing the application to interact with the persistence mechanism without knowing the underlying implementation.

### Application Layer:

> The Application Layer sits on top of the Domain Layer and acts as an orchestrator of domain objects to fulfill user requests. It is responsible for coordinating the application's business workflows and handling user interactions. This layer contains application services that provide coarse-grained use cases, often mapped one-to-one with the use case requirements defined by the business. The application services collaborate with domain entities, value objects, and domain services from the Domain Layer to execute business logic, enforce business rules, and perform transactions.

- **Mappings:**
- **Behaviour:**
- **Policies:**
- **Features:**
- **Application services:**

### Infrastructure Layer:

> The Infrastructure Layer is responsible for providing technical implementations that support the Domain and Application Layers. It contains components that handle concerns like data persistence, messaging, caching, external integrations, and any other infrastructure-related services. Examples of infrastructure components are databases, data access repositories, message queues, email services, file storage systems, etc. This layer abstracts the underlying technologies from the rest of the application, allowing easy replacement or adaptation of technical components without impacting the core domain logic.

- **Persistence configurations:**
- **Repository implementation:**
- **Migration scripts:**
- **External services implementation:**

### Interface Layer:

> The Interface Layer is the outermost layer of the application and is responsible for handling communication with external systems and users. It provides various interfaces to interact with the application, such as user interfaces (web UI, mobile app UI), APIs (RESTful, GraphQL), command-line interfaces (CLI), and messaging interfaces. The Interface Layer translates user input into application requests and presents the application's output to users or external systems. This layer should be as thin as possible, delegating most of the business logic and processing to the Domain and Application Layers.

- **Services configurations:**
- **REST API:**
- **GraphQL API:**
- **Custom middlewares:**
- **Interface services implementation:**

## IAC

> Infrastructure as Code (IAC) plays a pivotal role within the context of a microservices and Domain-Driven Design (DDD) solution architecture. 
> IAC refers to the practice of defining and managing the underlying infrastructure, including servers, databases, networking, and more, through code-based configurations and scripts. 
> IAC empowers development teams to provision, configure, and manage the diverse components of their distributed systems consistently and programmatically. 
> By treating infrastructure as code, organizations can ensure scalability, maintainability, and reliability across their microservices ecosystem. 

- **docker-compose.yml:** 
- **init.sql :**

## Clients
### SDK

> The SDK (Software Development Kit) layer represents a boundary between the clients layer and the services exposing REST and/or GraphQL APIs. 
> Its primary objective is to encapsulate the complexities of interacting with these APIs, offering a simplified, uniform, and consistent interface to the clients (web, console, desktop, mobile, ...).

**TodoApp GraphQL SDK:**
  * Strawberry shake

**TodoApp REST SDK:**
  * Typescript
  * dotnet
 
### Web

> The web application represents the user interface layer of the client-side. Whether it is a Single Page Application or a Progressive Web App, the focus is on delivering a rich and responsive user experience in the browser. 
> The SPA architecture allows for dynamic content updates without reloading the entire page, while PWAs enable native-like capabilities such as offline access and push notifications.

**Web Application (SPA / PWA):**
- **Configuration:**
- **Pages:**
- **Shared components:**
- **Pages:**
- **Settings:**

### BFF (Backend For Frontend)

> The Backend for Frontend is a design pattern where a dedicated backend service is created for each specific client application or type of client.
> It acts as an intermediary between the web application and the microservices, serving as a tailored API gateway for the client. The BFF pattern enables customizing data and functionalities to suit the exact requirements of the client, reducing unnecessary data transfers and minimizing the risk of over-fetching (aggregation call).
> By hosting the PWA, the BFF serves the static files and resources required for the client-side application to run in the user's browser. This hosting can be done using web servers or serverless infrastructure, depending on the application's needs and scalability requirements. Hosting the PWA on the BFF allows for better control over caching strategies, content delivery, and server-side configurations to enhance the application's performance and reliability.
> As the BFF interacts with various microservices to fetch data for the client application, it acts as a reverse proxy for the microservice API. This means that instead of the client directly communicating with the microservices, all API requests from the client are directed to the BFF, which then forwards those requests to the appropriate microservices.

- **Blazor hosted:**
- **YARP reverse proxy:**

### CLI

> A word for CLI client

- **CLI:**
- **Console GUI:**

### Mobile

> A word for mobile client

## Documentation

### Documentation as code

> The "Documentation Layer" revolutionizes the way we approach software project documentation. By writing solution documentation as code using Markdown, incorporating Mermaid for visualizations, and using VitePress to generate a web documentation portal, developers can foster a culture of up-to-date, accessible, and engaging documentation that grows alongside the project. Embrace the power of code-driven documentation and unlock the true potential of your software projects.

### Living documentation

**Product Discovery**
* Discovery discipline notes template

**Architecture**
* Getting started guidelines
* Event storming catalog
* Architecture schema
* Bounded context canvas
  * Aggregate canvas
* Technology matrix guidelines

**Organization & Governance**
* Governance process templates
  * Weekly meeting notes
  * Architecture Decision record
* Product engineering organization
  * product engineering squad template

**Guidelines**
* Generic product engineering guidelines & must-read you want to provide to the teams

## Additional resources

**Clean architecture**
![](https://miro.medium.com/v2/resize:fit:800/1*0R0r00uF1RyRFxkxo3HVDg.png)

**Domain driven hexagon**
![](https://user-images.githubusercontent.com/776825/144645528-e30234bd-088d-4066-845c-2e4bb3ed556e.png)
