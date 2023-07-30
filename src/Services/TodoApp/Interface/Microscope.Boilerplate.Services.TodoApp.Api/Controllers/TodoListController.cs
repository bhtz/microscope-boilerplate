using AspNetCore.Authentication.ApiKey;
using MediatR;
using Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Commands.CreateTodoList;
using Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Queries.GetTodoLists;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Microscope.Boilerplate.Services.TodoApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = ApiKeyDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TodoListController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public TodoListController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoListQueryResult>>> GetTodoLists([FromQuery] string search)
    {
        var res = await _mediator.Send(new GetTodoListQuery());
        return Ok(res);
    }
    
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateTodoList(CreateTodoListCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }
}