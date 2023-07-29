using HotChocolate.Execution;
using MediatR;
using Microscope.Boilerplate.Services.TodoApp.Api.GraphQL.Mutations;
using Microscope.Boilerplate.Services.TodoApp.Api.GraphQL.Queries;

namespace Microscope.Boilerplate.Services.TodoApp.Api.Configurations;

public static class GraphQLConfiguration
{
    public static IServiceCollection AddGraphQLConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGraphQLServer()
            .ModifyOptions(o => o.DefaultResolverStrategy = ExecutionStrategy.Serial)
            .AddAuthorization()
            .AddMutationType<Mutation>()
            .AddQueryType<Query>()
            .AddType<UploadType>()
            .AddProjections()
            .AddFiltering()
            .AddSorting();

        return services;
    }
}
