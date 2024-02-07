using Dyslexword.Data;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Dyslexword.Startup))]
namespace Dyslexword
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            if (!roleManager.RoleExists("Administrador"))
            {
                roleManager.Create(new IdentityRole("Administrador"));
            }

            if (!roleManager.RoleExists("Alumno"))
            {
                roleManager.Create(new IdentityRole("Alumno"));
            }

            if (!roleManager.RoleExists("Tutor"))
            {
                roleManager.Create(new IdentityRole("Tutor"));
            }

        }
    }
}
