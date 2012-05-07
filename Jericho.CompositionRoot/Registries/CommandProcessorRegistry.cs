using Jericho.CommandProcessor;
using Jericho.Core.Commands;
using StructureMap.AutoFactory;
using StructureMap.Configuration.DSL;

namespace Jericho.CompositionRoot.Registries
{
    public class CommandProcessorRegistry : Registry
    {
        public CommandProcessorRegistry()
        {
            Scan(scan =>
            {
                scan.AssembliesFromApplicationBaseDirectory();
                scan.ConnectImplementationsToTypesClosing(typeof(ICommandHandler<>));
            });

            For<ICommandInvoker>().Use<CommandInvoker>();
            For<ICommandHandlerFactory>().CreateFactory();
            For<ICommandProcessor>().Use<CommandProcessor.CommandProcessor>();
            SetAllProperties(x=>x.OfType<ICommandProcessor>());
        }
    }
}