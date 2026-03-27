using System;
using System.Collections.Generic;

namespace UniversitetSystem.Models
{
    // Student klassen arver fra User (bruker : for arv i C#)
    // Den får alle properties og metoder fra User, pluss sine egne
    public class Student : User
    {
        public string StudentID { get; set; }
        
        // Liste over kurs studenten er påmeldt
        // List<T> er en dynamisk liste som kan vokse/krympe
        public List<Kurs> PåmeldteKurs { get; set; }
        
        // Constructor som tar inn verdier og sender noen videre til baseklassen (User)
        // : base(navn, email) kaller User sin constructor
        public Student(string studentId, string navn, string email) 
            : base(navn, email)
        {
            StudentID = studentId;
            PåmeldteKurs = new List<Kurs>(); // Initialiserer tom liste
        }
        
        // Override (overstyre) den abstrakte metoden fra User
        // override nøkkelordet forteller at vi implementerer metoden fra baseklassen
        public override string GetUserId()
        {
            return StudentID;
        }
        
        // Override GetInfo for å legge til StudentID i informasjonen
        public override string GetInfo()
        {
            return $"StudentID: {StudentID}, {base.GetInfo()}, Antall kurs: {PåmeldteKurs.Count}";
        }
        
        // Metode for å melde studenten på et kurs
        public void MeldPåKurs(Kurs kurs)
        {
            if (!PåmeldteKurs.Contains(kurs))
            {
                PåmeldteKurs.Add(kurs);
            }
        }
        
        // Metode for å melde studenten av et kurs
        public void MeldAvKurs(Kurs kurs)
        {
            PåmeldteKurs.Remove(kurs);
        }
    }
}
