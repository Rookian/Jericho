// Type: NHibernate.Cfg.Configuration
// Assembly: NHibernate, Version=3.1.0.4000, Culture=neutral, PublicKeyToken=aa95f207798dfdb4
// Assembly location: C:\Development\Jericho\Jericho.MVC\bin\NHibernate.dll

using Iesi.Collections.Generic;
using NHibernate;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Dialect.Function;
using NHibernate.Engine;
using NHibernate.Event;
using NHibernate.Mapping;
using NHibernate.Proxy;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;

namespace NHibernate.Cfg
{
    [Serializable]
    public class Configuration : ISerializable
    {
        public const string DefaultHibernateCfgFileName = "hibernate.cfg.xml";
        protected IDictionary<string, PersistentClass> classes;
        protected IDictionary<string, Collection> collections;
        protected IDictionary<string, Table> tables;
        protected IList<SecondPassCommand> secondPasses;
        protected Queue<FilterSecondPassArgs> filtersSecondPasses;
        protected IList<Mappings.PropertyReference> propertyReferences;
        protected IList<IAuxiliaryDatabaseObject> auxiliaryDatabaseObjects;
        protected IDictionary<string, TypeDef> typeDefs;
        protected ISet<ExtendsQueueEntry> extendsQueue;
        protected IDictionary<string, Mappings.TableDescription> tableNameBinding;
        protected IDictionary<Table, Mappings.ColumnNames> columnNameBindingPerTable;
        protected internal SettingsFactory settingsFactory;
        public Configuration(SerializationInfo info, StreamingContext context);
        protected Configuration(SettingsFactory settingsFactory);
        public Configuration();
        public void GetObjectData(SerializationInfo info, StreamingContext context);
        protected void Reset();
        public virtual IMapping BuildMapping();
        public PersistentClass GetClassMapping(Type persistentClass);
        public PersistentClass GetClassMapping(string entityName);
        public Collection GetCollectionMapping(string role);
        public Configuration AddFile(string xmlFile);
        public Configuration AddFile(FileInfo xmlFile);
        public Configuration AddXmlFile(string xmlFile);
        public Configuration AddXml(string xml);
        public Configuration AddXml(string xml, string name);
        public Configuration AddXmlString(string xml);
        public Configuration AddUrl(string url);
        public Configuration AddUrl(Uri url);
        public Configuration AddDocument(XmlDocument doc);
        public Configuration AddDocument(XmlDocument doc, string name);
        public void AddDeserializedMapping(HbmMapping mappingDocument, string documentFileName);
        public Mappings CreateMappings(Dialect dialect);
        public Configuration AddInputStream(Stream xmlInputStream);
        public Configuration AddInputStream(Stream xmlInputStream, string name);
        public Configuration AddResource(string path, Assembly assembly);
        public Configuration AddResources(IEnumerable<string> paths, Assembly assembly);
        public Configuration AddClass(Type persistentClass);
        public Configuration AddAssembly(string assemblyName);
        public Configuration AddAssembly(Assembly assembly);
        public Configuration AddDirectory(DirectoryInfo dir);
        public string[] GenerateDropSchemaScript(Dialect dialect);
        public static bool IncludeAction(SchemaAction actionsSource, SchemaAction includedAction);
        public string[] GenerateSchemaCreationScript(Dialect dialect);
        public virtual void BuildMappings();
        protected virtual void ConfigureProxyFactoryFactory();
        public ISessionFactory BuildSessionFactory();
        public Configuration SetDefaultAssembly(string newDefaultAssembly);
        public Configuration SetDefaultNamespace(string newDefaultNamespace);
        public Configuration SetInterceptor(IInterceptor newInterceptor);
        public Configuration SetProperties(IDictionary<string, string> newProperties);
        public Configuration AddProperties(IDictionary<string, string> additionalProperties);
        public Configuration SetProperty(string name, string value);
        public string GetProperty(string name);
        public Configuration Configure();
        public Configuration Configure(string fileName);
        public Configuration Configure(Assembly assembly, string resourceName);
        public Configuration Configure(XmlReader textReader);
        protected Configuration DoConfigure(ISessionFactoryConfiguration factoryConfiguration);
        public Configuration SetCacheConcurrencyStrategy(string clazz, string concurrencyStrategy);
        public void SetCacheConcurrencyStrategy(string clazz, string concurrencyStrategy, string region);
        public Configuration SetCollectionCacheConcurrencyStrategy(string collectionRole, string concurrencyStrategy);
        public Configuration SetNamingStrategy(INamingStrategy newNamingStrategy);
        public void AddFilterDefinition(FilterDefinition definition);
        public void AddAuxiliaryDatabaseObject(IAuxiliaryDatabaseObject obj);
        public void AddSqlFunction(string functionName, ISQLFunction sqlFunction);
        public NamedXmlDocument LoadMappingDocument(XmlReader hbmReader, string name);
        public Configuration AddXmlReader(XmlReader hbmReader);
        public Configuration AddXmlReader(XmlReader hbmReader, string name);
        protected virtual string GetDefaultConfigurationFilePath();
        public void SetListeners(ListenerType type, string[] listenerClasses);
        public void SetListener(ListenerType type, object listener);
        public void SetListeners(ListenerType type, object[] listeners);
        public void AppendListeners(ListenerType type, object[] listeners);
        public string[] GenerateSchemaUpdateScript(Dialect dialect, DatabaseMetadata databaseMetadata);
        public void ValidateSchema(Dialect dialect, DatabaseMetadata databaseMetadata);
        public ICollection<PersistentClass> ClassMappings { get; }
        public ICollection<Collection> CollectionMappings { get; }
        public IDictionary<string, NamedQueryDefinition> NamedQueries { get; protected set; }
        public IEntityNotFoundDelegate EntityNotFoundDelegate { get; set; }
        public EventListeners EventListeners { get; }
        public IInterceptor Interceptor { get; set; }
        public IDictionary<string, string> Properties { get; set; }
        public IDictionary<string, string> Imports { get; protected set; }
        public IDictionary<string, NamedSQLQueryDefinition> NamedSQLQueries { get; protected set; }
        public INamingStrategy NamingStrategy { get; }
        public IDictionary<string, ResultSetMappingDefinition> SqlResultSetMappings { get; protected set; }
        public IDictionary<string, FilterDefinition> FilterDefinitions { get; protected set; }
        public IDictionary<string, ISQLFunction> SqlFunctions { get; protected set; }
        public event EventHandler<BindMappingEventArgs> BeforeBindMapping;
        public event EventHandler<BindMappingEventArgs> AfterBindMapping;
    }
}
