using SendGrid;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DotNetAppSqlDb.Helpers.SendgridEmailServiceHelper
{
    public class EmailSenderService: IEmailSenderService
    {
        private ISendGridClient client;
        public EmailSenderService()
        {
            this.client = (ISendGridClient)MvcApplication.serviceProvider.GetService(typeof(ISendGridClient));
        }
        private async Task SendMessageAsync(SendGridMessage message, List<EmailAddress> addresses)
        {
            
            message.AddTos(addresses);
            Response response = await client.SendEmailAsync(message);
            if(response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                Trace.TraceError(response.StatusCode + "SendGrid failures");
            }
        }

        public async Task SendRegistrationConfirmation(string Name, EmailAddress address)
        {
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("adprakas@microsoft.com"),
                Subject = "Welcome to PX-PACE Chess Tournament 2019 - Giving Campaign",
                PlainTextContent = "Thanks, " + Name + "for Registering, " +
                                    "keep visiting https://chesstournament.azurewebsites.net/ " +
                                    "for updates",
                HtmlContent = "<strong> Hello, ChessMasters! <br/> Thanks, " + Name + " for Registering, " +
                                    "keep visiting https://chesstournament.azurewebsites.net/ " +
                                    "for updates </strong>"
            };

            List<EmailAddress> addresses = new List<EmailAddress>();
            addresses.Add(address);
            await SendMessageAsync(msg, addresses);
        }

        public async Task SendFixtureUpdateMessage(List<EmailAddress> addresses)
        {
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("adprakas@microsoft.com"),
                Subject = "Fixture update for PX-PACE Chess Tournament 2019 - Giving Campaign",
                PlainTextContent = "Thanks for Participating, " +
                                    "There is an update on match schedules. " +
                                    "Please visit https://chesstournament.azurewebsites.net/Todos/Index" +
                                    "for viewing the new fixture",

                HtmlContent = "<strong>Hello, ChessMasters! </strong>" + "<br/>" +
                                    "Thanks for Participating, " +
                                    "There is an update on match schedules. " +
                                    "Please visit https://chesstournament.azurewebsites.net/Todos/Index" +
                                    "for viewing the new fixture"
            };

            await SendMessageAsync(msg, addresses);
        }
    }
}