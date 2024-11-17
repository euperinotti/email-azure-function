using System;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace euperinotti.azure
{
  class SendEmail
  {
    public static async Task Execute(string email)
    {
      var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
      var client = new SendGridClient(apiKey);
      var from = new EmailAddress("guilherme9115@gmail.com", "Guilherme Perinotti");
      var subject = "Sending with SendGrid is Fun";
      var to = new EmailAddress(email);
      var plainTextContent = "and easy to do anywhere, even with C#";
      var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
      var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
      var response = await client.SendEmailAsync(msg);
    }
  }
}