using Web.UserAgent.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.UserAgent
{
    public interface IUserAgentParser
    {
        ClientInfo Parse(string uaString);
    }
}
