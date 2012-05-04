// Type: StructureMap.IContainer
// Assembly: StructureMap, Version=2.6.3.0, Culture=neutral, PublicKeyToken=e60ad81abae3c223
// Assembly location: C:\Development\Jericho\packages\structuremap.2.6.3\lib\StructureMap.dll

using StructureMap.Pipeline;
using StructureMap.Query;
using System;
using System.Collections;
using System.Collections.Generic;

namespace StructureMap
{
  public interface IContainer : IDisposable
  {
    IModel Model { get; }

    object GetInstance(Type pluginType, string instanceKey);

    object GetInstance(Type pluginType);

    object GetInstance(Type pluginType, Instance instance);

    T GetInstance<T>(string instanceKey);

    T GetInstance<T>();

    T GetInstance<T>(Instance instance);

    IList<T> GetAllInstances<T>();

    IList GetAllInstances(Type pluginType);

    object TryGetInstance(Type pluginType, string instanceKey);

    object TryGetInstance(Type pluginType);

    T TryGetInstance<T>();

    T TryGetInstance<T>(string instanceKey);

    [Obsolete("Please use GetInstance<T>() instead.")]
    T FillDependencies<T>();

    [Obsolete("Please use GetInstance(Type) instead")]
    object FillDependencies(Type type);

    void Configure(Action<ConfigurationExpression> configure);

    void Inject<PLUGINTYPE>(PLUGINTYPE instance);

    void Inject<PLUGINTYPE>(string name, PLUGINTYPE value);

    void Inject(Type pluginType, object @object);

    void SetDefaultsToProfile(string profile);

    string WhatDoIHave();

    void AssertConfigurationIsValid();

    IList<T> GetAllInstances<T>(ExplicitArguments args);

    IList GetAllInstances(Type type, ExplicitArguments args);

    T GetInstance<T>(ExplicitArguments args);

    ExplicitArgsExpression With<T>(T arg);

    IExplicitProperty With(string argName);

    object GetInstance(Type pluginType, ExplicitArguments args);

    void EjectAllInstancesOf<T>();

    void BuildUp(object target);

    void SetDefault(Type pluginType, Instance instance);

    Container.OpenGenericTypeExpression ForGenericType(Type templateType);

    T GetInstance<T>(ExplicitArguments args, string name);

    ExplicitArgsExpression With(Type pluginType, object arg);

    CloseGenericTypeExpression ForObject(object subject);

    IContainer GetNestedContainer();

    IContainer GetNestedContainer(string profileName);
  }
}
