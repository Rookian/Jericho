// Type: System.Web.Mvc.DefaultControllerFactory
// Assembly: System.Web.Mvc, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Development\Jericho\packages\AspNetMvc.4.0.20126.16343\lib\net40\System.Web.Mvc.dll

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc.Resources;
using System.Web.Routing;
using System.Web.SessionState;

namespace System.Web.Mvc
{
  public class DefaultControllerFactory : IControllerFactory
  {
    private static readonly ConcurrentDictionary<Type, SessionStateBehavior> _sessionStateCache = new ConcurrentDictionary<Type, SessionStateBehavior>();
    private static ControllerTypeCache _staticControllerTypeCache = new ControllerTypeCache();
    private IBuildManager _buildManager;
    private IResolver<IControllerActivator> _activatorResolver;
    private IControllerActivator _controllerActivator;
    private ControllerBuilder _controllerBuilder;
    private ControllerTypeCache _instanceControllerTypeCache;

    IControllerActivator ControllerActivator
    {
      private get
      {
        if (this._controllerActivator != null)
          return this._controllerActivator;
        this._controllerActivator = this._activatorResolver.Current;
        return this._controllerActivator;
      }
    }

    internal IBuildManager BuildManager
    {
      get
      {
        if (this._buildManager == null)
          this._buildManager = (IBuildManager) new BuildManagerWrapper();
        return this._buildManager;
      }
      set
      {
        this._buildManager = value;
      }
    }

    internal ControllerBuilder ControllerBuilder
    {
      get
      {
        return this._controllerBuilder ?? ControllerBuilder.Current;
      }
      set
      {
        this._controllerBuilder = value;
      }
    }

    internal ControllerTypeCache ControllerTypeCache
    {
      get
      {
        return this._instanceControllerTypeCache ?? DefaultControllerFactory._staticControllerTypeCache;
      }
      set
      {
        this._instanceControllerTypeCache = value;
      }
    }

    static DefaultControllerFactory()
    {
    }

    public DefaultControllerFactory()
      : this((IControllerActivator) null, (IResolver<IControllerActivator>) null, (IDependencyResolver) null)
    {
    }

    public DefaultControllerFactory(IControllerActivator controllerActivator)
      : this(controllerActivator, (IResolver<IControllerActivator>) null, (IDependencyResolver) null)
    {
    }

    internal DefaultControllerFactory(IControllerActivator controllerActivator, IResolver<IControllerActivator> activatorResolver, IDependencyResolver dependencyResolver)
    {
      if (controllerActivator != null)
        this._controllerActivator = controllerActivator;
      else
        this._activatorResolver = activatorResolver ?? (IResolver<IControllerActivator>) new SingleServiceResolver<IControllerActivator>((Func<IControllerActivator>) (() => (IControllerActivator) null), (IControllerActivator) new DefaultControllerFactory.DefaultControllerActivator(dependencyResolver), "DefaultControllerFactory constructor");
    }

    internal static InvalidOperationException CreateAmbiguousControllerException(RouteBase route, string controllerName, ICollection<Type> matchingTypes)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (Type type in (IEnumerable<Type>) matchingTypes)
      {
        stringBuilder.AppendLine();
        stringBuilder.Append(type.FullName);
      }
      Route route1 = route as Route;
      string message;
      if (route1 != null)
        message = string.Format((IFormatProvider) CultureInfo.CurrentCulture, MvcResources.DefaultControllerFactory_ControllerNameAmbiguous_WithRouteUrl, (object) controllerName, (object) route1.Url, (object) stringBuilder);
      else
        message = string.Format((IFormatProvider) CultureInfo.CurrentCulture, MvcResources.DefaultControllerFactory_ControllerNameAmbiguous_WithoutRouteUrl, new object[2]
        {
          (object) controllerName,
          (object) stringBuilder
        });
      return new InvalidOperationException(message);
    }

    public virtual IController CreateController(RequestContext requestContext, string controllerName)
    {
      if (requestContext == null)
        throw new ArgumentNullException("requestContext");
      if (string.IsNullOrEmpty(controllerName))
        throw new ArgumentException(MvcResources.Common_NullOrEmpty, "controllerName");
      Type controllerType = this.GetControllerType(requestContext, controllerName);
      return this.GetControllerInstance(requestContext, controllerType);
    }

    protected internal virtual IController GetControllerInstance(RequestContext requestContext, Type controllerType)
    {
      if (controllerType == (Type) null)
      {
        throw new HttpException(404, string.Format((IFormatProvider) CultureInfo.CurrentCulture, MvcResources.DefaultControllerFactory_NoControllerFound, new object[1]
        {
          (object) requestContext.HttpContext.Request.Path
        }));
      }
      else
      {
        if (typeof (IController).IsAssignableFrom(controllerType))
          return this.ControllerActivator.Create(requestContext, controllerType);
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, MvcResources.DefaultControllerFactory_TypeDoesNotSubclassControllerBase, new object[1]
        {
          (object) controllerType
        }), "controllerType");
      }
    }

    protected internal virtual SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, Type controllerType)
    {
      if (controllerType == (Type) null)
        return SessionStateBehavior.Default;
      else
        return DefaultControllerFactory._sessionStateCache.GetOrAdd(controllerType, (Func<Type, SessionStateBehavior>) (type =>
        {
          Type temp_12 = type;
          bool local_1 = true;
          Type temp_15 = typeof (SessionStateAttribute);
          int temp_16 = local_1 ? 1 : 0;
          SessionStateAttribute local_0 = Enumerable.FirstOrDefault<SessionStateAttribute>(Enumerable.OfType<SessionStateAttribute>((IEnumerable) temp_12.GetCustomAttributes(temp_15, temp_16 != 0)));
          if (local_0 == null)
            return SessionStateBehavior.Default;
          else
            return local_0.Behavior;
        }));
    }

    protected internal virtual Type GetControllerType(RequestContext requestContext, string controllerName)
    {
      if (string.IsNullOrEmpty(controllerName))
        throw new ArgumentException(MvcResources.Common_NullOrEmpty, "controllerName");
      object obj;
      if (requestContext != null && requestContext.RouteData.DataTokens.TryGetValue("Namespaces", out obj))
      {
        IEnumerable<string> enumerable = obj as IEnumerable<string>;
        if (enumerable != null && Enumerable.Any<string>(enumerable))
        {
          HashSet<string> namespaces = new HashSet<string>(enumerable, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
          Type withinNamespaces = this.GetControllerTypeWithinNamespaces(requestContext.RouteData.Route, controllerName, namespaces);
          if (withinNamespaces != (Type) null || false.Equals(requestContext.RouteData.DataTokens["UseNamespaceFallback"]))
            return withinNamespaces;
        }
      }
      if (this.ControllerBuilder.DefaultNamespaces.Count > 0)
      {
        HashSet<string> namespaces = new HashSet<string>((IEnumerable<string>) this.ControllerBuilder.DefaultNamespaces, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        Type withinNamespaces = this.GetControllerTypeWithinNamespaces(requestContext.RouteData.Route, controllerName, namespaces);
        if (withinNamespaces != (Type) null)
          return withinNamespaces;
      }
      return this.GetControllerTypeWithinNamespaces(requestContext.RouteData.Route, controllerName, (HashSet<string>) null);
    }

    private Type GetControllerTypeWithinNamespaces(RouteBase route, string controllerName, HashSet<string> namespaces)
    {
      this.ControllerTypeCache.EnsureInitialized(this.BuildManager);
      ICollection<Type> controllerTypes = this.ControllerTypeCache.GetControllerTypes(controllerName, namespaces);
      switch (controllerTypes.Count)
      {
        case 0:
          return (Type) null;
        case 1:
          return Enumerable.First<Type>((IEnumerable<Type>) controllerTypes);
        default:
          throw DefaultControllerFactory.CreateAmbiguousControllerException(route, controllerName, controllerTypes);
      }
    }

    public virtual void ReleaseController(IController controller)
    {
      IDisposable disposable = controller as IDisposable;
      if (disposable == null)
        return;
      disposable.Dispose();
    }

    SessionStateBehavior IControllerFactory.GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
    {
      if (requestContext == null)
        throw new ArgumentNullException("requestContext");
      if (string.IsNullOrEmpty(controllerName))
        throw new ArgumentException(MvcResources.Common_NullOrEmpty, "controllerName");
      Type controllerType = this.GetControllerType(requestContext, controllerName);
      return this.GetControllerSessionBehavior(requestContext, controllerType);
    }

    private class DefaultControllerActivator : IControllerActivator
    {
      private Func<IDependencyResolver> _resolverThunk;

      public DefaultControllerActivator()
        : this((IDependencyResolver) null)
      {
      }

      public DefaultControllerActivator(IDependencyResolver resolver)
      {
        if (resolver == null)
          this._resolverThunk = (Func<IDependencyResolver>) (() => DependencyResolver.Current);
        else
          this._resolverThunk = (Func<IDependencyResolver>) (() => resolver);
      }

      public IController Create(RequestContext requestContext, Type controllerType)
      {
        try
        {
          return (IController) (this._resolverThunk().GetService(controllerType) ?? Activator.CreateInstance(controllerType));
        }
        catch (Exception ex)
        {
          throw new InvalidOperationException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, MvcResources.DefaultControllerFactory_ErrorCreatingController, new object[1]
          {
            (object) controllerType
          }), ex);
        }
      }
    }
  }
}
