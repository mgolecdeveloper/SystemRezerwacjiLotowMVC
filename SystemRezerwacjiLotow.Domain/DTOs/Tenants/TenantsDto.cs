

using SystemRezerwacjiLotow.Domain.Models;

namespace SystemRezerwacjiLotow.Domain.DTOs.Tenants
{
    public class TenantsDto : BaseSearchModel<Tenant>
    {
        public List<Tenant> Tenants { get; set; }
    }
}
