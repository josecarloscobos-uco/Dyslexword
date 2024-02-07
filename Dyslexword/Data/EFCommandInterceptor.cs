using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Web;

namespace Dyslexword.Data
{
    public class EFCommandInterceptor : IDbCommandInterceptor
    {
        /// <summary>
        /// Propiedad con la SQL a injectar en el comando
        /// </summary>
        public string SqlInject { get; set; }

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
            if (!string.IsNullOrEmpty(SqlInject))
                command.CommandText = SqlInject + " " + command.CommandText;
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