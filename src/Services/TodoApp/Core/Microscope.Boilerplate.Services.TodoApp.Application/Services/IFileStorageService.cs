namespace Microscope.Boilerplate.Services.TodoApp.Application.Services;

public interface IFileStorageService
{
    Task UploadTodoFileAsync(string blobName, Stream data);
}
