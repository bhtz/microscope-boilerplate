# Microscope.Boilerplate Scheduler

## Run aspire
    cd IAC/Aspire/Microscope.Boilerplate.IAC.Aspire
    dotnet run

## Add migration
    dotnet ef migrations add TickerQInitialCreate --context TickerQDbContext

## Apply migration
    dotnet ef database update --context TickerQDbContext
