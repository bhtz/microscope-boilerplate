using Marten;
using MediatR;
using Microscope.Boilerplate.Framework.Infrastructure.Persistence.Marten;
using Microscope.Boilerplate.Todo.Domain;
using Microscope.Boilerplate.Todo.Slices;
using Microsoft.Extensions.DependencyInjection;

namespace Microscope.Management.Todo.Infrastructure.Persistence.Marten;

public class TodoMartenUnitOfWork([FromKeyedServices(nameof(ITodoModule))] IDocumentSession session, IMediator mediator) : MartenUnitOfWork(session, mediator), ITodoUnitOfWork;