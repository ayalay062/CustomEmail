using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
  public  interface ISendEmailRepository
    {
        bool send(dynamic mail);
    }
}
