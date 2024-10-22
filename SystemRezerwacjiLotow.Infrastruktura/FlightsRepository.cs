using Microsoft.EntityFrameworkCore;
using SystemRezerwacjiLotow.Domain.Models;
using SystemRezerwacjiLotow.Domain.DTOs;
using SystemRezerwacjiLotow.Domain.DTOs.Flights;
using SystemRezerwacjiLotow.Infrastruktura.Abstractions;

namespace SystemRezerwacjiLotow.Infrastruktura
{
    public class FlightsRepository : IFlightsRepository
    {
        private readonly ApplicationDbContext _context;
        public FlightsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Flight>> GetAll()
        {
            return await _context.Flights
                .Include(i => i.DniWylotu)
                .Include(i => i.FlightBuys)
                .ToListAsync();
        }

        public async Task<Flight> Get(string flightId)
        {
            return await _context.Flights
                .Include(i => i.Tenant)
                .Include(i => i.DniWylotu)
                .Include(i => i.FlightBuys)
                .FirstOrDefaultAsync(f => f.FlightId == flightId);
        }

        public async Task<FlightDto> Create(FlightDto model)
        {
            if (model != null)
            {
                try
                {

                    Flight flight = new Flight(
                        trasaOd: model.TrasaOd,
                        trasaDo: model.TrasaOd,
                        price: model.Price,
                        godzinaWylotu: model.GodzinaWylotu,
                        tenantId: "" // tutaj realne Id osoby zalogowanej odpowiedzialnej za dodawanie lotów
                        );
                     
                    _context.Flights.Add(flight);
                    await _context.SaveChangesAsync();


                    // Przypisanie dni wylotow do lotu
                    string[] dniWylotu = model.DniWylotu.Split(',');
                    foreach (var dzienWylotu in dniWylotu)
                    {
                        DzienWylotu dw = new DzienWylotu(
                            dzien: dzienWylotu,
                            flightId: flight.FlightId
                            );

                        _context.DniWylotow.Add(dw);
                        await _context.SaveChangesAsync();
                    }


                    model.Success = true;
                }
                catch (Exception ex)
                {
                    model = new FlightDto() { Success = false, Message = ex.Message };
                }
            }
            else
            {
                model = new FlightDto() { Success = false, Message = "Model was null" };
            }
            return model;
        }



        public async Task<FlightDto> Update(FlightDto model)
        {
            if (model != null)
            {
                try
                {
                    var flight = await _context.Flights.FirstOrDefaultAsync(f => f.FlightId == model.FlightId);
                    if (flight != null)
                    {
                        flight.Update (
                            trasaOd: model.TrasaOd,
                            trasaDo: model.TrasaDo,
                            price: model.Price,
                            godzinaWylotu: model.GodzinaWylotu
                            ); 

                        _context.Entry(flight).State = EntityState.Modified;
                        await _context.SaveChangesAsync();


                        model.Success = true;
                    }
                    else
                    {
                        model.Success = false;
                        model.Message = "Flight was null";
                    }
                }
                catch (Exception ex)
                {
                    model = new FlightDto() { Success = false, Message = ex.Message };
                }
            }
            else
            {
                model = new FlightDto() { Success = false, Message = "Model was null" };
            }
            return model;
        }





        public async Task<DeleteResult> Delete(string flightId)
        {
            DeleteResult deleteResult = new DeleteResult() { Success = false, Message = "Usuwanie rekordu nie powiodło się" };
            try
            {
                var flight = await _context.Flights.FirstOrDefaultAsync(f => f.FlightId == flightId);
                if (flight != null)
                {
                    _context.Flights.Remove(flight);
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




        private string GenerateFlightId()
        {
            Random random = new Random();
            int randomNumber = random.Next(10000, 100000); // Generuje losową liczbę 5-cyfrową
            string code = $"KLM {randomNumber} BCA";
            return code;
        }


    }
}
