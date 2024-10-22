using SystemRezerwacjiLotow.Domain.Models;
using SystemRezerwacjiLotow.Domain.DTOs;
using SystemRezerwacjiLotow.Domain.DTOs.DniWylotow;

namespace SystemRezerwacjiLotow.Infrastruktura.Abstractions
{
    public interface IDniWylotowRepository
    {
        Task<List<DzienWylotu>> GetAll();
        Task<DzienWylotu> Get(string flightBuyId);
        Task<DzienWylotuDto> Create(DzienWylotuDto model);
        Task<DzienWylotuDto> Update(DzienWylotuDto model);
        Task<DeleteResult> Delete(string flightBuyId);
    }
}
