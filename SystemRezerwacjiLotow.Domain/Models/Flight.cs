using System.ComponentModel.DataAnnotations;

namespace SystemRezerwacjiLotow.Domain.Models
{
    public class Flight
    {
        [Key]
        public string FlightId { get; private set; }
        public string TrasaOd { get; private set; }
        public string TrasaDo { get; private set; }
        public double Price { get; private set; }
        public string GodzinaWylotu { get; private set; }
        public string DataDodania { get; private set; }



        public string? TenantId { get; private set; }
        public Tenant? Tenant { get; private set; }


        public List<DzienWylotu>? DniWylotu { get; private set; }
        public List<FlightBuy>? FlightBuys { get; private set; }

         
        public Flight(string trasaOd, string trasaDo, double price, string godzinaWylotu)
        {
            FlightId = GenerateFlightId();
            TrasaOd = trasaOd;
            TrasaDo = trasaDo;
            Price = price;
            GodzinaWylotu = godzinaWylotu;
            DataDodania = DateTime.Now.ToString();
        }

        public Flight(string trasaOd, string trasaDo, double price, string godzinaWylotu, string tenantId)
        {
            FlightId = GenerateFlightId();
            TrasaOd = trasaOd;
            TrasaDo = trasaDo;
            Price = price;
            GodzinaWylotu = godzinaWylotu;
            TenantId = tenantId;
            DataDodania = DateTime.Now.ToString();
        }

        public void Update(string trasaOd, string trasaDo, double price, string godzinaWylotu)
        {
            TrasaOd = trasaOd;
            TrasaDo = trasaDo;
            Price = price;
            GodzinaWylotu = godzinaWylotu;
        }


        /// <summary>
        /// Generuje losowy numer jako identyfikato Id rekordu np. "KLM 12345 BCA",
        /// gdzie jedynym zmieniającym się elementem jest środkowa część ciągu z cudzysłowia
        /// </summary>
        private string GenerateFlightId()
        {
            Random random = new Random();
            int randomNumber = random.Next(10000, 100000); // Generuje losową liczbę 5-cyfrową
            string code = $"KLM {randomNumber} BCA";
            return code;
        }


    }
}
