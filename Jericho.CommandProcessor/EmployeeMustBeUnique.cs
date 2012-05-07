using System.Collections.Generic;
using Jericho.Core;
using Jericho.Core.Commands;
using Jericho.Core.Commands.CommandMessages;
using Jericho.Core.Domain;
using Jericho.Core.Repositories;

namespace Jericho.CommandProcessor
{
    public class EmployeeMustBeUnique : IRule<CreateOrUpdateEmployeeMessage>
    {
        readonly IEmployeeRepository _employeeRepository;

        public EmployeeMustBeUnique(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public ExecutionResult Validate(CreateOrUpdateEmployeeMessage commandMessage)
        {
            var isUnique = _employeeRepository.IsUnique(x => x.FirstName == commandMessage.FirstName, x => x.LastName == commandMessage.LastName);

            if (!isUnique)
            {
                return new ExecutionResult
                {
                    Errors = new List<Error>(new[]
                    {
                        new Error
                        {
                            ErrorMessage = "Employee is already defined.", 
                            InvalidProperties = new[]{Reflector.GetPropertyName<CreateOrUpdateEmployeeMessage>(x => x.FirstName), Reflector.GetPropertyName<CreateOrUpdateEmployeeMessage>(x => x.LastName)}
                        }
                    })
                };
            }
            return new ExecutionResult();
        }
    }

    public class MailMustBeUnique : IRule<CreateOrUpdateEmployeeMessage>
    {
        readonly IEmployeeRepository _employeeRepository;

        public MailMustBeUnique(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public ExecutionResult Validate(CreateOrUpdateEmployeeMessage commandMessage)
        {
            var isMailUnique = _employeeRepository.IsUnique(x => x.EMail == commandMessage.EMail);

            if (!isMailUnique)
            {
                return new ExecutionResult
                {
                    Errors = new List<Error>(new[] { new Error { ErrorMessage = "Mail is already defined.", InvalidProperties = new[] { Reflector.GetPropertyName<CreateOrUpdateEmployeeMessage>(x => x.EMail) } } })
                };
            }
            return new ExecutionResult();
        }
    }
}