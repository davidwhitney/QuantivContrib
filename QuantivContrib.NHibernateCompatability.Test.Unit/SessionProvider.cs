using System.Data;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;

namespace QuantivContrib.NHibernateCompatability.Test.Unit
{
    public class SessionProvider
    {
        public static SessionProvider Instance { get; private set; }
        public ISessionFactory SessionFactory { get; private set; }

        static SessionProvider()
        {
            Instance = new SessionProvider();
        }
        private SessionProvider()
        {
            SessionFactory = CreateSessionFactory();
        }

        public ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        public ISession OpenSession(IDbConnection connection)
        {
            return SessionFactory.OpenSession(connection);
        }

        private class FluentMappingAssembly
        {
        }

        private static ISessionFactory CreateSessionFactory()
        {
            var sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration
                              .MsSql2005.ShowSql()
                              .ConnectionString(c => c.FromConnectionStringWithKey("BB01")))
                .Mappings(m =>
                          m.FluentMappings.AddFromAssemblyOf<FluentMappingAssembly>()
                              .Conventions.Add(Table.Is(x => "BB01_" + x.EntityType.Name))
                              .Conventions.Add(DefaultLazy.Always())
                              .Conventions.Add(DefaultAccess.Property())
                              .Conventions.Add(DefaultCascade.All())
                              .Conventions.Add(PrimaryKey.Name.Is(x => x.EntityType.Name + "ID")))
                .BuildSessionFactory();

            return sessionFactory;
        }
    }
}