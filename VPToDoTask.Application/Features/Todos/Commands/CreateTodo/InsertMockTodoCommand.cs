using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VPToDoTask.Application.Interfaces.Repositories;
using VPToDoTask.Application.Wrappers;

namespace VPToDoTask.Application.Features.Todos.Commands.CreateTodo
{
    public partial class InsertMockTodoCommand : IRequest<Response<int>>
    {
        public int RowCount { get; set; }
    }

    public class SeedTodoCommandHandler : IRequestHandler<InsertMockTodoCommand, Response<int>>
    {
        private readonly ITodoRepositoryAsync _repository;

        public SeedTodoCommandHandler(ITodoRepositoryAsync repository)
        {
            _repository = repository;
        }

        public async Task<Response<int>> Handle(InsertMockTodoCommand request, CancellationToken cancellationToken)
        {
            await _repository.SeedDataAsync(request.RowCount);
            return new Response<int>(request.RowCount);
        }
    }
}