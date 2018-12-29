using MitigatingCircumstances.Models;

namespace MitigatingCircumstances.Services.Interface
{
    public interface IMailService
    {
        void SendInboundEmailExtensionCreated(string toEmail, string toName, ExtensionRequest extensionRequest);

        void SendInboundEmailDoesntExistEmail(string toEmail, string toName, string extensionRequestTitle);

        void SendTeacherCreatedNotificationEmail(ApplicationUser teacher, ApplicationUser student, ExtensionRequest extensionRequest);
    }
}