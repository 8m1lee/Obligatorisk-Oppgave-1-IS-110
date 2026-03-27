using System;
using System.Collections.Generic;
using System.Linq;
using UniversitetSystem.Models;

namespace UniversitetSystem.Services
{
    // Service klasse for å håndtere alle bibliotek-relaterte operasjoner
    public class BibliotekService
    {
        private List<Bok> bøker;
        private List<Lån> lån;  // Historikk over alle lån (aktive og avsluttede)
        
        public BibliotekService()
        {
            bøker = new List<Bok>();
            lån = new List<Lån>();
        }
        
        // Registrerer en ny bok i biblioteket
        public Bok RegistrerBok(string bokId, string tittel, string forfatter, 
                                 int utgivelsesår, int antallEksemplarer)
        {
            // Sjekk om BokID allerede eksisterer
            if (bøker.Any(b => b.BokID == bokId))
            {
                Console.WriteLine($"Feil: Bok med ID {bokId} eksisterer allerede.");
                return null;
            }
            
            var nyBok = new Bok(bokId, tittel, forfatter, utgivelsesår, antallEksemplarer);
            bøker.Add(nyBok);
            Console.WriteLine($"Bok '{tittel}' registrert i biblioteket!");
            return nyBok;
        }
        
        // Finner en bok basert på ID
        public Bok FinnBok(string bokId)
        {
            return bøker.FirstOrDefault(b => b.BokID.Equals(bokId, StringComparison.OrdinalIgnoreCase));
        }
        
        // Søker etter bøker basert på tittel eller forfatter
        public List<Bok> SøkBok(string søkeord)
        {
            return bøker.Where(b =>
                b.Tittel.Contains(søkeord, StringComparison.OrdinalIgnoreCase) ||
                b.Forfatter.Contains(søkeord, StringComparison.OrdinalIgnoreCase) ||
                b.BokID.Contains(søkeord, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }
        
        // Låner ut en bok til en bruker
        public bool LånBok(string bokId, User bruker)
        {
            var bok = FinnBok(bokId);
            
            if (bok == null)
            {
                Console.WriteLine($"Feil: Fant ikke bok med ID {bokId}");
                return false;
            }
            
            // Sjekk om brukeren allerede har et aktivt lån av denne boken
            var eksisterendeLån = lån.FirstOrDefault(l => 
                l.Bok.BokID == bokId && 
                l.Låntaker.GetUserId() == bruker.GetUserId() && 
                l.ErAktiv
            );
            
            if (eksisterendeLån != null)
            {
                Console.WriteLine($"Feil: {bruker.Navn} har allerede lånt denne boken.");
                return false;
            }
            
            // Prøv å låne ut boken
            if (bok.LånUt())
            {
                var nyttLån = new Lån(bok, bruker);
                lån.Add(nyttLån);
                Console.WriteLine($"{bruker.Navn} har lånt '{bok.Tittel}'");
                return true;
            }
            else
            {
                Console.WriteLine($"Feil: '{bok.Tittel}' er ikke tilgjengelig for utlån.");
                return false;
            }
        }
        
        // Returnerer en bok
        public bool ReturnerBok(string bokId, User bruker)
        {
            // Finn aktivt lån for denne boken og brukeren
            var aktivtLån = lån.FirstOrDefault(l =>
                l.Bok.BokID.Equals(bokId, StringComparison.OrdinalIgnoreCase) &&
                l.Låntaker.GetUserId() == bruker.GetUserId() &&
                l.ErAktiv
            );
            
            if (aktivtLån == null)
            {
                Console.WriteLine($"Feil: Fant ikke aktivt lån av bok {bokId} for {bruker.Navn}");
                return false;
            }
            
            aktivtLån.ReturnerBok();
            Console.WriteLine($"{bruker.Navn} har returnert '{aktivtLån.Bok.Tittel}'");
            return true;
        }
        
        // Viser alle aktive lån
        public void VisAktiveLån()
        {
            var aktiveLån = lån.Where(l => l.ErAktiv).ToList();
            
            if (aktiveLån.Count == 0)
            {
                Console.WriteLine("Ingen aktive lån.");
                return;
            }
            
            Console.WriteLine("\n=== AKTIVE LÅN ===");
            foreach (var l in aktiveLån)
            {
                Console.WriteLine(l.GetInfo());
            }
            Console.WriteLine();
        }
        
        // Viser historikk over alle lån (både aktive og returnerte)
        public void VisLånehistorikk()
        {
            if (lån.Count == 0)
            {
                Console.WriteLine("Ingen lånehistorikk.");
                return;
            }
            
            Console.WriteLine("\n=== LÅNEHISTORIKK ===");
            foreach (var l in lån)
            {
                Console.WriteLine(l.GetInfo());
            }
            Console.WriteLine();
        }
        
        // Viser alle bøker i biblioteket
        public void VisAlleBøker()
        {
            if (bøker.Count == 0)
            {
                Console.WriteLine("Ingen bøker registrert.");
                return;
            }
            
            Console.WriteLine("\n=== ALLE BØKER ===");
            foreach (var bok in bøker)
            {
                Console.WriteLine(bok.GetInfo());
            }
            Console.WriteLine();
        }
    }
}
