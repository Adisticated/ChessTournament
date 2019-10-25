using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetAppSqlDb.Helpers.SendgridEmailServiceHelper
{
    public interface IEmailSenderService
    {
        Task SendRegistrationConfirmation(string Name, EmailAddress address);
        Task SendFixtureUpdateMessage(List<EmailAddress> addresses);

    }
}