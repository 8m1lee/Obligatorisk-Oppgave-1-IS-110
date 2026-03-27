using System;

namespace UniversitetSystem.Models
{
    // Abstrakt baseklasse for alle brukere i systemet
    // 'abstract' betyr at vi ikke kan lage instanser av denne klassen direkte,
    // men at andre klasser må arve fra den
    public abstract class User
    {
        // Properties (egenskaper) som alle brukere har
        public string Navn { get; set; }
        public string Email { get; set; }
        
        // Constructor (konstruktør) - kalles når vi lager en ny bruker
        public User(string navn, string email)
        {
            Navn = navn;
            Email = email;
        }
        
        // Abstract metode - må implementeres av klasser som arver fra User
        // Den returnerer brukerens ID (StudentID eller AnsattID)
        public abstract string GetUserId();
        
        // Virtual metode - kan overrides (overstyres) av subklasser
        // Gir en tekstlig representasjon av brukeren
        public virtual string GetInfo()
        {
            return $"Navn: {Navn}, Email: {Email}";
        }
    }
}
