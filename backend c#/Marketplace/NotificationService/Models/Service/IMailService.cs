using NotificationService.Models.Entity;

namespace NotificationService.Models.Service
{
    public interface IMailService
    {
        Task<bool> SendAsync(MailData mailData);
    }
}
