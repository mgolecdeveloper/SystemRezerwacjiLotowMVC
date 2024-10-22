
using SystemRezerwacjiLotow.Domain.Models;

namespace SystemRezerwacjiLotow.Domain.DTOs.KryteriaZnizek
{
    public class KryteriaZnizkiDto : BaseModel
    {
        public string? KryteriaZnizkiId { get; set; }
        public string? Name { get; set; }
        public string? FlightBuyId { get; set; }
    }
}
