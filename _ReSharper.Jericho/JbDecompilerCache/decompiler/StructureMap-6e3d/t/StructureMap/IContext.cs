// Type: StructureMap.IContext
// Assembly: StructureMap, Version=0.0.0.0, Culture=neutral, PublicKeyToken=e60ad81abae3c223
// Assembly location: C:\Development\Jericho\packages\structuremap.2.6.3\lib\StructureMap.dll

using StructureMap.Pipeline;
using System;
using System.Collections.Generic;

namespace StructureMap
{
  public interface IContext
  {
    BuildStack BuildStack { get; }

    Type ParentType { get; }

    BuildFrame Root { get; }

    string RequestedName { get; }

    void BuildUp(object target);

    T GetInstance<T>();

    object GetInstance(Type pluginType);

    T GetInstance<T>(string name);

    void RegisterDefault(Type pluginType, object defaultObject);

    T TryGetInstance<T>() where T : class;

    T TryGetInstance<T>(string name) where T : class;

    IEnumerable<T> All<T>() where T : class;

    IEnumerable<T> GetAllInstances<T>();
  }
}
