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
                db.LogFormattedSql = true;  // Optional: pretty SQL in logs
                db.LogSqlInConsole = true;  // Optional: show SQL in console
                db.BatchSize = 32;
            });

            // This scans the current assembly (LeapEventTech) for embedded .hbm.xml files
            cfg.AddAssembly(typeof(NHibernateConfig).Assembly);

            return cfg.BuildSessionFactory();
        }
    }
}
