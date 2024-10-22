using System.ComponentModel.DataAnnotations;

namespace SystemRezerwacjiLotow.Domain.Models
{
    public class FlightBuy
    {
        [Key]
        public string FlightBuyId { get; private set; }

        public int IloscBiletow { get; private set; }
        public double PriceSuma { get; private set; }
	    public string DataZakupu { get; private set; }


        public string? FlightId { get; private set; }
        public Flight? Flight { get; private set; }


        public string? TenantId { get; private set; }
        public Tenant? Tenant { get; private set; }



        // Relacja wiele-do-wielu
        public List<FlightBuyKryteriaZnizki>? FlightBuysKryteriaZnizkis { get; private set; }



        public FlightBuy(int iloscBiletow, double priceSuma, string flightId, string tenantId)
        {
            FlightBuyId = Guid.NewGuid().ToString();
            IloscBiletow = iloscBiletow;
            PriceSuma = priceSuma;
            FlightId = flightId;
            TenantId = tenantId;
            DataZakupu = DateTime.Now.ToString();
        }

        public void Update (int iloscBiletow, double priceSuma)
        {
            IloscBiletow = iloscBiletow;
            PriceSuma = priceSuma;
        }

    }
}
