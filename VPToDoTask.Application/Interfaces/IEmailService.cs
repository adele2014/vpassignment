using System.Threading.Tasks;
using VPToDoTask.Application.DTOs.Email;

namespace VPToDoTask.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}