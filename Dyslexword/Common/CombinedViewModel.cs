using Dyslexword.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dyslexword.Common
{
    public class CombinedViewModelWord
    {
            public List<Level> level { get; set; }
            public List<OrderWord> orderWord { get; set; }

    }

    public class CombinedViewModelPhrase
    {
        public List<Level> level { get; set; }
        public List<OrderPhrase> orderPhrase { get; set; }
    }

    public class CombinedViewModelLongReading
    {
        public List<Level> level { get; set; }
        public List<LongReading> longReading { get; set; }
    }

    public class CombinedViewModelGuess
    {
        public List<Level> level { get; set; }
        public List<Guess> guess { get; set; }
    }

    public class CombinedViewModelUser
    {
        public List<Level> level { get; set; }
        public List<AlumnoTutor> alumnotutors { get; set; }
        public List<ApplicationUser> applicationUsers { get; set; }
    }
}