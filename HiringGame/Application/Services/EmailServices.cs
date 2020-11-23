using System;
using System.Net.Mail;
using System.Threading.Tasks;
using HiringGame.Application.Dtos;
using HiringGame.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace HiringGame.Application.Services
{
    public class EmailServices : IEmailServices
    {
        private readonly IConfiguration _configuration;

        public EmailServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<bool> SendMailAsync(SendMailParamsDto dto)
        {
            var msg = new MailMessage { From = new MailAddress("Web@minapharm.com", "MINAPHARM Web Services") };

            foreach (var to in dto.ToList)
            {
                msg.To.Add(to);
            }

            foreach (var cc in dto.CcList)
            {
                msg.CC.Add(cc);
            }

            msg.Subject = dto.MessageTitle;
            msg.IsBodyHtml = dto.IsHtml;
            msg.Body = dto.MessageBody;

            var smtp = new SmtpClient(_configuration["EmailSetting:Smtp"])
            {
                Credentials = new System.Net.NetworkCredential(_configuration["EmailSetting:UserName"],
                    _configuration["EmailSetting:Password"])
            };

                await smtp.SendMailAsync(msg);
            
            msg.Dispose();

            return true;
        }

    }
}
