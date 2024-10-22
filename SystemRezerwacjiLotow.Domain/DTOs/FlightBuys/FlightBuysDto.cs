using SystemRezerwacjiLotow.Domain.Models;

namespace SystemRezerwacjiLotow.Domain.DTOs.FlightBuys
{
    public class FlightBuysDto : BaseSearchModel <FlightBuy>
    {
        public List<FlightBuy> FlightBuys { get; set; } = new List<FlightBuy> ();
    }
}
