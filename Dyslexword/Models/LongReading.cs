using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Dyslexword.Common;

namespace Dyslexword.Models
{
    public class LongReading
    {
        public LongReading()
        {
            this.Levels = new HashSet<Level>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(510, ErrorMessageResourceName = nameof(Resource.Longitud), ErrorMessageResourceType = typeof(Resource), MinimumLength = 0)]
        [Display(Name = ("Texto"))]
        public string Text { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(60, ErrorMessageResourceName = nameof(Resource.Longitud), ErrorMessageResourceType = typeof(Resource), MinimumLength = 0)]
        [Display(Name = ("Pregunta"))]
        public string Question { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(60, ErrorMessageResourceName = nameof(Resource.Longitud), ErrorMessageResourceType = typeof(Resource), MinimumLength = 0)]
        [Display(Name = ("Opción Correcta"))]
        public string CorrectOption { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(60, ErrorMessageResourceName = nameof(Resource.Longitud), ErrorMessageResourceType = typeof(Resource), MinimumLength = 0)]
        [Display(Name = ("Opción 1"))]
        public string OptionA { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(60, ErrorMessageResourceName = nameof(Resource.Longitud), ErrorMessageResourceType = typeof(Resource), MinimumLength = 0)]
        [Display(Name = ("Opción 2"))]
        public string OptionB { get; set; }


        // Colección de Niveles
        [Display(Name = ("Niveles"))]
        public virtual ICollection<Level> Levels { get; set; }
    }

}