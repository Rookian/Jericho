using Jericho.Core.Commands;
using Jericho.Core.Commands.CommandMessages;
using Jericho.Core.Repositories;

namespace Jericho.Core.Rules
{
    public class EmployeeMustBeUnique : IRuleFor<CreateOrUpdateEmployeeMessage>
    {
        readonly IEmployeeRepository _employeeRepository;

        public EmployeeMustBeUnique(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public ExecutionResult Validate(CreateOrUpdateEmployeeMessage commandMessage)
        {
            var executionResult = new ExecutionResult();
            var isUnique = _employeeRepository.IsUnique(commandMessage.Id, x => x.FirstName == commandMessage.FirstName, x => x.LastName == commandMessage.LastName);

            if (!isUnique)
            {
                executionResult.Errors.Add(new Error
                {
                    ErrorMessage = "Employee is already defined.",
                    InvalidProperties = new[] { Reflector.GetPropertyName<CreateOrUpdateEmployeeMessage>(x => x.FirstName), Reflector.GetPropertyName<CreateOrUpdateEmployeeMessage>(x => x.LastName) }
                });
            }
            return executionResult;
        }
    }
}