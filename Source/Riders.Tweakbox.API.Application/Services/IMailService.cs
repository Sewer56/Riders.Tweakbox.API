using System.Threading.Tasks;

namespace Riders.Tweakbox.API.Application.Services
{
    public interface IMailService
    {
        Task SendPasswordResetToken(string email, string userName, string token);
        Task SendConfirmationEmail(string email, string userName);
    }
}