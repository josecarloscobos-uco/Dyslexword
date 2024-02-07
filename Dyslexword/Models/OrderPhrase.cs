using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Web.Util;
using Dyslexword.Common;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Dyslexword.Models
{
        public class OrderPhrase
        {
            public OrderPhrase()
            {
                this.Levels = new HashSet<Level>();
            }

            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            [Required(AllowEmptyStrings = false)]
            [StringLength(250, ErrorMessageResourceName = nameof(Resource.Longitud), ErrorMessageResourceType = typeof(Resource), MinimumLength = 0)]
            [Display(Name = ("Frase"))]
            public string Phrase { get; set; }


            // Colección de Niveles
            [Display(Name = ("Niveles"))]
            public virtual ICollection<Level> Levels { get; set; }

        }
}