using Jericho.Core.Commands.CommandMessages;
using Jericho.Core.Domain;

namespace Jericho.Core.Commands.CommandHandlers
{
    public class TestCommandHandler : ICommandHandler<CreateOrUpdateEmployeeMessage>
    {
        public object Execute(CreateOrUpdateEmployeeMessage commandMessage)
        {
            return new Employee();
        }
    }
}