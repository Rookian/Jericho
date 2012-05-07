using System.Collections.Generic;
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
            var employee = _employeeRepository.GetById(commandMessage.Id) ?? new Employee();
            var isUnique = _employeeRepository.IsUnique(employee);
            if (!isUnique)
            {
                return new ExecutionResult { Errors = new List<Error>(new[] { new Error { ErrorMessage = "Employee is already defined. " } }) };
            }
            return new ExecutionResult();
        }
    }
}