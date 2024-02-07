using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Text.RegularExpressions;

namespace Dyslexword.Data
{
    public class CreateDatabaseCollationInterceptor : IDbCommandInterceptor
    {
        private readonly string _collation;

        public CreateDatabaseCollationInterceptor(string collation)
        {
            _collation = collation;
        }

        /// <summary>
        /// Intercepción cuando se ha ejecutado el comando SQL
        /// </summary>
        /// <param name="command"></param>
        /// <param name="interceptionContext"></param>
        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext) { }

        /// <summary>
        /// Intercepción cuando se va a ejcutar el comando SQL
        /// </summary>
        /// <param name="command"></param>
        /// <param name="interceptionContext"></param>
        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            // Works for SQL Server
            if (Regex.IsMatch(command.CommandText, @"^create database \[.*]$"))
                command.CommandText += " COLLATE " + _collation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="interceptionContext"></param>
        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="interceptionContext"></param>
        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="interceptionContext"></param>
        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="interceptionContext"></param>
        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext) { }
    }
}