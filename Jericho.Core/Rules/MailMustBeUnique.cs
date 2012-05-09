using Jericho.Core.Commands;
using Jericho.Core.Commands.CommandMessages;
using Jericho.Core.Repositories;

namespace Jericho.Core.Rules
{
    public class MailMustBeUnique : IRuleFor<CreateOrUpdateEmployeeMessage>
    {
        readonly IEmployeeRepository _employeeRepository;

        public MailMustBeUnique(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public ExecutionResult Validate(CreateOrUpdateEmployeeMessage commandMessage)
        {
            var executionResult = new ExecutionResult();
            var isMailUnique = _employeeRepository.IsUnique(commandMessage.Id, x => x.EMail == commandMessage.EMail);

            if (!isMailUnique)
            {
                executionResult.Errors.Add(new Error { ErrorMessage = "Mail is already defined.", InvalidProperties = new[] { Reflector.GetPropertyName<CreateOrUpdateEmployeeMessage>(x => x.EMail) } });
            }
            return executionResult;
        }
    }
}