// Type: StructureMap.Configuration.DSL.SetterConvention
// Assembly: StructureMap, Version=2.6.3.0, Culture=neutral, PublicKeyToken=e60ad81abae3c223
// Assembly location: C:\Development\Jericho\packages\structuremap.2.6.3\lib\StructureMap.dll

using StructureMap.Graph;
using StructureMap.TypeRules;
using System;
using System.Reflection;

namespace StructureMap.Configuration.DSL
{
  public class SetterConvention
  {
    public void OfType<T>()
    {
      this.Matching((Predicate<PropertyInfo>) (prop => prop.PropertyType == typeof (T)));
    }

    public void TypeMatches(Predicate<Type> predicate)
    {
      this.Matching((Predicate<PropertyInfo>) (prop => predicate(prop.PropertyType)));
    }

    public void Matching(Predicate<PropertyInfo> rule)
    {
      PluginCache.UseSetterRule(rule);
    }

    public void WithAnyTypeFromNamespace(string nameSpace)
    {
      this.Matching((Predicate<PropertyInfo>) (prop => TypeExtensions.IsInNamespace(prop.PropertyType, nameSpace)));
    }

    public void WithAnyTypeFromNamespaceContainingType<T>()
    {
      this.WithAnyTypeFromNamespace(typeof (T).Namespace);
    }

    public void NameMatches(Predicate<string> rule)
    {
      this.Matching((Predicate<PropertyInfo>) (prop => rule(prop.Name)));
    }
  }
}
