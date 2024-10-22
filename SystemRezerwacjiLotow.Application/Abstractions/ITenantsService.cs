using SystemRezerwacjiLotow.Domain.Models;
using SystemRezerwacjiLotow.Domain.DTOs;
using SystemRezerwacjiLotow.Domain.DTOs.Tenants;

namespace SystemRezerwacjiLotow.Application.Abstractions
{
    public interface ITenantsService
    {
        Task<List<Tenant>> GetAll();
        Task<Tenant> GetTenantById(string tenantId);
        Task<Tenant> GetTenantByEmail(string email);
        Task<TenantDto> Create(TenantDto model);
        Task<TenantDto> Update(TenantDto model);
        Task<DeleteResult> Delete(string tenantId);

    }
}
