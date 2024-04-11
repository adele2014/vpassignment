using Bogus;
using VPToDoTask.Domain.Entities;
using VPToDoTask.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VPToDoTask.Infrastructure.Shared.Services
{
    public class DatabaseSeeder
    {
        
        public IReadOnlyCollection<Todo> Todos { get; }



        public DatabaseSeeder(int rowCount=100, int seedValue = 1969)
        {
          
            Todos = GenerateTodos(rowCount, seedValue);

       


        }

      

        private static IReadOnlyCollection<Todo> GenerateTodos(int rowCount, int seedValue)
        {
            var faker = new Faker<Todo>()
                  .UseSeed(seedValue) // Use any number
                  .RuleFor(r => r.Id, f => Guid.NewGuid())
                  .RuleFor(r => r.Name, f => f.Name.FirstName())
                  
                 // .RuleFor(r => r.Phone, f => f.Phone.PhoneNumberFormat().OrNull(f, .15f))
                  //.RuleFor(r => r.Created, f => f.Date.Recent())
                  .RuleFor(r => r.CreatedBy, f => f.Internet.UserName())
                  ;

            return faker.Generate(rowCount);

        }


    }
}
