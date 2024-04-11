using MediatR;
using VPToDoTask.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;
using VPToDoTask.Application.Exceptions;
using VPToDoTask.Application.Interfaces.Repositories;
using VPToDoTask.Application.Wrappers;

namespace VPToDoTask.Application.Features.Todos.Commands.UpdateTodo
{
    public class UpdateTodoCommand : Todo, IRequest<Response<Guid>>
    {
        public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand, Response<Guid>>
        {
            private readonly ITodoRepositoryAsync _repository;

            public UpdateTodoCommandHandler(ITodoRepositoryAsync repository)
            {
                _repository = repository;
            }

            public async Task<Response<Guid>> Handle(UpdateTodoCommand command, CancellationToken cancellationToken)
            {
                var todo = await _repository.GetByIdAsync(command.Id);

                if (todo == null)
                {
                    throw new ApiException($"TO DO Not Found.");
                }
                else
                {
                    todo.Name = command.Name;
                    todo.Description = command.Description;
                    todo.DeadLine = command.DeadLine;
                    todo.Status = command.Status;
                    await _repository.UpdateAsync(todo);
                    return new Response<Guid>(todo.Id);
                }
            }
        }
    }
}