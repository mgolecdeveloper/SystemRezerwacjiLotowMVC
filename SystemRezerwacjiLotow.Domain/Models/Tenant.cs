using Microsoft.AspNetCore.Identity;
using SystemRezerwacjiLotow.Domain.Models.Enums;

namespace SystemRezerwacjiLotow.Domain.Models
{
    public class Tenant : IdentityUser<string>
    {
        public override string Id { get; set; }
        public string? Imie { get; private set; }
        public string? Nazwisko { get; private set; }
        public string? DataUrodzenia { get; private set; }
        public TenantGroupKind TenantKind { get; private set; }



        public List<Flight>? Flights { get; private set; }
        public List<FlightBuy>? FlightBuys { get; private set; }


         
        public Tenant(string email, string imie, string nazwisko, string dataUrodzenia, TenantGroupKind tenantKind)
        {
            Id = Guid.NewGuid().ToString();
            Email = email;
            UserName = email;
            NormalizedUserName = email.ToUpper();
            NormalizedEmail = email.ToUpper();
            SecurityStamp = Guid.NewGuid().ToString();
            ConcurrencyStamp = Guid.NewGuid().ToString();
            EmailConfirmed = true;

            Imie = imie;
            Nazwisko = nazwisko;
            DataUrodzenia = dataUrodzenia;
            TenantKind = tenantKind;
        }

        public void Update(string imie, string nazwisko, string dataUrodzenia, TenantGroupKind tenantKind)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            DataUrodzenia = dataUrodzenia;
            TenantKind = tenantKind;
        }



    }
}
