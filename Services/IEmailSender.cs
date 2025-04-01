

namespace InsuranceWebApp.Services
{
    public interface IEmailSender
    {
        bool SendMail(MailData Mail_Data);
    }
}
