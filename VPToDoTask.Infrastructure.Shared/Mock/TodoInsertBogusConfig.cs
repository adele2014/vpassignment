using Bogus;
using VPToDoTask.Domain.Entities;
using System;


namespace VPToDoTask.Infrastructure.Shared.Mock
{
    public class TodoInsertBogusConfig : Faker<Todo>
    {
        public TodoInsertBogusConfig()
        {
            RuleFor(o => o.Id, f => Guid.NewGuid());
            RuleFor(o => o.Name, f => f.Name.JobType());
            RuleFor(o => o.Created, f => f.Date.Past(1));
            RuleFor(o => o.CreatedBy, f => f.Name.FullName());
            RuleFor(o => o.LastModified, f => f.Date.Recent(1));
            RuleFor(o => o.LastModifiedBy, f => f.Name.FullName());
        }
    }
}
