using Microsoft.EntityFrameworkCore;
using SystemRezerwacjiLotow.Domain.Models;
using SystemRezerwacjiLotow.Domain.DTOs;
using SystemRezerwacjiLotow.Domain.DTOs.DniWylotow;
using SystemRezerwacjiLotow.Infrastruktura.Abstractions;

namespace SystemRezerwacjiLotow.Infrastruktura
{
    public class DniWylotowRepository : IDniWylotowRepository
    {
        private readonly ApplicationDbContext _context;
        public DniWylotowRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DzienWylotu>> GetAll()
        {
            return await _context.DniWylotow
                .Include(i => i.Flight)
                .ToListAsync();
        }

        public async Task<DzienWylotu> Get(string dzienWylotuId)
        {
            return await _context.DniWylotow
                .Include(i => i.Flight)
                .FirstOrDefaultAsync(f => f.DzieWylotuId == dzienWylotuId);
        }

        public async Task<DzienWylotuDto> Create(DzienWylotuDto model)
        {
            if (model != null)
            {
                try
                {
                    DzienWylotu dzienWylotu = new DzienWylotu(
                        dzien: model.Dzien,
                        flightId: model.FlightId
                        );
                    _context.DniWylotow.Add(dzienWylotu);
                    await _context.SaveChangesAsync();


                    model.Success = true;

                }
                catch (Exception ex)
                {
                    model = new DzienWylotuDto() { Success = false, Message = ex.Message };
                }
            }
            else
            {
                model = new DzienWylotuDto() { Success = false, Message = "Model was null" };
            }
            return model;
        }



        public async Task<DzienWylotuDto> Update(DzienWylotuDto model)
        {
            if (model != null)
            {
                try
                {
                    var dzienWylotu = await _context.DniWylotow.FirstOrDefaultAsync(f => f.DzieWylotuId == model.DzieWylotuId);
                    if (dzienWylotu != null)
                    {
                        dzienWylotu.Update (
                            dzien: model.Dzien,
                            flightId: model.FlightId
                            );

                        _context.Entry(dzienWylotu).State = EntityState.Modified;
                        await _context.SaveChangesAsync();


                        model.Success = true;
                    }
                    else
                    {
                        model.Success = false;
                        model.Message = "DzienWylotu was null";
                    }
                }
                catch (Exception ex)
                {
                    model = new DzienWylotuDto() { Success = false, Message = ex.Message };
                }
            }
            else
            {
                model = new DzienWylotuDto() { Success = false, Message = "Model was null" };
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
