using VPToDoTask.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using VPToDoTask.Application.Features.Todos.Queries.GetTodos;
using VPToDoTask.Application.Parameters;

namespace VPToDoTask.Application.Interfaces.Repositories
{
    /// <summary>
    /// Repository interface for To DO entity with asynchronous methods.
    /// </summary>
    /// <param name="Name">TO DO number to check for uniqueness.</param>
    /// <returns>
    /// Task indicating whether the Todo name is unique.
    /// </returns>
    /// <param name="rowCount">Number of rows to seed.</param>
    /// <returns>
    /// Task indicating the completion of seeding.
    /// </returns>
    /// <param name="requestParameters">Parameters for the query.</param>
    /// <param name="data">Data to be returned.</param>
    /// <param name="recordsCount">Number of records.</param>
    /// <returns>
    /// Task containing the paged response.
    /// </returns>    
    public interface ITodoRepositoryAsync : IGenericRepositoryAsync<Todo>
    {
        Task<bool> IsUniqueTodoNumberAsync(string Name);

        Task<bool> SeedDataAsync(int rowCount);

        Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedTodoReponseAsync(GetTodosQuery requestParameters);
    }
}