namespace MitigatingCircumstances.Services.Interface
{
    public interface IMailService
    {
        void SendEmail(string toEmail, string message);
    }
}