using SystemRezerwacjiLotow.Domain.Models;

namespace SystemRezerwacjiLotow.Domain.DTOs.FlightBuys
{
    public class FlightBuyDto : BaseModel
    {

        // Model Flight
        public string FlightId { get; set; }
        public string TrasaOd { get; set; }
        public string TrasaDo { get; set; }
        public double Price { get; set; }
        public string GodzinaWylotu { get; set; }
        public List <DzienWylotu> DniWylotu { get; set; }



        // Model FlightBuy
        public string? FlightBuyId { get; set; }
        public int IloscBiletow { get; set; }
        public double PriceSuma { get; set; }



        // Model Tenant
        public string? Imie { get; set; }
        public string? Nazwisko { get; set; }



        public string PrzyslugujaceZnizki { get; set; }
        public string? TenantEmail { get; set; } // nazwa emaila pobierana z HttpContext zalogowanego użytkownika.


    }
}
