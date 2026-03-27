using System;
using System.Collections.Generic;
using System.Linq;
using UniversitetSystem.Models;

namespace UniversitetSystem.Services
{
    // Service klasse for å håndtere alle kursrelaterte operasjoner
    // Dette følger "separation of concerns" prinsippet
    public class KursService
    {
        // Liste over alle kurs i systemet
        private List<Kurs> kurs;
        
        public KursService()
        {
            kurs = new List<Kurs>();
        }
        
        // Oppretter et nytt kurs
        public Kurs OpprettKurs(string kursKode, string kursNavn, int studiepoeng, int maksAntallPlasser)
        {
            // Sjekk om kurskoden allerede eksisterer
            if (kurs.Any(k => k.KursKode == kursKode))
            {
                Console.WriteLine($"Feil: Kurs med kode {kursKode} eksisterer allerede.");
                return null;
            }
            
            var nyttKurs = new Kurs(kursKode, kursNavn, studiepoeng, maksAntallPlasser);
            kurs.Add(nyttKurs);
            Console.WriteLine($"Kurs '{kursNavn}' ({kursKode}) opprettet!");
            return nyttKurs;
        }
        
        // Finner kurs basert på kurskode
        public Kurs FinnKurs(string kursKode)
        {
            // LINQ (Language Integrated Query) for å søke i listen
            // FirstOrDefault returnerer første match eller null
            return kurs.FirstOrDefault(k => k.KursKode.Equals(kursKode, StringComparison.OrdinalIgnoreCase));
        }
        
        // Søker etter kurs basert på kode eller navn
        public List<Kurs> SøkKurs(string søkeord)
        {
            // LINQ Where for å filtrere listen
            // ToList() konverterer resultatet til en List
            return kurs.Where(k => 
                k.KursKode.Contains(søkeord, StringComparison.OrdinalIgnoreCase) ||
                k.KursNavn.Contains(søkeord, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }
        
        // Melder en student på et kurs
        public bool MeldPåKurs(string kursKode, Student student)
        {
            var kursObj = FinnKurs(kursKode);
            
            if (kursObj == null)
            {
                Console.WriteLine($"Feil: Fant ikke kurs med kode {kursKode}");
                return false;
            }
            
            if (kursObj.MeldPåStudent(student))
            {
                Console.WriteLine($"{student.Navn} ble meldt på {kursObj.KursNavn}");
                return true;
            }
            else
            {
                Console.WriteLine($"Feil: Kunne ikke melde {student.Navn} på {kursObj.KursNavn}. " +
                                "Kurset kan være fullt eller studenten allerede påmeldt.");
                return false;
            }
        }
        
        // Viser alle kurs med deltakere
        public void VisAlleKurs()
        {
            if (kurs.Count == 0)
            {
                Console.WriteLine("Ingen kurs registrert.");
                return;
            }
            
            Console.WriteLine("\n=== ALLE KURS ===");
            foreach (var k in kurs)
            {
                Console.WriteLine($"\n{k.GetInfo()}");
                
                if (k.PåmeldteStudenter.Count > 0)
                {
                    Console.WriteLine("Påmeldte studenter:");
                    foreach (var student in k.PåmeldteStudenter)
                    {
                        Console.WriteLine($"  - {student.Navn} ({student.StudentID})");
                    }
                }
                else
                {
                    Console.WriteLine("  Ingen påmeldte studenter");
                }
            }
            Console.WriteLine();
        }
        
        // Returnerer alle kurs (for testing/seeding)
        public List<Kurs> HentAlleKurs()
        {
            return kurs;
        }
    }
}
