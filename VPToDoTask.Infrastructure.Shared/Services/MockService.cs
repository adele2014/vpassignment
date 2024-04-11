using System.Collections.Generic;
using VPToDoTask.Application.Interfaces;
using VPToDoTask.Infrastructure.Shared.Mock;
using VPToDoTask.Domain.Entities;

namespace VPToDoTask.Infrastructure.Shared.Services
{
    public class MockService : IMockService
    {


       
        /// <summary>
        /// Generates a list of cusotmer using the TodoInsertBogusConfig class.
        /// </summary>
        /// <param name="rowCount">The number of Todos to generate.</param>
        /// <returns>A list of generated positions.</returns>
        public List<Todo> GetTodos(int rowCount)
        {
            var faker = new TodoInsertBogusConfig();
            return faker.Generate(rowCount);
        }

       
    }
}