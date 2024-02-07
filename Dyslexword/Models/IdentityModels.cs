using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Dyslexword.Common;
using System.Xml.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Dyslexword.Models
{
    // Para agregar datos de perfil del usuario, agregue más propiedades a su clase ApplicationUser. Visite https://go.microsoft.com/fwlink/?LinkID=317594 para obtener más información.
    public class ApplicationUser : IdentityUser
    {

        [Required(AllowEmptyStrings = false)]
        [StringLength(40, ErrorMessageResourceName = nameof(Resource.Longitud), ErrorMessageResourceType = typeof(Resource), MinimumLength = 1)]
        [Display(Name = ("Nombre"))]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(40, ErrorMessageResourceName = nameof(Resource.Longitud), ErrorMessageResourceType = typeof(Resource), MinimumLength = 1)]
        [Display(Name = ("Apellidos"))]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = ("Monedas"))]
        [DefaultValue(100)]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public decimal Coins { get; set; }

        // Fecha de nacimiento
        [Required]
        [Display(Name = ("Fecha de nacimiento"))]
        public DateTime Birthdate { get; set; }

        // Fecha de ultimo login
        [Required]
        [Display(Name = ("Ultimo Login"))]
        public DateTime? Login { get; set; }



        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que authenticationType debe coincidir con el valor definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar reclamaciones de usuario personalizadas aquí
            return userIdentity;
        }
    }
}