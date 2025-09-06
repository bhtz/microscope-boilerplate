using Microscope.Boilerplate.Todo.Domain.TodoListAggregate;
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Repositories;

namespace Microscope.Boilerplate.Todo.Slices.Features.CreateTodoList;

public class CreateTodoListCommandHandler(
    IUnitOfWork unitOfWork,
    ITodoListRepository todoListRepository,
    IIdentityService identityService) : IRequestHandler<CreateTodoListCommand, Guid>
{
    public async Task<Guid> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
    {
        var userId = identityService.GetUserId();
        var userMail = identityService.GetUserMail();
        var tenantId =  identityService.GetTenantId();

        var todoList = TodoList.Create(tenantId, Guid.NewGuid(), userId, userMail, request.Name);
         
        await todoListRepository.AddAsync(todoList);
        await unitOfWork.SaveChangesAndDispatchEventsAsync(cancellationToken);

        return todoList.Id;
    }
}