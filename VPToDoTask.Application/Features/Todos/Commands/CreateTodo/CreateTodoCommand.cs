using AutoMapper;
using MediatR;
using VPToDoTask.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using VPToDoTask.Application.Interfaces.Repositories;
using VPToDoTask.Application.Wrappers;

namespace VPToDoTask.Application.Features.Todos.Commands.CreateTodo
{

    public partial class CreateTodoCommand : Todo, IRequest<Response<Todo>>
    {
    }

    public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, Response<Todo>>
    {
        private readonly ITodoRepositoryAsync _repository;
        private readonly IMapper _mapper;

        public CreateTodoCommandHandler(ITodoRepositoryAsync repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<Todo>> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            var Todo = _mapper.Map<Todo>(request);
            await _repository.AddAsync(Todo);
            return new Response<Todo>(Todo);
        }
    }


}