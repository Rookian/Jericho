using System;

namespace Jericho.Core.Commands
{
    public class Error
    {
        public Exception Exception { get; set; }
        public string ErrorMessage { get; set; }
    }
}