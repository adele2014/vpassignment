using System.Collections.Generic;
using VPToDoTask.Domain.Entities;

namespace VPToDoTask.Application.Interfaces
{
    public interface IMockService
    {
     

        List<Todo> GetTodos(int rowCount);


      
    }
}