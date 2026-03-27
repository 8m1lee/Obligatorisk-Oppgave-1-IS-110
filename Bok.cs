using System;

namespace UniversitetSystem.Models
{
    // Bok klassen representerer en bok eller media i biblioteket
    public class Bok
    {
        public string BokID { get; set; }
        public string Tittel { get; set; }
        public string Forfatter { get; set; }
        public int Utgivelsesår { get; set; }
        public int AntallEksemplarer { get; set; }      // Totalt antall eksemplarer
        public int TilgjengeligeEksemplarer { get; set; } // Antall tilgjengelige nå
        
        // Constructor
        public Bok(string bokId, string tittel, string forfatter, 
                   int utgivelsesår, int antallEksemplarer)
        {
            BokID = bokId;
            Tittel = tittel;
            Forfatter = forfatter;
            Utgivelsesår = utgivelsesår;
            AntallEksemplarer = antallEksemplarer;
            TilgjengeligeEksemplarer = antallEksemplarer; // I starten er alle tilgjengelige
        }
        
        // Sjekk om boken er tilgjengelig for utlån
        public bool ErTilgjengelig()
        {
            return TilgjengeligeEksemplarer > 0;
        }
        
        // Metode som kalles når noen låner boken
        public bool LånUt()
        {
            if (ErTilgjengelig())
            {
                TilgjengeligeEksemplarer--;
                return true;
            }
            return false;
        }
        
        // Metode som kalles når noen leverer tilbake boken
        public void LeverInn()
        {
            // Kan ikke ha flere tilgjengelige enn totalt antall
            if (TilgjengeligeEksemplarer < AntallEksemplarer)
            {
                TilgjengeligeEksemplarer++;
            }
        }
        
        // Metode for å få en tekstrepresentasjon av boken
        public string GetInfo()
        {
            return $"[{BokID}] {Tittel} av {Forfatter} ({Utgivelsesår}) - " +
                   $"Tilgjengelig: {TilgjengeligeEksemplarer}/{AntallEksemplarer}";
        }
    }
}
