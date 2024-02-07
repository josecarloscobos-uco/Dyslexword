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
    public class Level
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Jugador
        [Display(Name = ("Jugador"))]
        public string UserId { get; set; }

        // OrderWord
        [ForeignKey("OrderWord")]
        [Required]
        [Display(Name = ("Ultimo nivel de Order Word"))]
        public int OrderWordId { get; set; } = 1;
        [Display(Name = ("Ultimo nivel de Order Word"))]
        public virtual OrderWord OrderWord { get; set; }

        // OrderPhrase
        [ForeignKey("OrderPhrase")]
        [Required]
        [Display(Name = ("Ultimo nivel de Order Phrase"))]
        public int OrderPhraseId { get; set; }= 1;
        [Display(Name = ("Ultimo nivel de Order Phrase"))]
        public virtual OrderPhrase OrderPhrase { get; set; }

        // Adivina
        [ForeignKey("Guess")]
        [Required]
        [Display(Name = ("Ultimo nivel de Guess"))]
        public int GuessId { get; set; } = 1;
        [Display(Name = ("Ultimo nivel de Guess"))]
        public virtual Guess Guess { get; set; }

        // Lectura Comprensiva
        [ForeignKey("LongReading")]
        [Required]
        [Display(Name = ("Ultimo nivel de Long Reading"))]
        public int LongReadingId { get; set; } = 1;
        [Display(Name = ("Ultimo nivel de Long Reading"))]
        public virtual LongReading LongReading { get; set; }

    }
}