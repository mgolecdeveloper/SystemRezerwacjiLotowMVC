
using SystemRezerwacjiLotow.Domain.DTOs;
using SystemRezerwacjiLotow.Domain.DTOs.FlightBuys;
using SystemRezerwacjiLotow.Domain.Models;

namespace SystemRezerwacjiLotow.Infrastruktura.Abstractions
{
    public interface IFlightBuysRepository
    {
        Task<List<FlightBuy>> GetAll();
        Task<FlightBuy> Get(string flightBuyId);
        Task<FlightBuyDto> Create(FlightBuyDto model);
        Task<FlightBuyDto> Update(FlightBuyDto model);
        Task<DeleteResult> Delete(string flightBuyId);
    }
}
