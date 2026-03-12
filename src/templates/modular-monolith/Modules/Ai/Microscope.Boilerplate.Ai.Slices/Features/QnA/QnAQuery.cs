using FluentValidation;
using MediatR;
using Microsoft.Extensions.AI;

namespace Microscope.Boilerplate.Ai.Slices.Features.QnA;

public record QnAQuery(IEnumerable<ChatMessageDto> Messages) : IRequest<QnAResult>;
public record QnAResult(IEnumerable<ChatMessageDto> Messages);
public record ChatMessageDto(ChatRole Role, string Text);

public class ChatMessageDtoValidator : AbstractValidator<ChatMessageDto>
{
    public ChatMessageDtoValidator()
    {
        RuleFor(v => v.Role)
            .NotNull()
            .NotEmpty();
        
        RuleFor(v => v.Text)
            .NotNull()
            .NotEmpty();
    }
}
