using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VPToDoTask.Application.Features.Todos.Queries.GetTodos;
using VPToDoTask.Application.Interfaces;
using VPToDoTask.Application.Interfaces.Repositories;
using VPToDoTask.Application.Parameters;
using VPToDoTask.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using VPToDoTask.Infrastructure.Shared.Services;
using System;
using VPToDoTask.Infrastructure.Persistence.Contexts;
using VPToDoTask.Infrastructure.Persistence.Repository;

namespace VPToDoTask.Infrastructure.Persistence.Repositories
{
    public class TodoRepositoryAsync : GenericRepositoryAsync<Todo>, ITodoRepositoryAsync
    {
        private readonly DbSet<Todo> _repository;
        private readonly IDataShapeHelper<Todo> _dataShaper;
        private readonly IMockService _mockData;


        public TodoRepositoryAsync(ApplicationDbContext dbContext,
            IDataShapeHelper<Todo> dataShaper, 
            IMockService mockData, 
            ILogger<TodoRepositoryAsync> logger) : base(dbContext)
        {
            _repository = dbContext.Set<Todo>();
            _dataShaper = dataShaper;
            _mockData = mockData;
        }

        public async Task<bool> IsUniqueTodoNumberAsync(string Name)
        {
            return await _repository
                .AllAsync(p => p.Name != Name);
        }

        public async Task<bool> SeedDataAsync(int rowCount)
        {
            // Create an instance of the Random class
            Random rnd = new Random();

            // Generate a random integer between 0 and 99
            int randomNumber = rnd.Next(1000000);
            // Generate seed data with Bogus
            var databaseSeeder = new DatabaseSeeder(rowCount, randomNumber);


            //await this.BulkInsertAsync(_mockData.GetTodos(rowCount));
            await this.BulkInsertAsync(databaseSeeder.Todos);
            return true;
        }

        public async Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedTodoReponseAsync(GetTodosQuery requestParameters)
        {
            var Name = requestParameters.Name;
            var contactName = requestParameters.ContactName;

            var pageNumber = requestParameters.PageNumber;
            var pageSize = requestParameters.PageSize;
            var orderBy = requestParameters.OrderBy;
            var fields = requestParameters.Fields;

            int recordsTotal, recordsFiltered;

            // Setup IQueryable
            var result = _repository
                .AsNoTracking()
                .AsExpandable();

            // Count records total
            recordsTotal = await result.CountAsync();

            // filter data
            FilterByColumn(ref result, Name, contactName);

            // Count records after filter
            recordsFiltered = await result.CountAsync();

            //set Record counts
            var recordsCount = new RecordsCount
            {
                RecordsFiltered = recordsFiltered,
                RecordsTotal = recordsTotal
            };

            // set order by
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                result = result.OrderBy(orderBy);
            }
            // projection
           // result = result.Include(Todo => Todo.Orders).ThenInclude(order => order.OrderItems).ThenInclude(product => product.Product);

            // select columns
            if (!string.IsNullOrWhiteSpace(fields))
            {
                result = result.Select<Todo>("new(" + fields + ")");
            }


            // paging
            result = result
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            // retrieve data to list
            var resultData = await result.ToListAsync();


            // shape data
            var shapeData = _dataShaper.ShapeData(resultData, fields);

            return (shapeData, recordsCount);

        }

        private void FilterByColumn(ref IQueryable<Todo> query, string Name, string status)
        {
            if (!query.Any())
                return;

            if (string.IsNullOrEmpty(status) && string.IsNullOrEmpty(Name))
                return;

            var predicate = PredicateBuilder.New<Todo>();

            if (!string.IsNullOrEmpty(Name))
                predicate = predicate.Or(p => p.Name.Contains(Name.Trim()));

            if (!string.IsNullOrEmpty(status))
                predicate = predicate.Or(p => p.Status.Contains(status.Trim()));

            query = query.Where(predicate);
        }
    }
}