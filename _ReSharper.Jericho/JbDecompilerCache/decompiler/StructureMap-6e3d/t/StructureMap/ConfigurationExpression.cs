// Type: StructureMap.ConfigurationExpression
// Assembly: StructureMap, Version=2.6.3.0, Culture=neutral, PublicKeyToken=e60ad81abae3c223
// Assembly location: C:\Development\Jericho\packages\structuremap.2.6.3\lib\StructureMap.dll

using StructureMap.Configuration;
using StructureMap.Configuration.DSL;
using StructureMap.Diagnostics;
using StructureMap.Graph;
using System;
using System.Collections.Generic;
using System.Xml;

namespace StructureMap
{
  public class ConfigurationExpression : Registry
  {
    protected readonly GraphLog _log = new GraphLog();
    private readonly List<Registry> _registries = new List<Registry>();
    protected readonly ConfigurationParserBuilder _parserBuilder;

    public bool IncludeConfigurationFromConfigFile
    {
      set
      {
        this._parserBuilder.UseAndEnforceExistenceOfDefaultFile = value;
      }
    }

    internal ConfigurationExpression()
    {
      this._parserBuilder = new ConfigurationParserBuilder(this._log);
      this._parserBuilder.IgnoreDefaultFile = true;
      this._parserBuilder.PullConfigurationFromAppConfig = false;
      this._registries.Add((Registry) this);
    }

    public void AddRegistry<T>() where T : new(), Registry
    {
      this.AddRegistry((Registry) Activator.CreateInstance<T>());
    }

    public void AddRegistry(Registry registry)
    {
      this._registries.Add(registry);
    }

    public void AddConfigurationFromXmlFile(string fileName)
    {
      this._parserBuilder.IncludeFile(fileName);
    }

    public void AddConfigurationFromNode(XmlNode node)
    {
      this._parserBuilder.IncludeNode(node, "Xml configuration");
    }

    internal PluginGraph BuildGraph()
    {
      return new PluginGraphBuilder(this._parserBuilder.GetParsers(), this._registries.ToArray(), this._log).Build();
    }
  }
}
