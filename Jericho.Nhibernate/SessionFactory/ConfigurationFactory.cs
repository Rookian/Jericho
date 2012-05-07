using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Jericho.Nhibernate.Mapping;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Jericho.Nhibernate.SessionFactory
{
    public class ConfigurationFactory
    {
        private const string Database = "Ariha";
        private const string Server = @".\sqlExpress";
        //private const string Server = "localhost";
        public Configuration Build()
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.Database(Database).TrustedConnection().Server(Server)))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<TeamMap>())
                .ExposeConfiguration(c => new SchemaExport(c).Execute(true, true, false))
                .BuildConfiguration();
        }
    }
}