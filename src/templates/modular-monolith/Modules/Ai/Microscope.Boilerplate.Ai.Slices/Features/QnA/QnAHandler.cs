using MediatR;
using Microscope.Boilerplate.Ai.Slices.Services;
using Microsoft.Extensions.AI;

namespace Microscope.Boilerplate.Ai.Slices.Features.QnA;

public class QnAQueryHandler(IChatClient chatClient, IRemoteMcpService remoteMcpService) : IRequestHandler<QnAQuery, QnAResult>
{
    public async Task<QnAResult> Handle(QnAQuery request, CancellationToken cancellationToken)
    {
        const string systemPrompt = "You are a helpful assistant. You will be given a question and you will answer it.";
        
        var chatHistory = new List<ChatMessage>
        {
            new(ChatRole.System, systemPrompt)
        };

        chatHistory.AddRange(request.Messages.Select(message => 
            new ChatMessage(message.Role, message.Text)));
        
        var chatOptions = new ChatOptions
        {
            Tools = new List<AITool>(),
        };
        
        var remoteTools = await remoteMcpService.GetToolsAsync(cancellationToken);
        foreach (var tool in remoteTools)
        {
            chatOptions.Tools?.Add(tool);
        }
        
        var resultPrompt = await chatClient.GetResponseAsync(chatHistory, chatOptions, cancellationToken);
        
        chatHistory.Add(new ChatMessage(ChatRole.Assistant, resultPrompt.Messages.LastOrDefault()?.Text));
        
        var dto = chatHistory
            .Where(x => x.Role != ChatRole.System)
            .Select(x => new ChatMessageDto(x.Role, x.Text));
        
        return new QnAResult(dto);
    }
}