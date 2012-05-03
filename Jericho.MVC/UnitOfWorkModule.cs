using System;
using System.Web;
using Jericho.Core;

namespace Jericho.MVC
{
    public class UnitOfWorkModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += ContextBeginRequest;
            context.EndRequest += ContextEndRequest;
        }

        public void Dispose() { }

        private static void ContextBeginRequest(object sender, EventArgs e)
        {
            var unitOfWork = UnitOfWorkFactory.GetDefault();
            unitOfWork.Begin();
        }

        private static void ContextEndRequest(object sender, EventArgs e)
        {
            var unitOfWork = UnitOfWorkFactory.GetDefault();
            
            try
            {
                unitOfWork.Commit();
            }
            catch
            {
                unitOfWork.RollBack();
                throw;
            }
            finally
            {
                unitOfWork.Dispose();
            }
        }
    }
}