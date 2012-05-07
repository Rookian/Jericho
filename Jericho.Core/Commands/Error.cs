using System;
using System.Collections.Generic;

namespace Jericho.Core.Commands
{
    public class Error
    {
        public Exception Exception { get; set; }
        public string ErrorMessage { get; set; }
        public IEnumerable<string> InvalidProperties { get; set; }
    }
}