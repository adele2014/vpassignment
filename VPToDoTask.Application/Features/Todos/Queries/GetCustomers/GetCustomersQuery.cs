using AutoMapper;
using MediatR;
using VPToDoTask.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VPToDoTask.Application.Interfaces;
using VPToDoTask.Application.Interfaces.Repositories;
using VPToDoTask.Application.Parameters;
using VPToDoTask.Application.Wrappers;

namespace VPToDoTask.Application.Features.Todos.Queries.GetTodos
{
    public class GetTodosQuery : QueryParameter, IRequest<PagedResponse<IEnumerable<Entity>>>
    {
        public string Name { get; set; }
        public string ContactName { get; set; }
    }

    public class GetAllTodosQueryHandler : IRequestHandler<GetTodosQuery, PagedResponse<IEnumerable<Entity>>>
    {
        private readonly ITodoRepositoryAsync _positionRepository;
        private readonly IMapper _mapper;
        private readonly IModelHelper _modelHelper;

        public GetAllTodosQueryHandler(ITodoRepositoryAsync positionRepository, IMapper mapper, IModelHelper modelHelper)
        {
            _positionRepository = positionRepository;
            _mapper = mapper;
            _modelHelper = modelHelper;
        }

        public async Task<PagedResponse<IEnumerable<Entity>>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
        {

            var validFilter = request;
            //filtered fields security
            if (!string.IsNullOrEmpty(validFilter.Fields))
            {
                //limit to fields in view model
                validFilter.Fields = _modelHelper.ValidateModelFields<GetTodosViewModel>(validFilter.Fields);
            }
            if (string.IsNullOrEmpty(validFilter.Fields))
            {
                //default fields from view model
                validFilter.Fields = _modelHelper.GetModelFields<GetTodosViewModel>();
            }
            // query based on filter
            var entityTodos = await _positionRepository.GetPagedTodoReponseAsync(validFilter);
            var data = entityTodos.data;
            RecordsCount recordCount = entityTodos.recordsCount;
            // response wrapper
            return new PagedResponse<IEnumerable<Entity>>(data, validFilter.PageNumber, validFilter.PageSize, recordCount);
        }
    }
}