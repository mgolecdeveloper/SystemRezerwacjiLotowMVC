using System.ComponentModel.DataAnnotations;

namespace SystemRezerwacjiLotow.Domain.Models
{
    public class DzienWylotu
    {
        [Key]
        public string DzieWylotuId { get; private set; }
        public string Dzien { get; private set; }


        public string? FlightId { get; private set; }
        public Flight? Flight { get; private set; }

         
         
        public DzienWylotu(string dzien, string flightId)
        {
            DzieWylotuId = Guid.NewGuid().ToString();
            Dzien = dzien;
            FlightId = flightId;
        }


        public void Update (string dzien, string flightId)
        {
            Dzien = dzien;
            FlightId = flightId;
        }
    }
}
