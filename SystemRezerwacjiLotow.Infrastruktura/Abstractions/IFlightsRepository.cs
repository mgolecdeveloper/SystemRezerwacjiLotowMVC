using SystemRezerwacjiLotow.Domain.Models;
using SystemRezerwacjiLotow.Domain.DTOs;
using SystemRezerwacjiLotow.Domain.DTOs.Flights;

namespace SystemRezerwacjiLotow.Infrastruktura.Abstractions
{
    public interface IFlightsRepository
    {
        Task<List<Flight>> GetAll();
        Task<Flight> Get(string flightId);
        Task<FlightDto> Create(FlightDto model);
        Task<FlightDto> Update(FlightDto model);
        Task<DeleteResult> Delete(string flightId);
    }
}
