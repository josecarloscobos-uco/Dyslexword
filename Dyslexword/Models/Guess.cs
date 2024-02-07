using Dyslexword.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Dyslexword.Models
{
    public class Guess
    {
        public Guess()
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