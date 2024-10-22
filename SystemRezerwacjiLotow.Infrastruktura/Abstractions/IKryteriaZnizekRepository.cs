using SystemRezerwacjiLotow.Domain.Models;
using SystemRezerwacjiLotow.Domain.DTOs;
using SystemRezerwacjiLotow.Domain.DTOs.KryteriaZnizek;

namespace SystemRezerwacjiLotow.Infrastruktura.Abstractions
{
    public interface IKryteriaZnizekRepository
    {
        Task<List<KryteriaZnizki>> GetAll();
        Task<KryteriaZnizki> Get(string flightBuyId);
        Task<KryteriaZnizkiDto> Create(KryteriaZnizkiDto model);
        Task<KryteriaZnizkiDto> Update(KryteriaZnizkiDto model);
        Task<DeleteResult> Delete(string flightBuyId);
    }
}
