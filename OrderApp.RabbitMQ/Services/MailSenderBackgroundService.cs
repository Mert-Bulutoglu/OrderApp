using Microsoft.Extensions.Configuration;
using OrderApp.Repository.SMTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace OrderApp.RabbitMQ.Services
{
    public class MailSenderBackgroundService : IMailSenderBackgroundService
    {
        private readonly IConfiguration _config;
        private readonly IConnection _rabbitMQConnection;

        public MailSenderBackgroundService(IConfiguration config)
        {
            _config = config;
            _rabbitMQConnection = CreateRabbitMQConnection();
        }
        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMailAsync(new[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {

            using (var channel = _rabbitMQConnection.CreateModel())
            {
                string queueName = _config["RabbitMQConfiguration:QueueName"];
                channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                var messageBody = Encoding.UTF8.GetBytes("Order successfully completed.");
                channel.BasicPublish("", queueName, properties, messageBody);
               
            }

            MailMessage mail = new();
            mail.IsBodyHtml = isBodyHtml;
            foreach (var to in tos)
                mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.From = new(_config["Mail:Username"], _config["Mail:Webname"], System.Text.Encoding.UTF8);

            SmtpClient smtp = new();
            smtp.Credentials = new NetworkCredential(_config["Mail:Username"], _config["Mail:Password"]);
            smtp.Port = Convert.ToInt32(_config["Mail:Port"]);
            smtp.EnableSsl = true;
            smtp.Host = _config["Mail:Host"];
            await smtp.SendMailAsync(mail);

        }


        public async Task SendSuccessMailAsync(string to)
        {
            StringBuilder mail = new();
            mail.AppendLine("Hello<br><br>Order successfully completed.</br><br><br>");
            mail.AppendLine("<br><br><span style=\"font-size:12px;\">NOTE: If this request has not been fulfilled by you, please do not take this e-mail seriously.</span><br/><br/><br>Kind Regards...<br><br><br>Order App<br/><br/><br/><br/>");
            await SendMailAsync(to, "Nice!", mail.ToString());
        }

        private IConnection CreateRabbitMQConnection()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _config["RabbitMQConfiguration:HostName"],
                UserName = _config["RabbitMQConfiguration:UserName"],
                Password = _config["RabbitMQConfiguration:Password"]
            };

            return factory.CreateConnection();
        }
    }
}
