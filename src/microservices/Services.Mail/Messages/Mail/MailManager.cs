using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Services.Mail.Messages.Mail
{
    public class MailManager : IMailManager
    {
        private readonly SmtpClient _client;
        private const string From = "vipvisitormanagment@gmail.com";
        
        
        public MailManager()
        {
            _client = new SmtpClient
            {
                Port = 587,
                Host = "smtp.gmail.com",
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential
                {
                    UserName = From,
                    Password = "-Vi$123-"
                },
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true
            };
        }
        
        public async Task SendAsync(string subject, string content, string to)
        {
            var message = new MailMessage(From, to);
            message.IsBodyHtml = true;
            message.Subject = subject;
            message.Body = "<html><head> <meta name=\"viewport\" content=\"width=device-width, initial-scale=1, shrink-to-fit=no\">    <meta name=\"description\" content=\"\">    <meta name=\"author\" content=\"\">    <link rel=\"icon\" href=\"/docs/4.0/assets/img/favicons/favicon.ico\">    <title>Sticky Footer Template for Bootstrap</title>    " +
                           "<link rel=\"canonical\" href=\"https://getbootstrap.com/docs/4.0/examples/sticky-footer/\">    <link rel=\"stylesheet\" href=\"https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css\" integrity=\"sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh\" crossorigin=\"anonymous\">    " +
                           "<link href=\"sticky-footer.css\" rel=\"stylesheet\">  </head>  <body cz-shortcut-listen=\"true\">" +
                           "<main role=\"main\" class=\"container\"> <h1 class=\"mt-5\">" + subject + "</h1> <p class=\"lead\">" + content + "</p>    </main>    " +
                           "<footer class=\"footer\">      <div class=\"container\">        <span class=\"text-muted\">VIP Visitor Management Systems</span>      </div>    </footer></body></html>";

            try
            {
                await _client.SendMailAsync(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}