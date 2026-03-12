---
apply: always
---

# Blazor Coding Rules

Spec-driven development rules for Blazor components, services, and UI interactions.

## Architecture Principles

- **Shared Components**: Generic components in `Shared/Components`, UI-specific in theme projects
- **Services**: Business logic in `Shared/Services`, UI state in component code
- **GraphQL First**: Use generated GraphQL clients for data fetching
- **Material Design**: Use MudBlazor (Material) in Material project
- **Fluent Design**: Use FluentUI (Fluent) in Fluent project
- **Responsive**: Support auto/server rendering modes

## Project Structure

```
src/Clients/
├── Web.Shared/
│   ├── Components/              # Shared generic components
│   ├── Services/                # Shared business logic services
│   ├── Models/                  # Shared DTOs and models
│   └── Generated/               # Generated GraphQL clients
├── Web.Blazor.Material/         # MudBlazor-specific UI
│   ├── Components/              # Material theme components
│   ├── Layouts/                 # Layout components
│   └── Pages/                   # Page components
├── Web.Blazor.Fluent/           # FluentUI-specific UI
│   ├── Components/              # Fluent theme components
│   ├── Layouts/
│   └── Pages/
```

## Component Development

### Shared Generic Components

Location: `src/Clients/Microscope.Boilerplate.Clients.Web.Shared/Components/`

**Pattern**:
```razor
@* Components/Shared/LeadCard.razor *@
@using Microscope.Boilerplate.Clients.Web.Shared.Models
@using Microscope.Boilerplate.Clients.Web.Shared.Components

<div class="lead-card">
    <h3>@Lead.FirstName @Lead.LastName</h3>
    <p>@Lead.Email</p>
    @if (!string.IsNullOrEmpty(Lead.Phone))
    {
        <p>@Lead.Phone</p>
    }
    <p>Created: @Lead.CreatedAt?.ToLocalTime()</p>
</div>q

@code {
    [Parameter]
    public required Lead Lead { get; set; }

    [Parameter]
    public EventCallback<Lead> OnEdit { get; set; }

    private async Task EditLead() => await OnEdit.InvokeAsync(Lead);
}
```

### Theme-Specific Components

**MudBlazor Example** (`Web.Blazor.Material`):
```razor
@* Components/Leads/LeadCard.razor *@
@using MudBlazor
@using Microscope.Boilerplate.Clients.Web.Shared.Models

<MudCard>
    <MudCardContent>
        <MudText Typo="Typo.h5">@Lead.FirstName @Lead.LastName</MudText>
        <MudText Color="Color.Secondary">@Lead.Email</MudText>
        @if (!string.IsNullOrEmpty(Lead.Phone))
        {
            <MudText>@Lead.Phone</MudText>
        }
        <MudText Typo="Typo.caption">Created: @Lead.CreatedAt?.ToLocalTime()</MudText>
    </MudCardContent>
    <MudCardActions>
        <MudButton Variant="Variant.Text" Color="Color.Primary" OnClick="EditLead">Edit</MudButton>
        <MudButton Variant="Variant.Text" Color="Color.Error" OnClick="DeleteLead">Delete</MudButton>
    </MudCardActions>
</MudCard>

@code {
    [Parameter]
    public required Lead Lead { get; set; }

    [Parameter]
    public EventCallback<Lead> OnEdit { get; set; }

    [Parameter]
    public EventCallback<Lead> OnDelete { get; set; }

    private async Task EditLead() => await OnEdit.InvokeAsync(Lead);
    private async Task DeleteLead() => await OnDelete.InvokeAsync(Lead);
}
```

## Page Development

Location: `src/Clients/Microscope.Boilerplate.Clients.Web.Blazor.{Theme}/Components/Pages/`

**Pattern**:
```razor
@* Pages/Leads/LeadsList.razor *@
@page "/leads"
@using Microscope.Boilerplate.Clients.Web.Shared.Models
@using Microscope.Boilerplate.Clients.Web.Shared.Services
@inject GatewayClient GatewayClient

<PageTitle>Leads</PageTitle>

<MudContainer MaxWidth="MaxWidth.Large" Class="py-8">
    <MudStack Spacing="2">
        <MudStack Row="true" Justify="Justify.SpaceBetween" AlignItems="AlignItems.Center">
            <MudText Typo="Typo.h4">Leads</MudText>
            <MudButton Variant="Variant.Filled" Color="Color.Primary" OnClick="OpenCreateDialog">New Lead</MudButton>
        </MudStack>

        @if (IsLoading)
        {
            <MudProgressCircular IsIndeterminate="true" />
        }
        else if (Leads == null || !Leads.Any())
        {
            <MudText>No leads found.</MudText>
        }
        else
        {
            <MudGrid>
                @foreach (var lead in Leads)
                {
                    <MudItem xs="12" sm="6" md="4">
                        <LeadCard Lead="lead" OnEdit="OpenEditDialog" OnDelete="DeleteLead" />
                    </MudItem>
                }
            </MudGrid>
        }
    </MudStack>
</MudContainer>

@code {
    private IEnumerable<Lead>? Leads;
    private bool IsLoading = true;

    protected override async Task OnInitializedAsync()
    {
        await LoadLeads();
    }

    private async Task LoadLeads()
    {
        IsLoading = true;
        try
        {
            Leads = await GatewayClient.GetLeadsAsync();
        }
        finally
        {
            IsLoading = false;
        }
    }

    private void OpenCreateDialog() { }
    private void OpenEditDialog(Lead lead) { }
    private async Task DeleteLead(Lead lead) { }
}
```

## Routing

- **Route Parameters**: Use `@page` directive with constraints
- **Navigation**: Use `NavigationManager` for programmatic navigation
- **Layout**: Use `@layout` directive to specify layout components

## Code Quality Standards

- **Naming**: Use PascalCase for components, camelCase for parameters
- **Component Parameters**: Use `[Parameter]` attribute, prefer `required` keyword
- **Event Callbacks**: Use `EventCallback<T>` for component communication
- **Async**: Use async patterns with proper error handling
- **CSS**: Use CSS modules or scoped styles with `.razor.css` files
- **Comments**: Document complex component logic