using Jericho.Core.Commands;

namespace Jericho.MVC.Models
{
    public class EmployeeViewModel : ICommandMessage
    {
        public virtual string LastName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string EMail { get; set; }
        public virtual string Infos { get; set; } 
    }
}