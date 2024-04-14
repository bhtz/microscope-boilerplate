namespace Microscope.Boilerplate.Services.TodoApp.Application.Services;

public interface IBusService
{
    // TODO:  make business oriented instead generic name
    public Task Publish<T>(T message) where T : class;
    
    // public Task Send(object data);
}