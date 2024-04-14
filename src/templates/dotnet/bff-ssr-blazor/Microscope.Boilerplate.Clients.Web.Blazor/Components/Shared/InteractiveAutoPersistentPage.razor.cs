using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Microscope.Boilerplate.Clients.Web.Blazor.Components.Shared;

public abstract class InteractiveAutoPersistentPage : ComponentBase, IDisposable
{
    [Inject] protected PersistentComponentState ApplicationState { get; set; }
    
    private readonly IList<PersistingComponentStateSubscription> _subscriptions = new List<PersistingComponentStateSubscription>();

    protected async Task<T> GetPersistentState<T>(string key, Func<Task<T?>> addState)
    {
        var data = default(T?);
        
        _subscriptions.Add(ApplicationState.RegisterOnPersisting(() =>
        {
            ApplicationState.PersistAsJson(key, data);
            return Task.CompletedTask;
        }, RenderMode.InteractiveAuto));

        if (ApplicationState.TryTakeFromJson(key, out T? storedData))
        {
            data = storedData;
        }
        else
        {
            data = await addState.Invoke();
        }
        
        return data;
    }
    
    public void Dispose()
    {
        foreach (var subscription in _subscriptions)    
        {
            subscription.Dispose();
        }
    }
}