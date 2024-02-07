namespace Dyslexword.Migrations
{
    using Dyslexword.Common;
    using Dyslexword.Data;
    using Dyslexword.Models;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Web;
    using System.Web.Hosting;
    using System.Web.Mvc;
    using System.Web.ApplicationServices;

    internal sealed class Configuration : DbMigrationsConfiguration<Dyslexword.Data.ApplicationDbContext>
    {
        private ApplicationUserManager userManager;

        public Configuration()
        {
            CommandTimeout = 100000;
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Dyslexword.Data.ApplicationDbContext";
        }

        protected override void Seed(Dyslexword.Data.ApplicationDbContext context)
        {

            CheckInitialization(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }

        private void CheckInitialization(ApplicationDbContext db)
        {
            if (HttpContext.Current != null && db.Users.Count() == 0) // no tenemos inicializada la bbdd
                CreateDataDatabase(db);
        }

        /// <summary>
        /// Metodo de creación de la informacion inicial (primera migración)
        /// </summary>
        /// <param name="db"></param>
        private void CreateDataDatabase(ApplicationDbContext db)
        {
            userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            if (userManager == null)
                return;


            // 5. Usuario por defecto para cordoware
            if (db.Users.Count() == 0)
            {
                const string firstName = "Administrador";
                const string lastName = "Administrador";
                const string userMail = "administrador@gmail.com";
                const string userPassword = "Admin@123456";
                const decimal coins = 0;

                var user = new ApplicationUser { UserName = userMail, Email = userMail, LastName = lastName, FirstName = firstName, Coins = coins, Birthdate = DateTime.Now, Login = DateTime.Now };
                var result = userManager.Create(user, userPassword);

                if (result.Succeeded)
                {

                    // Deshabilitamos el locking del usuario
                    userManager.SetLockoutEnabled(user.Id, false);

                    // Asignamos todos los módulos
                    userManager.AddToRole(user.Id, "Administrador");
                }
            }
        }
    }
}
