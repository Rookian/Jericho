using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Jericho.Core.Domain;

namespace Jericho.MVC.Models
{
    public class EmployeeViewModel 
    {

        //[Remote("IsEmployeeUnique", "Home", "FirstName")]
        [Required(ErrorMessage = "Last name is required.")]
        public virtual string LastName { get; set; }

        //[Remote("IsEmployeeUnique", "Home", "LastName")]
        [Required(ErrorMessage = "First name is required.")]
        public virtual string FirstName { get; set; }
        
        [Required(ErrorMessage = "E-mail is required.")]
        [Remote("IsMailUnique", "Home")]
        public virtual string EMail { get; set; }

        public virtual string Infos { get; set; }
        public int Id { get; set; }

        public EmployeeViewModel()
        {
            
        }

        public EmployeeViewModel(Employee employee)
        {
            LastName = employee.LastName;
            FirstName = employee.FirstName;
            EMail = employee.EMail;
            Id = employee.Id;
            Infos = employee.Infos;
        }
    }
}