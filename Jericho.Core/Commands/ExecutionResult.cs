using System.Collections.Generic;
using System.Linq;

namespace Jericho.Core.Commands
{
    public class ExecutionResult
    {
        object _result;
        public bool Successful { get { return !Errors.Any(); } }
        public List<Error> Errors { get; set; }

        public ExecutionResult()
        {
            Errors = new List<Error>();
        }

        public void SetExecutionResult(object result)
        {
            _result = result;
        }

        public T Result<T>() where T : class
        {
            return _result as T;
        }

        public void Merge(IEnumerable<ExecutionResult> ruleResults)
        {
            foreach (var error in ruleResults.SelectMany(ruleResult => ruleResult.Errors))
            {
                Errors.Add(error);
            }
        }

        public void Merge(ExecutionResult ruleResults)
        {
            foreach (var error in ruleResults.Errors)
            {
                Errors.Add(error);
            }
        }
    }
}