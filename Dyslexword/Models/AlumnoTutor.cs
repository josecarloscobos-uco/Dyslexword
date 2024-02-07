using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.UI.WebControls;
using System.Web.Util;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Dyslexword.Models
{ 
    public class AlumnoTutor
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Jugador
        [Display(Name = ("Alumno"))]
        public string UserAlumnoId { get; set; }

        // Jugador
        [Display(Name = ("Tutor"))]
        public string UserTutorId { get; set; }

    }
}