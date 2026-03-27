using System;
using System.Collections.Generic;
using System.Linq;
using UniversitetSystem.Models;

namespace UniversitetSystem.Services
{
    // Hovedklasse som koordinerer alle services og lagrer brukere
    public class UniversitySystem
    {
        // Services for de forskjellige delene av systemet
        public KursService KursService { get; private set; }
        public BibliotekService BibliotekService { get; private set; }
        
        // Lister over brukere
        private List<Student> studenter;
        private List<Ansatt> ansatte;
        
        public UniversitySystem()
        {
            KursService = new KursService();
            BibliotekService = new BibliotekService();
            studenter = new List<Student>();
            ansatte = new List<Ansatt>();
        }
        
        // Legger til en student i systemet
        public void LeggTilStudent(Student student)
        {
            studenter.Add(student);
        }
        
        // Legger til en ansatt i systemet
        public void LeggTilAnsatt(Ansatt ansatt)
        {
            ansatte.Add(ansatt);
        }
        
        // Finner en student basert på StudentID
        public Student FinnStudent(string studentId)
        {
            return studenter.FirstOrDefault(s => 
                s.StudentID.Equals(studentId, StringComparison.OrdinalIgnoreCase)
            );
        }
        
        // Finner en ansatt basert på AnsattID
        public Ansatt FinnAnsatt(string ansattId)
        {
            return ansatte.FirstOrDefault(a => 
                a.AnsattID.Equals(ansattId, StringComparison.OrdinalIgnoreCase)
            );
        }
        
        // Finner en bruker (enten student eller ansatt) basert på ID
        public User FinnBruker(string brukerId)
        {
            // Prøv først som student
            User bruker = FinnStudent(brukerId);
            
            // Hvis ikke funnet, prøv som ansatt
            if (bruker == null)
            {
                bruker = FinnAnsatt(brukerId);
            }
            
            return bruker;
        }
        
        // Viser alle studenter
        public void VisAlleStudenter()
        {
            if (studenter.Count == 0)
            {
                Console.WriteLine("Ingen studenter registrert.");
                return;
            }
            
            Console.WriteLine("\n=== ALLE STUDENTER ===");
            foreach (var student in studenter)
            {
                Console.WriteLine(student.GetInfo());
            }
            Console.WriteLine();
        }
        
        // Viser alle ansatte
        public void VisAlleAnsatte()
        {
            if (ansatte.Count == 0)
            {
                Console.WriteLine("Ingen ansatte registrert.");
                return;
            }
            
            Console.WriteLine("\n=== ALLE ANSATTE ===");
            foreach (var ansatt in ansatte)
            {
                Console.WriteLine(ansatt.GetInfo());
            }
            Console.WriteLine();
        }
        
        // Returnerer alle studenter (for testing/seeding)
        public List<Student> HentAlleStudenter()
        {
            return studenter;
        }
    }
}
