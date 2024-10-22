

using SystemRezerwacjiLotow.Domain.Models;
using SystemRezerwacjiLotow.Domain.Models.Enums;

namespace SystemRezerwacjiLotow.Domain.DTOs.Tenants
{
    public class TenantDto : BaseModel
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? Imie { get; set; }
        public string? Nazwisko { get; set; }
        public string? DataUrodzenia { get; set; }
        public TenantGroupKind TenantKind { get; set; } 

    }
}
