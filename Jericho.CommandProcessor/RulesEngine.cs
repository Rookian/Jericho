using System.Collections.Generic;
using System.Linq;
using Jericho.Core.Commands;
using Jericho.Core.Rules;

namespace Jericho.CommandProcessor
{
    public class RulesEngine : IRulesEngine
    {
        readonly IRuleFactory _ruleFactory;

        public RulesEngine(IRuleFactory ruleFactory)
        {
            _ruleFactory = ruleFactory;
        }

        public IEnumerable<ExecutionResult> ValidateMessage<TCommandMessage>(TCommandMessage commandMessage) where TCommandMessage : ICommandMessage
        {
            var rules = _ruleFactory.Create(typeof(IRuleFor<TCommandMessage>));
            var messagesRules = rules.Cast<IRuleFor<TCommandMessage>>();

            return messagesRules.Select(messagesRule => messagesRule.Validate(commandMessage));
        }
    }
}