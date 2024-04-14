using AutoMapper;
using MediatR;
using Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Queries.GetTodoLists;
using Microscope.Boilerplate.Services.TodoApp.Application.Services;
using Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.Events;
using Microscope.SharedKernel;

namespace Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Events;

public class SendMailOnTodoListCompletedEventHandler : INotificationHandler<OnTodoListCompletedEvent>
{
    private readonly IMailService _mailService;
    private readonly IMapper _mapper;
    
    public SendMailOnTodoListCompletedEventHandler(IMailService mailService, IMapper mapper)
    {
        _mailService = mailService;
        _mapper = mapper;
    }
    
    public async Task Handle(OnTodoListCompletedEvent notification, CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<GetTodoListQueryResult>(notification.TodoList);
        
        if (notification.TodoList.CreatorMail != null)
            await _mailService.SendTodoListCompletedMail(notification.TodoList.CreatorMail, dto);
    }
}