using System.ComponentModel.DataAnnotations;

namespace SystemRezerwacjiLotow.Domain.Models
{
    public class FlightBuyKryteriaZnizki
    {
        [Key]
        public string FlightBuyId { get; private set; }
        public FlightBuy FlightBuy { get; private set; }


        public string KryteriaZnizkiId { get; private set; }
        public KryteriaZnizki KryteriaZnizki { get; private set; }


        public FlightBuyKryteriaZnizki(string flightBuyId, string kryteriaZnizkiId)
        {
            FlightBuyId = flightBuyId;
            KryteriaZnizkiId = kryteriaZnizkiId;
        }
    }
}
