using System.Collections.Generic;
using System.Linq;
using Jericho.Core.Commands;

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
            var rules = _ruleFactory.Create(typeof(IRule<TCommandMessage>));
            var messagesRules = rules.Cast<IRule<TCommandMessage>>();
            return messagesRules.Select(messagesRule => messagesRule.Validate(commandMessage));
        }
    }
}