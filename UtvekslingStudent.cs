using System;

namespace UniversitetSystem.Models
{
    // UtvekslingStudent arver fra Student
    // Dette er et eksempel på flere nivåer av arv (multi-level inheritance)
    // UtvekslingStudent -> Student -> User
    public class UtvekslingStudent : Student
    {
        public string Hjemuniversitet { get; set; }
        public string Land { get; set; }
        public DateTime PeriodeFra { get; set; }
        public DateTime PeriodeTil { get; set; }
        
        // Constructor som kaller Student sin constructor med : base()
        public UtvekslingStudent(string studentId, string navn, string email,
                                  string hjemuniversitet, string land, 
                                  DateTime periodeFra, DateTime periodeTil)
            : base(studentId, navn, email)
        {
            Hjemuniversitet = hjemuniversitet;
            Land = land;
            PeriodeFra = periodeFra;
            PeriodeTil = periodeTil;
        }
        
        // Override GetInfo for å inkludere utvekslingsinformasjon
        public override string GetInfo()
        {
            return $"{base.GetInfo()}, Hjemuniversitet: {Hjemuniversitet}, Land: {Land}, " +
                   $"Periode: {PeriodeFra.ToShortDateString()} - {PeriodeTil.ToShortDateString()}";
        }
    }
}
