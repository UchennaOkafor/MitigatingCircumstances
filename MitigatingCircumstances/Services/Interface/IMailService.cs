namespace MitigatingCircumstances.Services.Interface
{
    public interface IMailService
    {
        void SendEmail(string toEmail, string toName);

        void SendEmail(string toEmail, string toName, string message);
    }
}