using System;

namespace UniversitetSystem.Models
{
    // Lån klassen representerer et utlån av en bok
    public class Lån
    {
        public Bok Bok { get; set; }
        public User Låntaker { get; set; }  // Kan være Student eller Ansatt
        public DateTime LånDato { get; set; }
        public DateTime? ReturnertDato { get; set; }  // ? betyr nullable - kan være null
        public bool ErAktiv { get; set; }
        
        // Constructor for nytt lån
        public Lån(Bok bok, User låntaker)
        {
            Bok = bok;
            Låntaker = låntaker;
            LånDato = DateTime.Now;  // Setter dagens dato
            ReturnertDato = null;    // Ikke returnert ennå
            ErAktiv = true;
        }
        
        // Metode for å returnere boken
        public void ReturnerBok()
        {
            if (ErAktiv)
            {
                ReturnertDato = DateTime.Now;
                ErAktiv = false;
                Bok.LeverInn();  // Oppdater bokens tilgjengelighet
            }
        }
        
        // Metode for å få info om lånet
        public string GetInfo()
        {
            string status = ErAktiv ? "Aktiv" : "Returnert";
            string returDato = ReturnertDato.HasValue 
                ? ReturnertDato.Value.ToShortDateString() 
                : "Ikke returnert";
            
            return $"{Bok.Tittel} - Lånt av: {Låntaker.Navn} ({Låntaker.GetUserId()}) - " +
                   $"Lånt: {LånDato.ToShortDateString()} - Status: {status} - Returnert: {returDato}";
        }
    }
}
