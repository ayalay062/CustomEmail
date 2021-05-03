using System;
using System.Collections.Generic;
using System.Text;

namespace Servieces
{
   public interface ISendEmailService
    {
        bool send(dynamic mail);
    }
}
