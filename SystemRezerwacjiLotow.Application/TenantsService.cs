using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using SystemRezerwacjiLotow.Application.Abstractions;
using SystemRezerwacjiLotow.Domain.Models;
using SystemRezerwacjiLotow.Domain.DTOs;
using SystemRezerwacjiLotow.Domain.DTOs.Tenants;
using SystemRezerwacjiLotow.Infrastruktura;
using SystemRezerwacjiLotow.Domain.Models.Enums;
using SystemRezerwacjiLotow.Domain.DTOs.DniWylotow;

namespace SystemRezerwacjiLotow.Application
{
    /// <summary>
    /// Klasa tworzy użytkowników systemu
    /// </summary>
    public class TenantsService : ITenantsService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Tenant> _userManager;

        public TenantsService(ApplicationDbContext context, UserManager<Tenant> userManager)
        {
            _context = context;
            _userManager = userManager;
        }



        public async Task<List<Tenant>> GetAll()
        {
            var users = await _context.Tenants
                .Include(i => i.Flights)
                .Include(i => i.FlightBuys)
                .ToListAsync();
            return users;
        }



        public async Task<Tenant> GetTenantById(string tenantId)
        {
            var tenant = await _context.Tenants
                .Include(i => i.Flights)
                .Include(i => i.FlightBuys)
                .FirstOrDefaultAsync(f => f.Id == tenantId);
            return tenant;
        }


        public async Task<Tenant> GetTenantByEmail(string email)
        {
            var tenant = await _context.Tenants
                .Include(i => i.Flights)
                .Include(i => i.FlightBuys)
                .FirstOrDefaultAsync(f => f.Email == email);
            return tenant;
        }



        public async Task<TenantDto> Create(TenantDto model)
        {
            if (model != null)
            {
                try
                {
                    // utworzenie nowego tenanta

                    // sprzewdzenie na podstawie maila czy użytkownik już istnieje
                    var findTenant = await _context.Tenants.FirstOrDefaultAsync(f => f.Email == model.Email);
                    // jeżeli użytkownik nie istnieje to tworzone jest nowe konto
                    if (findTenant == null)
                    {

                        // stworzenie użytkownika 

                        var tenant = new Tenant(
                            email: model.Email,
                            imie: model.Imie,
                            nazwisko: model.Nazwisko,
                            dataUrodzenia: model.DataUrodzenia,
                            tenantKind: model.TenantKind
                            );
                        var result = await _userManager.CreateAsync(tenant, "SDG%$@5423sdgagSDert");


                        // jeżeli konto zostało utworzone
                        if (result.Succeeded)
                        {  

                            model.Success = true;
                            model.Message = "Zarejestrowano nowego tenanta. Sprawdź pocztę aby dokończyć rejestrację";
                        }
                    }
                    else
                    {
                        model.Message = "Wyszukiwanie użytkownika nie powiodło się";
                        model.Success = false;
                    }
                }
                catch (Exception ex)
                {
                    model = new TenantDto() { Success = false, Message = ex.Message };
                }
            }
            else
            {
                model = new TenantDto() { Success = false, Message = "Model was null" };
            }
            return model;
        }


        public async Task<TenantDto> Update(TenantDto model)
        {
            if (model != null)
            {
                try
                {
                    var tenant = await _context.Tenants
                        .Include (i=> i.Flights)
                        .Include (i=> i.FlightBuys)
                        .FirstOrDefaultAsync(f => f.Id == model.Id);


                    if (tenant != null)
                    {

                        // aktualizacja danych osobowych klienta

                        tenant.Update (
                            imie: model.Imie,
                            nazwisko: model.Nazwisko,
                            dataUrodzenia: model.DataUrodzenia,
                            tenantKind: model.TenantKind
                            );

                        _context.Entry(tenant).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                         
                         

                        model.Message = "Dane zostały zaktualizowane poprawnie";
                        model.Success = true;

                    }
                    else
                    {
                        model.Message = "Model is null";
                    }

                }
                catch (Exception ex)
                {
                    model.Message = $"Exception: {ex.Message}";
                }
            }
            else
            {
                model = new TenantDto() { Success = false, Message = "Model was null" };
            }
            return model;

        }



        public async Task<DeleteResult> Delete(string tenantId)
        {
            DeleteResult deleteResult = new DeleteResult() { Success = false, Message = "Usuwanie rekordu nie powiodło się" };
            try
            {
                var tenant = await _context.Tenants.FirstOrDefaultAsync(f => f.Id == tenantId);
                if (tenant != null)
                {
                    _context.Tenants.Remove(tenant);
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
