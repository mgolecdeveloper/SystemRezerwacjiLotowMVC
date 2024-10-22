using Microsoft.EntityFrameworkCore;
using SystemRezerwacjiLotow.Domain.Models;
using SystemRezerwacjiLotow.Domain.DTOs;
using SystemRezerwacjiLotow.Domain.DTOs.KryteriaZnizek;
using SystemRezerwacjiLotow.Infrastruktura.Abstractions;

namespace SystemRezerwacjiLotow.Infrastruktura
{
    public class KryteriaZnizekRepository : IKryteriaZnizekRepository
    {
        private readonly ApplicationDbContext _context;
        public KryteriaZnizekRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<KryteriaZnizki>> GetAll()
        {
            return await _context.KryteriaZnizek
                .Include(i => i.FlightBuy)
                .Include(i => i.FlightBuysKryteriaZnizkis)
                .ToListAsync();
        }

        public async Task<KryteriaZnizki> Get(string kryteriaZnizkiId)
        {
            return await _context.KryteriaZnizek
                .Include(i => i.FlightBuy)
                .Include(i => i.FlightBuysKryteriaZnizkis)
                .FirstOrDefaultAsync(f => f.KryteriaZnizkiId == kryteriaZnizkiId);
        }

        public async Task<KryteriaZnizkiDto> Create(KryteriaZnizkiDto model)
        {
            if (model != null)
            {
                try
                {
                    KryteriaZnizki kryteriaZnizki = new KryteriaZnizki(
                        name: model.Name
                        );

                    _context.KryteriaZnizek.Add(kryteriaZnizki);
                    await _context.SaveChangesAsync();


                    model.Success = true;
                }
                catch (Exception ex)
                {
                    model = new KryteriaZnizkiDto() { Success = false, Message = ex.Message };
                }
            }
            else
            {
                model = new KryteriaZnizkiDto() { Success = false, Message = "Model was null" };
            }
            return model;
        }



        public async Task<KryteriaZnizkiDto> Update(KryteriaZnizkiDto model)
        {
            if (model != null)
            {
                try
                {
                    var kryteriaZnizki = await _context.KryteriaZnizek.FirstOrDefaultAsync(f => f.KryteriaZnizkiId == model.KryteriaZnizkiId);
                    if (kryteriaZnizki != null)
                    {

                        kryteriaZnizki.Update (
                            name: model.Name
                            );

                        _context.Entry(kryteriaZnizki).State = EntityState.Modified;
                        await _context.SaveChangesAsync();


                        model.Success = true;
                    }
                    else
                    {
                        model.Success = false;
                        model.Message = "KryteriaZnizki was null";
                    }
                }
                catch (Exception ex)
                {
                    model = new KryteriaZnizkiDto() { Success = false, Message = ex.Message };
                }
            }
            else
            {
                model = new KryteriaZnizkiDto() { Success = false, Message = "Model was null" };
            }
            return model;
        }





        public async Task<DeleteResult> Delete(string dzienWylotuId)
        {
            DeleteResult deleteResult = new DeleteResult() { Success = false, Message = "Usuwanie rekordu nie powiodło się" };
            try
            {
                var dzienWylotu = await _context.DniWylotow.FirstOrDefaultAsync(f => f.DzieWylotuId == dzienWylotuId);
                if (dzienWylotu != null)
                {
                    _context.DniWylotow.Remove(dzienWylotu);
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

    }
}
