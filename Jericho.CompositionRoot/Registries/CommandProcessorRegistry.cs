﻿using System;
using System.Collections.Generic;
using System.Linq;
using Jericho.CommandProcessor;
using Jericho.Core.Commands;
using StructureMap;
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
            
            For<Func<Type, IEnumerable<ICommandHandler>>>().Use(type => ObjectFactory.GetAllInstances(type).Cast<ICommandHandler>());
            For<ICommandProcessor>().Use<CommandProcessor.CommandProcessor>();
            SetAllProperties(x=>x.OfType<ICommandProcessor>());
        }
    }
}