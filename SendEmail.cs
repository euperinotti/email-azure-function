using System;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace euperinotti.azure
{
  class SendEmail
  {
    public static async Task Execute(string email, string content)
    {
      var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
      var client = new SendGridClient(apiKey);
      var from = new EmailAddress("guilherme9115@gmail.com", "Guilherme Perinotti");
      var subject = "Contato de interesse";
      var to = new EmailAddress(email);
      var plainTextContent = $"{email} está interessado. \nMensagem: {content}";
      var htmlContent = $"{email} está interessado. <br>Mensagem: {content}";
      var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
      var response = await client.SendEmailAsync(msg);
    }
  }
}