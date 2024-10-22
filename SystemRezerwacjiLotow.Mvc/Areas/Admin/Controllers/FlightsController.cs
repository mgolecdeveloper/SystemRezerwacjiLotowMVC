using Microsoft.AspNetCore.Mvc;
using SystemRezerwacjiLotow.Application.Abstractions;
using SystemRezerwacjiLotow.Domain;
using SystemRezerwacjiLotow.Domain.Models;
using SystemRezerwacjiLotow.Domain.DTOs.FlightBuys;
using SystemRezerwacjiLotow.Domain.DTOs.Flights;
using SystemRezerwacjiLotow.Infrastruktura.Abstractions;

namespace SystemRezerwacjiLotow.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Administrator, Manager")]
    public class FlightsController : Controller
    {
        private readonly IFlightsRepository _flightsRepository;
        private readonly IFlightBuysRepository _flightBuysRepository;
        private readonly ITenantsService _tenantsService;

        public FlightsController(IFlightsRepository flightsRepository, IFlightBuysRepository flightBuysRepository, ITenantsService tenantsService)
        {
            _flightsRepository = flightsRepository;
            _flightBuysRepository = flightBuysRepository;
            _tenantsService = tenantsService;
        }


        [HttpGet]
        public async Task<IActionResult> Index(FlightsDto model)
        {
            var flights = await _flightsRepository.GetAll();  

            return View(new FlightsDto()
            {
                Flights = flights,
                Paginator = Paginator<Flight>.CreateAsync(flights, model.PageIndex, model.PageSize), 
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string s, FlightsDto model)
        {
            var flights = await _flightsRepository.GetAll();
             

            model.Flights = flights;
            model.Paginator = Paginator<Flight>.CreateAsync(flights, model.PageIndex, model.PageSize);
            return View(model);
        }




        [HttpGet]
        public async Task<IActionResult> Create()
        { 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FlightDto model)
        {
            var result = await _flightsRepository.Create(model);
            if (result.Success)
                return RedirectToAction("Index", "Flights");

            return View(model);
        }
         

        [HttpGet]
        public async Task<IActionResult> KupBilet(string flightId)
        {
            if (string.IsNullOrEmpty(flightId))
                return NotFound();



            // pobierz email zalogowanego tenanta i przekaż go do modelu
            string tenantEmail = "admin@admin.pl";
            if (HttpContext.User != null && HttpContext.User.Identity != null && !string.IsNullOrEmpty (HttpContext.User.Identity.Name))
                tenantEmail = HttpContext.User.Identity.Name;


            var flight = await _flightsRepository.Get(flightId);
            if (flight == null)
                return NotFound(); 

            var tenant = flight.Tenant;
            if (tenant == null)
                return NotFound ();


            // sprawdza jakie zniżki przysługują kupującemu
            List<string> przyslugujaceZnizki = await SprawdzCzyTenantowiPrzyslugujeZnizka (flight, 1, tenantEmail);

            return View(new FlightBuyDto() 
            { 
                // Model Flight
                FlightId = flight.FlightId,
                TrasaOd = flight.TrasaOd,
                TrasaDo = flight.TrasaDo,
                Price = flight.Price,
                GodzinaWylotu = flight.GodzinaWylotu,
                DniWylotu = flight.DniWylotu,


                // Model FlightBuy  
                IloscBiletow = 1,


                // Model Tenant
                Imie = tenant.Imie,
                Nazwisko = tenant.Nazwisko,

                TenantEmail = tenantEmail,
                PrzyslugujaceZnizki = string.Concat(przyslugujaceZnizki)
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> KupBilet(FlightBuyDto model)
        {
            var result = await _flightBuysRepository.Create(model);
            if (result.Success)
                return RedirectToAction("Potwierdzenie", "Flights");


            var flight = await _flightsRepository.Get(model.FlightId);
            if (flight == null)
                return NotFound();


            List<string> przyslugujaceZnizki = await SprawdzCzyTenantowiPrzyslugujeZnizka(flight, model.IloscBiletow, model.TenantEmail);

            model.DniWylotu = flight.DniWylotu;
            model.PrzyslugujaceZnizki = string.Concat (przyslugujaceZnizki);
            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> Potwierdzenie()
        {
            return View();
        }





        /// <summary>
        /// Metoda sprawdza jakie zniżki przysługujsą kupującemu
        /// Program daje możliwość zakupu tenantowi większej ilości biletów, ale standardowo przypisany jest 1
        /// </summary>
        private async Task<List<string>> SprawdzCzyTenantowiPrzyslugujeZnizka(Flight flight, int iloscBiletow, string tenantEmail)
        {
            List<string> przyslugujaceZnizkiLista = new List<string>();
            double wysokoscZnizki = 5;

            double cenaBiletu = flight.Price;

            // pobierz dane zalogowanego tenanta
            var tenant = await _tenantsService.GetTenantByEmail(tenantEmail);
            if (tenant != null)
            {

                // sprawdzanie cen biletu/lotu
                // I warunek - możliwość zastosowania 2 i więcej zniżek
                if (flight.Price >= 30)
                {
                    przyslugujaceZnizkiLista.Clear();
                    // I przypadek - zastosowanie zniżki na datę urodzin w tym przedziale ceny
                    // sprawdź czy tenant ma dziś urodziny
                    var d = DateTime.Now;
                    var dt = DateTime.Parse(tenant.DataUrodzenia);
                    if (d.Month == dt.Month && d.Day == dt.Day)
                    {
                        // tenant ma dziś urodziny, wiec zastosowana jest zniżka na cenę
                        // nazwa zniżki pobierana jest z kolumny Name z tabeli KryteriaZnizek
                        //await ZastosujZnizke("Urodziny kupującego", flightBuy.FlightBuyId, tenant);

                        // oblicz wartość biletu/lotu po zastosowaniu zniżki
                        cenaBiletu = cenaBiletu - wysokoscZnizki;
                        przyslugujaceZnizkiLista.Add("Urodziny kupującego");
                    }


                    // II przypadek - zastosowanie zniżki na kraj w tym przedziale ceny
                    // sprawdź czy tenant wybrał lot do Afryki
                    if (flight.TrasaDo == "Afryka")
                    {
                        // klient wybrał lot do Afryki, więc zastosowana jest wynikjąca z wybranego kraju
                        // nazwa zniżki pobierana jest z kolumny Name z tabeli KryteriaZnizek
                        //await ZastosujZnizke("Lot do Afryki", flightBuy.FlightBuyId, tenant);

                        // oblicz wartość biletu/lotu po zastosowaniu zniżki
                        cenaBiletu = cenaBiletu - wysokoscZnizki;
                        przyslugujaceZnizkiLista.Add("Afryka");
                    }

                    // wykonanie obliczeń dla sumy zakupów wynikających z ilości wybranych biletów/lotów oraz ceny biletu/lotu i przypisanie obliczeń do modelu danych
                    double priceSuma = cenaBiletu;
                    priceSuma = priceSuma * iloscBiletow; // cena pojedyńczego biletu * ilość biletów = suma do zapłaty, czyli jeśli klient kupi 3 bilety i będzie miał dzisiaj urodziny to zostanie zastosowana zniżka 3 biletów

                }
                // II warunek - można zastosować 1 zniżkę na 1 wybrane kryterium np "Urodziny kupujacego" lub "Lot do Afryki", w tym przypadku wybrano lot do Afryki
                else if (flight.Price >= 21 && flight.Price <= 31)
                {
                    przyslugujaceZnizkiLista.Clear();

                    if (flight.TrasaDo == "Afryka")
                    {
                        // klient wybrał lot do Afryki, więc zastosowana jest wynikjąca z wybranego kraju
                        // nazwa zniżki pobierana jest z kolumny Name z tabeli KryteriaZnizek
                        //await ZastosujZnizke("Lot do Afryki", flightBuy.FlightBuyId, tenant);

                        // oblicz wartość biletu/lotu po zastosowaniu zniżki
                        cenaBiletu = cenaBiletu - wysokoscZnizki;


                        // wykonanie obliczeń dla sumy zakupów wynikających z ilości wybranych biletów/lotów oraz ceny biletu/lotu i przypisanie obliczeń do modelu danych
                        double priceSuma = cenaBiletu;
                        priceSuma = priceSuma * iloscBiletow; // cena pojedyńczego biletu * ilość biletów = suma do zapłaty, czyli jeśli klient kupi 3 bilety i będzie miał dzisiaj urodziny to zostanie zastosowana zniżka 3 biletów


                        przyslugujaceZnizkiLista.Add("Afryka");
                    }

                }
                // III waruenk - nie można zastosować żadnej zniżki
                else if (flight.Price <= 21)
                {

                }


            }

            return przyslugujaceZnizkiLista;
        }


    }
}
