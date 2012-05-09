using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Jericho.Core.Repositories;

namespace Jericho.MVC.Models
{
    public class EmployeeViewModel :IValidatableObject
    {
        readonly IEmployeeRepository _employeeRepository;

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

        public EmployeeViewModel(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var exists = _employeeRepository.Exists(x => x.LastName == LastName, x => x.FirstName == FirstName);
            if (exists)
            {
                yield return new ValidationResult("Employee already exists");
            }
        }
    }
}