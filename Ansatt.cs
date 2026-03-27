using System;

namespace UniversitetSystem.Models
{
    // Ansatt klassen arver fra User
    // Representerer ansatte ved universitetet
    public class Ansatt : User
    {
        public string AnsattID { get; set; }
        public string Stilling { get; set; }  // f.eks. "Foreleser", "Bibliotekar"
        public string Avdeling { get; set; }
        
        // Constructor
        public Ansatt(string ansattId, string navn, string email, 
                      string stilling, string avdeling)
            : base(navn, email)
        {
            AnsattID = ansattId;
            Stilling = stilling;
            Avdeling = avdeling;
        }
        
        // Implementerer GetUserId fra User
        public override string GetUserId()
        {
            return AnsattID;
        }
        
        // Override GetInfo for å vise ansattinformasjon
        public override string GetInfo()
        {
            return $"AnsattID: {AnsattID}, {base.GetInfo()}, " +
                   $"Stilling: {Stilling}, Avdeling: {Avdeling}";
        }
    }
}
