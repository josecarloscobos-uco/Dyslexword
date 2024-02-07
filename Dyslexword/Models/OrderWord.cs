using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Resources;
using System.Web.Util;
using Dyslexword.Common;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Dyslexword.Models
{
        public class OrderWord
        {
            public OrderWord()
            {
                this.Levels = new HashSet<Level>();
            }

            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            [Required(AllowEmptyStrings = false)]
            [StringLength(60, ErrorMessageResourceName = nameof(Resource.Longitud), ErrorMessageResourceType = typeof(Resource), MinimumLength = 0)]
            [Display(Name = ("Palabra"))]
            public string Word { get; set; }

            [Required(AllowEmptyStrings = false)]
            [Display(Name = ("Imagen"))]
            public byte[] Image { get; set; }

            // Colección de Niveles
            [Display(Name = ("Niveles"))]
            public virtual ICollection<Level> Levels { get; set; }

        }
}