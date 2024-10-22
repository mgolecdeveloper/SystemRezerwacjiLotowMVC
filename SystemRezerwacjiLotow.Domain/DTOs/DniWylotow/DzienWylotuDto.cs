using SystemRezerwacjiLotow.Domain.Models;

namespace SystemRezerwacjiLotow.Domain.DTOs.DniWylotow
{
    public class DzienWylotuDto : BaseModel
    {
        public string? DzieWylotuId { get; set; }
        public string? Dzien { get; set; }
        public string? FlightId { get; set; }
    }
}
