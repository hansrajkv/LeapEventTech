using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;

namespace LeapEventTech.Infrastructure.NHibernate
{
    public static class NHibernateConfig
    {
        public static ISessionFactory BuildSessionFactory(string connectionString)
        {
            var cfg = new Configuration();

            cfg.DataBaseIntegration(db =>
            {
                db.ConnectionString = connectionString;
                db.Dialect<SQLiteDialect>();
                db.Driver<SQLite20Driver>();
                db.ConnectionReleaseMode = ConnectionReleaseMode.OnClose;
                db.BatchSize = 32;
            });

            cfg.AddAssembly(typeof(NHibernateConfig).Assembly);

            return cfg.BuildSessionFactory();
        }
    }
}
