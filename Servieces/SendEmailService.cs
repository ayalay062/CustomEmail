using System;
using System.Collections.Generic;
using System.Text;
using Repositories;

namespace Servieces
{
    public class SendEmailService : ISendEmailService
    {
        ISendEmailRepository sendEmailRepository;
        public SendEmailService(ISendEmailRepository sendEmailRepository)
        {
            this.sendEmailRepository = sendEmailRepository;
        }
        public bool send(dynamic mail)
        {
            return sendEmailRepository.send(mail);
        }
    }
}
