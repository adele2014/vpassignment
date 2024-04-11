using System;
using System.Collections.Generic;
using VPToDoTask.Domain.Common;

namespace VPToDoTask.Domain.Entities
{
    public class Todo : AuditableBaseEntity
    {
        public string Name { get; set; }
       
        public string Description { get; set; }
        public string DeadLine { get; set; }
        public string Status { get; set; }

    }
}
