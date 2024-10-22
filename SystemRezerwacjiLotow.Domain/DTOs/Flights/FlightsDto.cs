using SystemRezerwacjiLotow.Domain.Models;

namespace SystemRezerwacjiLotow.Domain.DTOs.Flights
{
    public class FlightsDto : BaseSearchModel<Flight>
    {
        public List<Flight>? Flights { get; set; }
    }
}
