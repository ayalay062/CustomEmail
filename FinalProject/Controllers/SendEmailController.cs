using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Servieces;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class SendEmailController:ControllerBase
    {
        ISendEmailService sendEmailService;
        public SendEmailController(ISendEmailService sendEmailService)
        {
            this.sendEmailService = sendEmailService;
        }
        [HttpPost("send")]
        [EnableCors("CorsPolicy")]
        public bool send([FromBody] dynamic mail)
        {
            return this.sendEmailService.send(mail);
        }
    }
}
