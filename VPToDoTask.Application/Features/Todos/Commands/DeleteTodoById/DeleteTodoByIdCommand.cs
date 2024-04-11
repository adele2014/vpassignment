using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using VPToDoTask.Application.Exceptions;
using VPToDoTask.Application.Interfaces.Repositories;
using VPToDoTask.Application.Wrappers;

namespace VPToDoTask.Application.Features.Todos.Commands.DeleteTodoById
{
    public class DeleteTodoByIdCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }

        public class DeleteTodoByIdCommandHandler : IRequestHandler<DeleteTodoByIdCommand, Response<Guid>>
        {
            private readonly ITodoRepositoryAsync _repository;

            public DeleteTodoByIdCommandHandler(ITodoRepositoryAsync repository)
            {
                _repository = repository;
            }

            public async Task<Response<Guid>> Handle(DeleteTodoByIdCommand command, CancellationToken cancellationToken)
            {
                var Todo = await _repository.GetByIdAsync(command.Id);
                if (Todo == null) throw new ApiException($"TO DO Not Found.");
                await _repository.DeleteAsync(Todo);
                return new Response<Guid>(Todo.Id);
            }
        }
    }
}