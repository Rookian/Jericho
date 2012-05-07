using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Castle.DynamicProxy;

namespace StructureMap.AutoFactory
{
    public class FactoryInterceptor : IInterceptor
    {
        private readonly IContext _context;

        public FactoryInterceptor(IContext context)
        {
            _context = context;
        }

        public void Intercept(IInvocation invocation)
        {
            Type pluginType;

            if ((invocation.Arguments.Length > 0) && (invocation.Arguments[0] is Type))
            {
                pluginType = (Type)invocation.Arguments[0];
            }
            else
            {
                pluginType = invocation.Method.ReturnType;
            }

            var returnType = invocation.Method.ReturnType;

            object returnValue;

            if (IsGenericIEnumerable(returnType))
            {
                if (IsGenericIEnumerable(pluginType))
                {
                    returnValue = InvokeGenericMethod<IContext>(x => x.GetAllInstances<object>(), pluginType.GetGenericArguments().First(), _context);
                }
                else
                {
                    returnValue = InvokeGenericMethod<IContext>(x => x.GetAllInstances<object>(), pluginType, _context);
                }
            }
            else
            {
                returnValue = _context.GetInstance(pluginType);
            }

            invocation.ReturnValue = returnValue;
        }

        static bool IsGenericIEnumerable(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>);
        }

        static object InvokeGenericMethod<T>(Expression<Action<T>> expr, Type elementType, object instance)
        {
            var method = ((MethodCallExpression)expr.Body).Method.GetGenericMethodDefinition();
            var genericMethod = method.MakeGenericMethod(elementType);
            return genericMethod.Invoke(instance, null);
        }
    }
}