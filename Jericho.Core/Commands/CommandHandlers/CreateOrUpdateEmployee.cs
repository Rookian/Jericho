using System;
using Jericho.Core.Commands.CommandMessages;
using Jericho.Core.Domain;
using Jericho.Core.Repositories;

namespace Jericho.Core.Commands.CommandHandlers
{
    public class CreateOrUpdateEmployee : ICommandHandler<CreateOrUpdateEmployeeMessage>
    {
        readonly IEmployeeRepository _employeeRepository;

        public CreateOrUpdateEmployee(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public object Execute(CreateOrUpdateEmployeeMessage commandMessage)
        {
            var employee = _employeeRepository.GetById(commandMessage.Id) ?? new Employee();
            
            employee.EMail = commandMessage.EMail;
            employee.FirstName = commandMessage.FirstName;
            employee.Infos = commandMessage.Infos;
            employee.LastName = commandMessage.LastName;

            _employeeRepository.SaveOrUpdate(employee);
            return employee;
        }
    }
}