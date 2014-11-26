using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContosoUniversity.Logging;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.SqlClient;
using System.Reflection;

namespace ContosoUniversity.DAL
{
    public class SchoolInterceptorTransientErrors : DbCommandInterceptor
    {
        private int _counter = 0;
        private ILogger _logger = new Logger();
        public override void ReaderExecuting(System.Data.Common.DbCommand command, DbCommandInterceptionContext<System.Data.Common.DbDataReader> interceptionContext)
        {
            bool throwTransientErrors = false;
            if (command.Parameters.Count > 0 && command.Parameters[0].Value.ToString() == "Throw")
            {
                throwTransientErrors = true;
                command.Parameters[0].Value = "an";
                command.Parameters[1].Value = "an";
            }

            if (throwTransientErrors && _counter < 4)
            {
                _logger.Info("Returning transient error for command: {0}", command.CommandText);
                _counter++;
                interceptionContext.Exception = CreateDummySqlException();
            }

            
        }

        private SqlException CreateDummySqlException()
        {
            var sqlErrorNumber = 20;
            var sqlErrorCtor = typeof(SqlError).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).Where(c => c.GetParameters().Count() == 7).Single();
            var sqlError = sqlErrorCtor.Invoke(new object[] { sqlErrorNumber, (byte)0, (byte)0, "", "", "", 1 });
            var errorCollection = Activator.CreateInstance(typeof(SqlErrorCollection), true);
            var addMethod = typeof(SqlErrorCollection).GetMethod("Add", BindingFlags.Instance | BindingFlags.NonPublic);
            addMethod.Invoke(errorCollection, new[] { sqlError });
            var sqlExceptionCtor = typeof(SqlException).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).Where(c => c.GetParameters().Count() == 4).Single();
            var sqlExeption = (SqlException)sqlExceptionCtor.Invoke(new object[] { "Dummy", errorCollection, null, Guid.NewGuid() });

            return sqlExeption;
        }

    }
}