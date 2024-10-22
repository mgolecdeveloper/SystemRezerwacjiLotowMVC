
using SystemRezerwacjiLotow.Domain.Models;

namespace SystemRezerwacjiLotow.Domain.DTOs.Flights
{
    public class FlightDto : BaseModel
    {
        public string FlightId { get; set; }
        public string TrasaOd { get; set; }
        public string TrasaDo { get; set; }
        public double Price { get; set; }
        public string GodzinaWylotu { get; set; } 

        public string TenantId { get; set; }
        public string DniWylotu { get; set; } // string z nazwami dni oddzielonych przecinkiem, które potem zostaną przekształcone do listy i zapisane do bazy jako pojedyńcze rekordy
    }
}
