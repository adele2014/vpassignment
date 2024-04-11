using FluentValidation;
using System.Threading;
using System.Threading.Tasks;
using VPToDoTask.Application.Interfaces.Repositories;

namespace VPToDoTask.Application.Features.Todos.Commands.CreateTodo
{
    public class CreateTodoCommandValidator : AbstractValidator<CreateTodoCommand>
    {
        private readonly ITodoRepositoryAsync repository;

        public CreateTodoCommandValidator(ITodoRepositoryAsync repository)
        {
            this.repository = repository;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.")
                .MustAsync(IsUniqueTodoNumber).WithMessage("{PropertyName} already exists.");

            RuleFor(p => p.Status)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
        }

        private async Task<bool> IsUniqueTodoNumber(string TodoNumber, CancellationToken cancellationToken)
        {
            return await repository.IsUniqueTodoNumberAsync(TodoNumber);
        }
    }
}