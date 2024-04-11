using System;
using VPToDoTask.Application.Interfaces;

namespace VPToDoTask.Infrastructure.Shared.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}