using Marten;
using MediatR;
using Microscope.Management.Framework.Infrastructure.Persistence.Marten;
using Microscope.Management.Todo.Domain;

namespace Microscope.Management.Todo.Infrastructure.Persistence.Marten;

public class TodoMartenUnitOfWork(IDocumentSession session, IMediator mediator) : MartenUnitOfWork(session, mediator), ITodoUnitOfWork
{
    
}