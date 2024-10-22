using Microsoft.EntityFrameworkCore;
using SystemRezerwacjiLotow.Domain.Models;
using SystemRezerwacjiLotow.Domain.Models.Enums;
using SystemRezerwacjiLotow.Domain.DTOs;
using SystemRezerwacjiLotow.Domain.DTOs.FlightBuys;
using SystemRezerwacjiLotow.Infrastruktura.Abstractions;

namespace SystemRezerwacjiLotow.Infrastruktura
{
    public class FlightBuysRepository : IFlightBuysRepository
    {
        private readonly ApplicationDbContext _context;
        private const double wysokoscZnizki = 5; // standardowa kwota zniżki w euro
        public FlightBuysRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<FlightBuy>> GetAll()
        {
            return await _context.FlightBuys
                .Include(i => i.Flight)
                .Include(i => i.Tenant)
                .Include(i => i.FlightBuysKryteriaZnizkis)
                .OrderByDescending (o=> o.DataZakupu)
                .ToListAsync();
        }

        public async Task<FlightBuy> Get(string flightBuyId)
        {
            return await _context.FlightBuys
                .Include(i => i.Flight)
                .Include(i => i.Tenant)
                .FirstOrDefaultAsync(f => f.FlightBuyId == flightBuyId);
        }






        public async Task<FlightBuyDto> Create(FlightBuyDto model)
        {
            if (model != null)
            {
                try
                {
                    var flight = await _context.Flights.FirstOrDefaultAsync(f => f.FlightId == model.FlightId);
                    var tenant = await _context.Tenants.FirstOrDefaultAsync(f => f.Email == "admin@admin.pl");

                    if (tenant != null && flight != null)
                    {
                        double suma = model.IloscBiletow * model.Price;
                        FlightBuy flightBuy = new FlightBuy(
                            iloscBiletow: model.IloscBiletow,
                            priceSuma: suma,
                            flightId: flight.FlightId,
                            tenantId: tenant.Id
                            );


                        // zapisanie rekordu do bazy
                        _context.FlightBuys.Add(flightBuy);
                        await _context.SaveChangesAsync();


                        // sprawdza czy tenantowi przysługuje zniżka
                        await SprawdzCzyTenantowiPrzyslugujeZnizka(model, flight, flightBuy, tenant);

                         

                        model.Success = true;
                    }
                }
                catch (Exception ex)
                {
                    model = new FlightBuyDto() { Success = false, Message = ex.Message };
                }
            }
            else
            {
                model = new FlightBuyDto() { Success = false, Message = "Model was null" };
            }
            return model;
        }






        public async Task<FlightBuyDto> Update(FlightBuyDto model)
        {
            if (model != null)
            {
                try
                {
                    var flight = await _context.Flights.FirstOrDefaultAsync(f => f.FlightId == model.FlightId);
                    var tenant = await _context.Tenants.FirstOrDefaultAsync(f => f.Email == model.TenantEmail);

                    if (flight != null && tenant != null)
                    {
                        var flightBuy = await _context.FlightBuys.FirstOrDefaultAsync(f => f.FlightBuyId == model.FlightBuyId);

                        if (flightBuy != null)
                        {
                            flightBuy.Update (
                                iloscBiletow: model.IloscBiletow,
                                priceSuma: model.PriceSuma
                                );

                            _context.Entry(tenant).State = EntityState.Modified;
                            await _context.SaveChangesAsync();


                            model.Success = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    model = new FlightBuyDto() { Success = false, Message = ex.Message };
                }
            }
            else
            {
                model = new FlightBuyDto() { Success = false, Message = "Model was null" };
            }
            return model;
        }



        public async Task<DeleteResult> Delete(string flightBuyId)
        {
            DeleteResult deleteResult = new DeleteResult() { Success = false, Message = "Usuwanie rekordu nie powiodło się" };
            try
            {
                var flightBuy = await _context.FlightBuys.FirstOrDefaultAsync(f => f.FlightBuyId == flightBuyId);
                if (flightBuy != null)
                {
                    _context.FlightBuys.Remove(flightBuy);
                    await _context.SaveChangesAsync();

                    deleteResult.Success = true;
                    deleteResult.Message = "Usuwanie rekordu powiodło się";
                }
            }
            catch (Exception ex)
            {
                deleteResult.Message = ex.Message;
            }
            return deleteResult;
        }



        /// <summary>
        /// Metoda sprawdza czy tenantowi przysługuje zniżka. Sprawdzane są 3 przedziały cen.
        /// Program daje możliwość zakupu tenantowi większej ilości biletów, ale standardowo przypisany jest 1
        /// </summary>
        private async Task SprawdzCzyTenantowiPrzyslugujeZnizka(FlightBuyDto model, Flight flight, FlightBuy flightBuy, Tenant tenant)
        {

            double iloscBiletow = model.IloscBiletow;
            double cenaBiletu = flight.Price;

            // sprawdzanie cen biletu/lotu
            // I warunek - możliwość zastosowania 2 i więcej zniżek
            if (flight.Price >= 30)
            {
                // I przypadek - zastosowanie zniżki na datę urodzin w tym przedziale ceny
                // sprawdź czy tenant ma dziś urodziny
                var d = DateTime.Now;
                var dt = DateTime.Parse(tenant.DataUrodzenia);
                if (d.Month == dt.Month && d.Day == dt.Day)
                {
                    // tenant ma dziś urodziny, wiec zastosowana jest zniżka na cenę
                    // nazwa zniżki pobierana jest z kolumny Name z tabeli KryteriaZnizek
                    await ZastosujZnizke("Urodziny kupującego", flightBuy.FlightBuyId, tenant);

                    // oblicz wartość biletu/lotu po zastosowaniu zniżki
                    cenaBiletu = cenaBiletu - wysokoscZnizki;
                }


                // II przypadek - zastosowanie zniżki na kraj w tym przedziale ceny
                // sprawdź czy tenant wybrał lot do Afryki
                if (flight.TrasaDo == "Afryka")
                {
                    // klient wybrał lot do Afryki, więc zastosowana jest wynikjąca z wybranego kraju
                    // nazwa zniżki pobierana jest z kolumny Name z tabeli KryteriaZnizek
                    await ZastosujZnizke("Lot do Afryki", flightBuy.FlightBuyId, tenant);

                    // oblicz wartość biletu/lotu po zastosowaniu zniżki
                    cenaBiletu = cenaBiletu - wysokoscZnizki;
                }

                // wykonanie obliczeń dla sumy zakupów wynikających z ilości wybranych biletów/lotów oraz ceny biletu/lotu i przypisanie obliczeń do modelu danych
                double priceSuma = cenaBiletu;
                priceSuma = priceSuma * model.IloscBiletow; // cena pojedyńczego biletu * ilość biletów = suma do zapłaty, czyli jeśli klient kupi 3 bilety i będzie miał dzisiaj urodziny to zostanie zastosowana zniżka 3 biletów
                flightBuy.Update (model.IloscBiletow, priceSuma);
            }
            // II warunek - można zastosować 1 zniżkę na 1 wybrane kryterium np "Lot do Afryki"
            else if (flight.Price >= 21 && flight.Price <= 31)
            {
                if (flight.TrasaDo == "Afryka")
                {
                    // klient wybrał lot do Afryki, więc zastosowana jest wynikjąca z wybranego kraju
                    // nazwa zniżki pobierana jest z kolumny Name z tabeli KryteriaZnizek
                    await ZastosujZnizke("Lot do Afryki", flightBuy.FlightBuyId, tenant);

                    // oblicz wartość biletu/lotu po zastosowaniu zniżki
                    cenaBiletu = cenaBiletu - wysokoscZnizki;


                    // wykonanie obliczeń dla sumy zakupów wynikających z ilości wybranych biletów/lotów oraz ceny biletu/lotu i przypisanie obliczeń do modelu danych
                    double priceSuma = cenaBiletu;
                    priceSuma = priceSuma * model.IloscBiletow; // cena pojedyńczego biletu * ilość biletów = suma do zapłaty, czyli jeśli klient kupi 3 bilety i będzie miał dzisiaj urodziny to zostanie zastosowana zniżka 3 biletów
                    flightBuy.Update(model.IloscBiletow, priceSuma);
                }

            }
            // III waruenk - nie można zastosować żadnej zniżki
            else if (flight.Price <= 21)
            { 

            }



            _context.Entry (flightBuy).State = EntityState.Modified;
            await _context.SaveChangesAsync ();
        }



        /// <summary>
        /// Logika odpowiedzialna za zapisywanie kryteriów zniżek tylko i wyłącznie dla tanenta należącego do grupyA,
        /// oczywiście zanim zostanie zapisany rekord wcześniej sprawdzana jest logika związana z przedziałem cen
        /// </summary>
        /// <param name="nazwaKryteriumZnizki">Kryteria zniżki pobierane są z bazy</param>
        private async Task ZastosujZnizke(string nazwaKryteriumZnizki, string flightBuyId, Tenant tenant)
        {
            // kryterium znizki wyszukiwane po unikalnej nazwie (nazwy kryteriów w bazie nie mogą się powtarzać)
            var kryteriumZnizki = await _context.KryteriaZnizek.FirstOrDefaultAsync(f => f.Name == nazwaKryteriumZnizki);
            if (kryteriumZnizki != null)
            {

                switch (tenant.TenantKind)
                {
                    // GrupaA dla której można zapisywać kryteria zniżek dla każdego zakupu
                    case TenantGroupKind.GroupA:

                        FlightBuyKryteriaZnizki flightBuyKryteriaZnizki = new FlightBuyKryteriaZnizki(
                            flightBuyId: flightBuyId,
                            kryteriaZnizkiId: kryteriumZnizki.KryteriaZnizkiId
                            );

                        _context.FlightBuysKryteriaZnizkis.Add(flightBuyKryteriaZnizki);
                        await _context.SaveChangesAsync();

                        break;

                    // GrupaB dla której NIE można zapisywać kryteriów zastosowanych do zakupu
                    case TenantGroupKind.GroupB:

                        break;
                }
            }




        }



    }
}
