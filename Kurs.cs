using System;
using System.Collections.Generic;
using System.Linq;

namespace UniversitetSystem.Models
{
    // Kurs klassen representerer et universitetskurs
    public class Kurs
    {
        public string KursKode { get; set; }      // f.eks. "DAT100"
        public string KursNavn { get; set; }      // f.eks. "Programmering"
        public int Studiepoeng { get; set; }      // f.eks. 10
        public int MaksAntallPlasser { get; set; }
        
        // Liste over studenter påmeldt kurset
        private List<Student> påmeldteStudenter;
        
        // Property som returnerer listen (read-only fra utsiden)
        public List<Student> PåmeldteStudenter 
        { 
            get { return påmeldteStudenter; } 
        }
        
        // Constructor
        public Kurs(string kursKode, string kursNavn, int studiepoeng, int maksAntallPlasser)
        {
            KursKode = kursKode;
            KursNavn = kursNavn;
            Studiepoeng = studiepoeng;
            MaksAntallPlasser = maksAntallPlasser;
            påmeldteStudenter = new List<Student>();
        }
        
        // Metode for å sjekke om det er ledig plass
        public bool HarLedigPlass()
        {
            return påmeldteStudenter.Count < MaksAntallPlasser;
        }
        
        // Metode for å melde en student på kurset
        // Returnerer true hvis vellykket, false hvis kurset er fullt
        public bool MeldPåStudent(Student student)
        {
            // Sjekk om kurset er fullt
            if (!HarLedigPlass())
            {
                return false;
            }
            
            // Sjekk om studenten allerede er påmeldt
            if (påmeldteStudenter.Contains(student))
            {
                return false;
            }
            
            // Meld på studenten både i kurset og studentens liste
            påmeldteStudenter.Add(student);
            student.MeldPåKurs(this);
            return true;
        }
        
        // Metode for å melde en student av kurset
        public bool MeldAvStudent(Student student)
        {
            if (påmeldteStudenter.Remove(student))
            {
                student.MeldAvKurs(this);
                return true;
            }
            return false;
        }
        
        // Metode for å hente antall påmeldte studenter
        public int AntallPåmeldte()
        {
            return påmeldteStudenter.Count;
        }
        
        // Metode for å få en tekstrepresentasjon av kurset
        public string GetInfo()
        {
            return $"[{KursKode}] {KursNavn} - {Studiepoeng} studiepoeng, " +
                   $"Påmeldte: {AntallPåmeldte()}/{MaksAntallPlasser}";
        }
    }
}
