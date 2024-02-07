using Dyslexword.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Dyslexword.Data
{
    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IDbModelCacheKeyProvider
    {

        /// <summary>
        ///     Propiedad que controla si se ha desechado el contexto.
        /// </summary>
        public bool IsDisposed { get; protected set; }

        /// <summary>
        ///     Propiedad con que hace uso de la clase que intercepta los comandos SQL
        /// </summary>
        private EFCommandInterceptor commandInterceptor;

        /// <summary>
        ///     Propiedad que nos indica si se deshabilitará la columna de Identidad de las tablas
        /// </summary>
        private readonly bool disableIdentity = false;

        /// <summary>
        ///     Implementación de la interfaz, que devuelve la CacheKey y hace que se vuelva a ejecutar OnModelCreating al cambiar su valor.
        /// </summary>
        string IDbModelCacheKeyProvider.CacheKey
        {
            get { return disableIdentity.ToString(); }
        }

        /// <summary>
        /// Constructor del contexto de la bbdd
        /// </summary>
        public ApplicationDbContext() : base("name=" + ConfigurationManager.AppSettings["DbConnectionName"])
        {
            Database.CommandTimeout = 600;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Migrations.Configuration>());
            DbInterception.Add(new CreateDatabaseCollationInterceptor("Latin1_General_CI_AI"));
        }

        /// <summary>
        /// Constructor del contexo de la bbdd, indicando si se quieren deshabilitar los campos con identity.
        /// </summary>
        /// <param name="DisableIdentity">Indica si se desahilitan los campos con identity.</param>
        public ApplicationDbContext(bool DisableIdentity) : base("name=" + ConfigurationManager.AppSettings["DbConnectionName"])
        {
            Database.CommandTimeout = 600;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Migrations.Configuration>());
            disableIdentity = DisableIdentity;
            if (disableIdentity)
            {
                commandInterceptor = new EFCommandInterceptor();
                DbInterception.Add(commandInterceptor);
            }
        }

        public DbSet<AlumnoTutor> AlumnoTutors { get; set; }
        public DbSet<Guess> Guesses { get; set; }
        public DbSet<OrderWord> OrderWords { get; set; }
        public DbSet<OrderPhrase> OrderPhrases { get; set; }
        public DbSet<LongReading> LongReadings { get; set; }
        public DbSet<Level> Levels { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        /// <summary>
		/// Método que guarda los cambios y puede injectar la eliminación de la columna de identidad
		/// </summary>
		/// <param name="tablaBBDD"></param>
		/// <returns></returns>
		public void SaveChangesWithoutIdentity(string tablaBBDD)
        {
            if (disableIdentity && !string.IsNullOrEmpty(tablaBBDD))
                commandInterceptor.SqlInject = string.Format("SET IDENTITY_INSERT {0} ON", tablaBBDD);
            SaveChanges();
            commandInterceptor.SqlInject = null;
        }

        /// <summary>
        /// Sobreescritura de la creación del modelo.
        /// </summary>
        /// <param name="modelBuilder">DbModelBuilder</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Se elimina el borrado en cascada.
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            if (disableIdentity)
            {
                modelBuilder.Entity<AlumnoTutor>().Property(i => i.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
                modelBuilder.Entity<Guess>().Property(i => i.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
                modelBuilder.Entity<OrderWord>().Property(i => i.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
                modelBuilder.Entity<OrderPhrase>().Property(i => i.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
                modelBuilder.Entity<Guess>().Property(i => i.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
                modelBuilder.Entity<LongReading>().Property(i => i.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
                modelBuilder.Entity<Level>().Property(i => i.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            }

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        ///     Sobreescritura del desecho del contexto.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            IsDisposed = true;
            base.Dispose(disposing);
        }
    }

}
