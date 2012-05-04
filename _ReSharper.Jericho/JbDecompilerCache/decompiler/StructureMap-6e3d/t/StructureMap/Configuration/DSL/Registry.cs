// Type: StructureMap.Configuration.DSL.Registry
// Assembly: StructureMap, Version=2.6.3.0, Culture=neutral, PublicKeyToken=e60ad81abae3c223
// Assembly location: C:\Development\Jericho\packages\structuremap.2.6.3\lib\StructureMap.dll

using StructureMap;
using StructureMap.Configuration.DSL.Expressions;
using StructureMap.Graph;
using StructureMap.Interceptors;
using StructureMap.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace StructureMap.Configuration.DSL
{
  public class Registry : IRegistry
  {
    private readonly List<Action<PluginGraph>> _actions = new List<Action<PluginGraph>>();
    private readonly List<Action> _basicActions = new List<Action>();

    public void AddType(Type pluginType, Type concreteType)
    {
      this._actions.Add((Action<PluginGraph>) (g => g.AddType(pluginType, concreteType)));
    }

    public void AddType(Type pluginType, Type concreteType, string name)
    {
      this._actions.Add((Action<PluginGraph>) (g => g.AddType(pluginType, concreteType, name)));
    }

    public void AddType(Type pluggedType)
    {
      this._actions.Add((Action<PluginGraph>) (g => g.AddType(pluggedType)));
    }

    public void IncludeRegistry<T>() where T : new(), Registry
    {
      this._actions.Add((Action<PluginGraph>) (g => Activator.CreateInstance<T>().ConfigurePluginGraph(g)));
    }

    public void IncludeRegistry(Registry registry)
    {
      this._actions.Add(new Action<PluginGraph>(registry.ConfigurePluginGraph));
    }

    [Obsolete("Change to For<T>()")]
    public CreatePluginFamilyExpression<PLUGINTYPE> BuildInstancesOf<PLUGINTYPE>()
    {
      return new CreatePluginFamilyExpression<PLUGINTYPE>(this);
    }

    [Obsolete("Change to For(pluginType)")]
    public GenericFamilyExpression ForRequestedType(Type pluginType)
    {
      return new GenericFamilyExpression(pluginType, this);
    }

    public Registry.BuildWithExpression<T> ForConcreteType<T>()
    {
      return new Registry.BuildWithExpression<T>(this.For<T>().Use<T>());
    }

    [Obsolete("Change to For<T>()")]
    public CreatePluginFamilyExpression<PLUGINTYPE> ForRequestedType<PLUGINTYPE>()
    {
      return new CreatePluginFamilyExpression<PLUGINTYPE>(this);
    }

    public CreatePluginFamilyExpression<PLUGINTYPE> ForSingletonOf<PLUGINTYPE>()
    {
      return this.ForRequestedType<PLUGINTYPE>().Singleton();
    }

    public PluginGraph Build()
    {
      PluginGraph graph = new PluginGraph();
      this.ConfigurePluginGraph(graph);
      graph.Seal();
      return graph;
    }

    [Obsolete("Prefer For<T>().Add() instead")]
    public IsExpression<T> InstanceOf<T>()
    {
      return (IsExpression<T>) new InstanceExpression<T>((Action<Instance>) (instance => this._actions.Add((Action<PluginGraph>) (g => g.FindFamily(typeof (T)).AddInstance(instance)))));
    }

    [Obsolete("Prefer For(type).Add() instead")]
    public GenericIsExpression InstanceOf(Type pluginType)
    {
      return new GenericIsExpression((Action<Instance>) (instance => this._actions.Add((Action<PluginGraph>) (graph => graph.FindFamily(pluginType).AddInstance(instance)))));
    }

    [Obsolete("Change to Profile( [name], Action<ProfileExpression> )")]
    public ProfileExpression Profile(string profileName)
    {
      return new ProfileExpression(profileName, this);
    }

    public void Profile(string profileName, Action<ProfileExpression> action)
    {
      ProfileExpression profileExpression = new ProfileExpression(profileName, this);
      action(profileExpression);
    }

    public void RegisterInterceptor(TypeInterceptor interceptor)
    {
      this.addExpression((Action<PluginGraph>) (pluginGraph => pluginGraph.InterceptorLibrary.AddInterceptor(interceptor)));
    }

    public MatchedTypeInterceptor IfTypeMatches(Predicate<Type> match)
    {
      MatchedTypeInterceptor interceptor = new MatchedTypeInterceptor(match);
      this._actions.Add((Action<PluginGraph>) (graph => graph.InterceptorLibrary.AddInterceptor((TypeInterceptor) interceptor)));
      return interceptor;
    }

    public void Scan(Action<IAssemblyScanner> action)
    {
      AssemblyScanner scanner = new AssemblyScanner();
      action((IAssemblyScanner) scanner);
      this._actions.Add((Action<PluginGraph>) (graph => graph.AddScanner(scanner)));
    }

    public CreatePluginFamilyExpression<PLUGINTYPE> FillAllPropertiesOfType<PLUGINTYPE>()
    {
      PluginCache.AddFilledType(typeof (PLUGINTYPE));
      return this.ForRequestedType<PLUGINTYPE>();
    }

    public void SetAllProperties(Action<SetterConvention> action)
    {
      action(new SetterConvention());
    }

    public void SelectConstructor<T>(Expression<Func<T>> expression)
    {
      PluginCache.GetPlugin(typeof (T)).UseConstructor((Expression) expression);
    }

    public void Forward<FROM, TO>() where FROM : class where TO : class
    {
      this.For<TO>().AddInstances((Action<IInstanceExpression<TO>>) (x => x.ConstructedBy((Func<IContext, TO>) (c => (object) c.GetInstance<FROM>() as TO))));
    }

    [Obsolete("Prefer For<T>().Use(value)")]
    public void Register<PLUGINTYPE>(PLUGINTYPE @object)
    {
      this.ForRequestedType<PLUGINTYPE>().TheDefault.IsThis(@object);
    }

    [Obsolete("Prefer For<T>().Use(instance)")]
    public void Register<PLUGINTYPE>(Instance instance)
    {
      this.ForRequestedType<PLUGINTYPE>().TheDefault.IsThis(instance);
    }

    public CreatePluginFamilyExpression<PLUGINTYPE> For<PLUGINTYPE>()
    {
      return new CreatePluginFamilyExpression<PLUGINTYPE>(this);
    }

    public GenericFamilyExpression For(Type pluginType)
    {
      return this.ForRequestedType(pluginType);
    }

    public LambdaInstance<T> Redirect<T, U>() where T : class where U : class
    {
      return this.For<T>().TheDefault.Is.ConstructedBy((Func<IContext, T>) (c =>
      {
        U local_0 = c.GetInstance<U>();
        T local_1 = (object) local_0 as T;
        if ((object) local_1 == null)
          throw new ApplicationException(local_0.GetType().AssemblyQualifiedName + " could not be cast to " + typeof (T).AssemblyQualifiedName);
        else
          return local_1;
      }));
    }

    public void Configure(Action<PluginGraph> configure)
    {
      this._actions.Add(configure);
    }

    protected void registerAction(Action action)
    {
      this._basicActions.Add(action);
    }

    internal void addExpression(Action<PluginGraph> alteration)
    {
      this._actions.Add(alteration);
    }

    internal void ConfigurePluginGraph(PluginGraph graph)
    {
      if (graph.Registries.Contains(this))
        return;
      graph.Log.StartSource("Registry:  " + this.GetType().AssemblyQualifiedName);
      this._basicActions.ForEach((Action<Action>) (action => action()));
      this._actions.ForEach((Action<Action<PluginGraph>>) (action => action(graph)));
      graph.Registries.Add(this);
    }

    internal static bool IsPublicRegistry(Type type)
    {
      if (type.Assembly == typeof (Registry).Assembly || !typeof (Registry).IsAssignableFrom(type) || (type.IsInterface || type.IsAbstract) || type.IsGenericType)
        return false;
      else
        return type.GetConstructor(new Type[0]) != null;
    }

    public bool Equals(Registry other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      if (other.GetType() == typeof (Registry) && this.GetType() == typeof (Registry))
        return false;
      else
        return object.Equals((object) other.GetType(), (object) this.GetType());
    }

    public override bool Equals(object obj)
    {
      if (object.ReferenceEquals((object) null, obj))
        return false;
      if (object.ReferenceEquals((object) this, obj))
        return true;
      if (!typeof (Registry).IsAssignableFrom(obj.GetType()))
        return false;
      else
        return this.Equals((Registry) obj);
    }

    public override int GetHashCode()
    {
      return this.GetType().GetHashCode();
    }

    public class BuildWithExpression<T>
    {
      private readonly SmartInstance<T> _instance;

      public SmartInstance<T> Configure
      {
        get
        {
          return this._instance;
        }
      }

      public BuildWithExpression(SmartInstance<T> instance)
      {
        this._instance = instance;
      }
    }
  }
}
