using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Services.Security
{
    public interface ISecurityHandler
    {
        bool ImaPravo(string akcija);
    }
}
