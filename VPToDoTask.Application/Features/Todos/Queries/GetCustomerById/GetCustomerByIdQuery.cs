using MediatR;
using VPToDoTask.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;
using VPToDoTask.Application.Exceptions;
using VPToDoTask.Application.Interfaces.Repositories;
using VPToDoTask.Application.Wrappers;

namespace VPToDoTask.Application.Features.Todos.Queries.GetTodoById
{
    public class GetTodoByIdQuery : IRequest<Response<Todo>>
    {
        public Guid Id { get; set; }

        public class GetTodoByIdQueryHandler : IRequestHandler<GetTodoByIdQuery, Response<Todo>>
        {
            private readonly ITodoRepositoryAsync _positionRepository;

            public GetTodoByIdQueryHandler(ITodoRepositoryAsync positionRepository)
            {
                _positionRepository = positionRepository;
            }

            public async Task<Response<Todo>> Handle(GetTodoByIdQuery query, CancellationToken cancellationToken)
            {
                var position = await _positionRepository.GetByIdAsync(query.Id);
                if (position == null) throw new ApiException($"TO DO Not Found.");
                return new Response<Todo>(position);
            }
        }
    }
}