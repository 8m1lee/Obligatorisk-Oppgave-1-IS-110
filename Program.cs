using System;
using UniversitetSystem.Models;
using UniversitetSystem.Services;

namespace UniversitetSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // Opprett hovedsystemet
            var system = new UniversitySystem();
            
            // Legg til testdata slik at systemet ikke er tomt ved oppstart
            ;
            
            bool running = true;
            
            // Hovedløkke for programmet
            while (running)
            {
                VisHovedmeny();
                string valg = Console.ReadLine();
                
                // Switch statement for å håndtere brukerens valg
                switch (valg)
                {
                    case "1":
                        OpprettKurs(system);
                        break;
                    case "2":
                        MeldStudentTilKurs(system);
                        break;
                    case "3":
                        system.KursService.VisAlleKurs();
                        break;
                    case "4":
                        SøkPåKurs(system);
                        break;
                    case "5":
                        SøkPåBok(system);
                        break;
                    case "6":
                        LånBok(system);
                        break;
                    case "7":
                        ReturnerBok(system);
                        break;
                    case "8":
                        RegistrerBok(system);
                        break;
                    case "0":
                        Console.WriteLine("Avslutter programmet...");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Ugyldig valg. Prøv igjen.");
                        break;
                }
                
                // Pause før neste menyvisning
                if (running)
                {
                    Console.WriteLine("\nTrykk en tast for å fortsette...");
                    Console.ReadKey();
                }
            }
        }
        
        // Viser hovedmenyen
        static void VisHovedmeny()
        {
            Console.Clear();
            Console.WriteLine("════════════════════════════════════════");
            Console.WriteLine("       UNIVERSITETS SYSTEM");
            Console.WriteLine("════════════════════════════════════════");
            Console.WriteLine("[1] Opprett kurs");
            Console.WriteLine("[2] Meld student til kurs");
            Console.WriteLine("[3] Print kurs og deltagere");
            Console.WriteLine("[4] Søk på kurs");
            Console.WriteLine("[5] Søk på bok");
            Console.WriteLine("[6] Lån bok");
            Console.WriteLine("[7] Returner bok");
            Console.WriteLine("[8] Registrer bok");
            Console.WriteLine("[0] Avslutt");
            Console.WriteLine("════════════════════════════════════════");
            Console.Write("Velg et alternativ: ");
        }
        
        // Metode for å opprette et nytt kurs
        static void OpprettKurs(UniversitySystem system)
        {
            Console.Clear();
            Console.WriteLine("=== OPPRETT NYTT KURS ===\n");
            
            Console.Write("Kurskode (f.eks. DAT100): ");
            string kursKode = Console.ReadLine();
            
            Console.Write("Kursnavn: ");
            string kursNavn = Console.ReadLine();
            
            Console.Write("Studiepoeng: ");
            if (!int.TryParse(Console.ReadLine(), out int studiepoeng))
            {
                Console.WriteLine("Ugyldig tall for studiepoeng.");
                return;
            }
            
            Console.Write("Maks antall plasser: ");
            if (!int.TryParse(Console.ReadLine(), out int maksPlasser))
            {
                Console.WriteLine("Ugyldig tall for maks plasser.");
                return;
            }
            
            system.KursService.OpprettKurs(kursKode, kursNavn, studiepoeng, maksPlasser);
        }
        
        // Metode for å melde en student på et kurs
        static void MeldStudentTilKurs(UniversitySystem system)
        {
            Console.Clear();
            Console.WriteLine("=== MELD STUDENT TIL KURS ===\n");
            
            Console.Write("StudentID: ");
            string studentId = Console.ReadLine();
            
            var student = system.FinnStudent(studentId);
            if (student == null)
            {
                Console.WriteLine($"Feil: Fant ingen student med ID {studentId}");
                return;
            }
            
            Console.WriteLine($"Student funnet: {student.Navn}");
            Console.Write("Kurskode: ");
            string kursKode = Console.ReadLine();
            
            system.KursService.MeldPåKurs(kursKode, student);
        }
        
        // Metode for å søke etter kurs
        static void SøkPåKurs(UniversitySystem system)
        {
            Console.Clear();
            Console.WriteLine("=== SØK PÅ KURS ===\n");
            
            Console.Write("Søkeord (kurskode eller kursnavn): ");
            string søkeord = Console.ReadLine();
            
            var resultat = system.KursService.SøkKurs(søkeord);
            
            if (resultat.Count == 0)
            {
                Console.WriteLine("Ingen kurs funnet.");
                return;
            }
            
            Console.WriteLine($"\nFant {resultat.Count} kurs:\n");
            foreach (var kurs in resultat)
            {
                Console.WriteLine(kurs.GetInfo());
            }
        }
        
        // Metode for å søke etter bøker
        static void SøkPåBok(UniversitySystem system)
        {
            Console.Clear();
            Console.WriteLine("=== SØK PÅ BOK ===\n");
            
            Console.Write("Søkeord (tittel, forfatter eller ID): ");
            string søkeord = Console.ReadLine();
            
            var resultat = system.BibliotekService.SøkBok(søkeord);
            
            if (resultat.Count == 0)
            {
                Console.WriteLine("Ingen bøker funnet.");
                return;
            }
            
            Console.WriteLine($"\nFant {resultat.Count} bok(er):\n");
            foreach (var bok in resultat)
            {
                Console.WriteLine(bok.GetInfo());
            }
        }
        
        // Metode for å låne en bok
        static void LånBok(UniversitySystem system)
        {
            Console.Clear();
            Console.WriteLine("=== LÅN BOK ===\n");
            
            Console.Write("BrukerID (StudentID eller AnsattID): ");
            string brukerId = Console.ReadLine();
            
            var bruker = system.FinnBruker(brukerId);
            if (bruker == null)
            {
                Console.WriteLine($"Feil: Fant ingen bruker med ID {brukerId}");
                return;
            }
            
            Console.WriteLine($"Bruker funnet: {bruker.Navn}");
            Console.Write("BokID: ");
            string bokId = Console.ReadLine();
            
            system.BibliotekService.LånBok(bokId, bruker);
        }
        
        // Metode for å returnere en bok
        static void ReturnerBok(UniversitySystem system)
        {
            Console.Clear();
            Console.WriteLine("=== RETURNER BOK ===\n");
            
            Console.Write("BrukerID (StudentID eller AnsattID): ");
            string brukerId = Console.ReadLine();
            
            var bruker = system.FinnBruker(brukerId);
            if (bruker == null)
            {
                Console.WriteLine($"Feil: Fant ingen bruker med ID {brukerId}");
                return;
            }
            
            Console.WriteLine($"Bruker funnet: {bruker.Navn}");
            Console.Write("BokID: ");
            string bokId = Console.ReadLine();
            
            system.BibliotekService.ReturnerBok(bokId, bruker);
        }
        
        // Metode for å registrere en ny bok
        static void RegistrerBok(UniversitySystem system)
        {
            Console.Clear();
            Console.WriteLine("=== REGISTRER NY BOK ===\n");
            
            Console.Write("BokID: ");
            string bokId = Console.ReadLine();
            
            Console.Write("Tittel: ");
            string tittel = Console.ReadLine();
            
            Console.Write("Forfatter: ");
            string forfatter = Console.ReadLine();
            
            Console.Write("Utgivelsesår: ");
            if (!int.TryParse(Console.ReadLine(), out int år))
            {
                Console.WriteLine("Ugyldig år.");
                return;
            }
            
            Console.Write("Antall eksemplarer: ");
            if (!int.TryParse(Console.ReadLine(), out int antall))
            {
                Console.WriteLine("Ugyldig antall.");
                return;
            }
            
            system.BibliotekService.RegistrerBok(bokId, tittel, forfatter, år, antall);
        }
        
        // Metode for å legge til testdata i systemet
        static void SeedData(UniversitySystem system)
        {
            // Opprett noen studenter
            var student1 = new Student("S001", "Ola Nordmann", "ola@student.no");
            var student2 = new Student("S002", "Kari Hansen", "kari@student.no");
            var student3 = new UtvekslingStudent("S003", "John Smith", "john@student.no",
                "Harvard University", "USA", 
                new DateTime(2025, 8, 1), new DateTime(2026, 6, 1));
            
            
            system.LeggTilStudent(student1);
            system.LeggTilStudent(student2);
            system.LeggTilStudent(student3);
          
            // Opprett noen ansatte
            var ansatt1 = new Ansatt("A001", "Per Olsen", "per@universitetet.no", 
                "Foreleser", "Informatikk");
            var ansatt2 = new Ansatt("A002", "Line Berg", "line@universitetet.no", 
                "Bibliotekar", "Bibliotek");
            
            system.LeggTilAnsatt(ansatt1);
            system.LeggTilAnsatt(ansatt2);
            
            // Opprett noen kurs
            var kurs1 = system.KursService.OpprettKurs("DAT100", "Programmering", 10, 30);
            var kurs2 = system.KursService.OpprettKurs("MAT101", "Matematikk 1", 10, 25);
            var kurs3 = system.KursService.OpprettKurs("INF102", "Algoritmer", 10, 20);
            
            // Meld noen studenter på kurs
            system.KursService.MeldPåKurs("DAT100", student1);
            system.KursService.MeldPåKurs("DAT100", student2);
            system.KursService.MeldPåKurs("MAT101", student1);
            
            // Registrer noen bøker
            system.BibliotekService.RegistrerBok("B001", "C# Programming", "Anders Hejlsberg", 2020, 5);
            system.BibliotekService.RegistrerBok("B002", "Clean Code", "Robert Martin", 2008, 3);
            system.BibliotekService.RegistrerBok("B003", "Design Patterns", "Gang of Four", 1994, 2);
            
            // Lån ut noen bøker
            system.BibliotekService.LånBok("B001", student1);
            system.BibliotekService.LånBok("B002", ansatt1);
            
            Console.Clear();
            Console.WriteLine("Testdata lastet inn i systemet!");
            Console.WriteLine("- 3 studenter (inkludert 1 utvekslingsstudent)");
            Console.WriteLine("- 2 ansatte");
            Console.WriteLine("- 3 kurs");
            Console.WriteLine("- 3 bøker");
            Console.WriteLine("\nTrykk en tast for å fortsette...");
            Console.ReadKey();
        }
    }
}
