using System.Threading.Tasks;

namespace VebLab2.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}