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

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<bool>> UpdateTodoList([FromRoute]Guid id, UpdateTodoListCommand command)
    {
        if (id != command.TodoListId)
        {
            return BadRequest();
        }
        
        return await _mediator.Send(command);
    }
    
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<bool>> DeleteTodoList([FromRoute]Guid id, [FromRoute]Guid itemId, [FromBody]DeleteTodoListCommand command)
    {
        if (id != command.TodoListId)
        {
            return BadRequest();
        }
        
        return await _mediator.Send(command);
    }
    
    [HttpPost]
    [Route("{id}/items")]
    public async Task<Guid> AddTodoItem(CreateTodoItemCommand command)
    {
        return await _mediator.Send(command);
    }
    
    [HttpPut]
    [Route("{id}/items/{itemId}")]
    public async Task<ActionResult<bool>> ToggleTodoItem([FromRoute]Guid id, [FromRoute]Guid itemId, [FromBody]ToggleTodoItemCommand command)
    {
        if (id != command.TodoListId || itemId != command.TodoItemId)
        {
            return BadRequest();
        }
        
        return await _mediator.Send(command);
    }
    
    [HttpDelete]
    [Route("{id}/items/{itemId}")]
    public async Task<ActionResult<bool>> DeleteTodoItem([FromRoute]Guid id, [FromRoute]Guid itemId, [FromBody]DeleteTodoItemCommand command)
    {
        if (id != command.TodoListId || itemId != command.TodoItemId)
        {
            return BadRequest();
        }
        
        return await _mediator.Send(command);
    }
}