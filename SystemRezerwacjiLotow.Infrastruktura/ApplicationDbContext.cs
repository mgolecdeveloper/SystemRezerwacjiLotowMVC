using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SystemRezerwacjiLotow.Domain.Models;
using SystemRezerwacjiLotow.Domain.Models.Enums;

namespace SystemRezerwacjiLotow.Infrastruktura
{
    public class ApplicationDbContext : IdentityDbContext<Tenant, IdentityRole<string>, string>
    {
        private Random _rand = new Random();

        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
               : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SystemRezerwacjiLotow;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            CreateInitialData(builder);
            base.OnModelCreating(builder);
        }


        public DbSet<Flight> Flights { get; set; }
        public DbSet<DzienWylotu> DniWylotow { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<FlightBuy> FlightBuys { get; set; }
        public DbSet<KryteriaZnizki> KryteriaZnizek { get; set; }
        public DbSet<FlightBuyKryteriaZnizki> FlightBuysKryteriaZnizkis { get; set; }



        /// <summary>
        /// Wygenerowanie przykładowych danych dla każdego modelu danych
        /// </summary>
        private void CreateInitialData(ModelBuilder builder)
        {
            // Zbudowanie relacji


            // relacje typu wiele-do-wielu


            builder.Entity<FlightBuyKryteriaZnizki>()
                .HasKey(h => new { h.FlightBuyId, h.KryteriaZnizkiId });

            builder.Entity<FlightBuyKryteriaZnizki>()
                .HasOne(h => h.FlightBuy)
                .WithMany(w => w.FlightBuysKryteriaZnizkis)
                .HasForeignKey(h => h.FlightBuyId);

            builder.Entity<FlightBuyKryteriaZnizki>()
                .HasOne(h => h.KryteriaZnizki)
                .WithMany(w => w.FlightBuysKryteriaZnizkis)
                .HasForeignKey(h => h.KryteriaZnizkiId);



            // relacje typu jeden-do-wielu

            builder.Entity<Tenant>()
                .HasMany(h => h.Flights).WithOne(w => w.Tenant).HasForeignKey(f => f.TenantId); /*.OnDelete(DeleteBehavior.ClientSetNull);*/

            builder.Entity<Tenant>()
                .HasMany(h => h.FlightBuys).WithOne(w => w.Tenant).HasForeignKey(f => f.TenantId);



            builder.Entity<Flight>()
                .HasMany(h => h.FlightBuys).WithOne(w => w.Flight).HasForeignKey(f => f.FlightId);

            builder.Entity<Flight>()
                .HasMany(h => h.DniWylotu).WithOne(w => w.Flight).HasForeignKey(f => f.FlightId);







            // Wygenerowanie 3 standardowych użytkowników systemu

            PasswordHasher<Tenant> passwordHasher = new PasswordHasher<Tenant>();


            // administrator systemu


            // losowa data urodzanie, z dniem oraz miesiącem przypisanym na dzisaj tak aby "wymusić" zniżkę związaną z urodzinami na cele testów
            string dataUrodzenia = new DateTime(_rand.Next(1980,2000), DateTime.Now.Month, DateTime.Now.Day, 12, 12, 12).ToString();
            var administratorTenant = new Tenant(
                email: "admin@admin.pl",
                imie: "Jan",
                nazwisko: "Kowalski",
                dataUrodzenia: dataUrodzenia,
                tenantKind: TenantGroupKind.GroupA
                );
            administratorTenant.PasswordHash = passwordHasher.HashPassword(administratorTenant, "SDG%$@5423sdgagSDert");
            // zapisanie użytkownika w bazie
            builder.Entity<Tenant>().HasData(administratorTenant);


            // manager
            var managerUser = new Tenant(
                email: "manager@manager.pl",
                imie: "Janina",
                nazwisko: "Kowalska",
                dataUrodzenia: dataUrodzenia,
                tenantKind: TenantGroupKind.GroupA
                );
            managerUser.PasswordHash = passwordHasher.HashPassword(managerUser, "SDG%$@5423sdgagSDert");
            // zapisanie użytkownika w bazie
            builder.Entity<Tenant>().HasData(managerUser);



            // listy przechowujące identyfikatory id poszczególnym modeli wykorzsytywane potem 
            // w kolejnych pętlach do zbudowania relacji w bazie
            List<string> tenantsId = new List<string>();
            List<string> flightsId = new List<string>();
            List<string> kryteriaZnizekId = new List<string>();


            // Dodanie do bazy 10 przykładowych Tenants

            for (var i = 0; i < 10; i++)
            {
                string email = $"ImieNazwisko_{i}@gmail.com";
                Tenant tenant = new Tenant(
                email: $"ImieNazwisko_{i}@gmail.com",
                imie: $"Imie_{i}",
                nazwisko: $"Nazwisko_{i}",
                dataUrodzenia: dataUrodzenia,
                tenantKind: TenantGroupKind.GroupA
                );
                tenant.PasswordHash = passwordHasher.HashPassword(tenant, "SDG%$@5423sdgagSDert");
                // zapisanie użytkownika w bazie
                builder.Entity<Tenant>().HasData(tenant);
                tenantsId.Add(tenant.Id);
            }




            // Dodanie do bazy przykładowych Flights
            List<string> trasyOd = new List<string>() { "Horwacja", "Hiszpania", "Chiny", "Rosja" };
            List<string> trasyDo = new List<string>() { "Londyn", "USA", "Afryka" };

            for (var i = 0; i < 25; i++)
            {
                string flightId = GenerateFlightId();
                string trasaOd = trasyOd[_rand.Next(0, trasyOd.Count)]; // trasa od wybierana losowo
                string trasaDo = trasyDo[_rand.Next(0, trasyDo.Count)]; // trasa do wybierana losowo
                string tenantId = tenantsId[_rand.Next(0, tenantsId.Count)]; // id tenanta wybierany losowo
                string godzinaWylotu = $"{_rand.Next(1, 23)}:{_rand.Next(1, 60)}";

                Flight flight = new Flight(
                    trasaOd: trasaOd,
                    trasaDo: trasaDo,
                    price: _rand.Next(100,200),
                    godzinaWylotu: godzinaWylotu,
                    tenantId: tenantId
                    );
                builder.Entity<Flight>().HasData(flight);
                flightsId.Add(flight.FlightId);



                // przypisanie dwoch losowo wybranych dni lotu
                List<string> dniWylotu = new List<string>() { "Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek", "Sobota", "Niedziela" };

                // dodaj do bazy dwa wpisy
                for (var j = 0; j < 2; j++)
                {
                    int losowyIndex = _rand.Next(0, 6);
                    DzienWylotu dzienWylotu = new DzienWylotu(
                        dzien: dniWylotu[losowyIndex], // przypisanie losowego dnia wylotu
                        flightId: flight.FlightId
                        );
                    builder.Entity<DzienWylotu>().HasData(dzienWylotu);
                }

            }



            // Dodanie do bazy przykładowych nazw kryteriów zniżek

            List<string> kryteriaZnizekNames = new List<string>() { "Urodziny kupującego", "Lot do Afryki" };
            for (var i = 0; i < kryteriaZnizekNames.Count; i++)
            {
                KryteriaZnizki kryteriaZnizki = new KryteriaZnizki(
                    name: kryteriaZnizekNames[i]
                    );
                builder.Entity<KryteriaZnizki>().HasData(kryteriaZnizki);
                kryteriaZnizekId.Add(kryteriaZnizki.KryteriaZnizkiId);
            }




            // Dodanie do bazy przykładowych FlightBuys, kod przydziela losowych los oraz losowego tenanta,
            // tworząc w ten sposób kupiony lot

            for (var i = 0; i < 10; i++)
            {
                string flightId = flightsId[_rand.Next(0, flightsId.Count)]; // przypisanie losowego Flight
                string tenantId = tenantsId[_rand.Next(0, tenantsId.Count)]; // przypisanie losowego Tenant
                string kryteriaZnizkiId = kryteriaZnizekId[_rand.Next(0, kryteriaZnizekId.Count)];

                FlightBuy flightBuy = new FlightBuy(
                    iloscBiletow: 1,
                    priceSuma: 125,
                    flightId: flightId,
                    tenantId: tenantId
                    );
                builder.Entity<FlightBuy>().HasData(flightBuy);

                // co drugi rekor będzie posiadał określone kryterium, przypisywanie odbywa się w sposób losowy,
                // niektóre będą miały wartość NULL, znacza to, że nie będą miały przypisanych żadnych zniżek
                if (i % 2 == 0)
                {
                    FlightBuyKryteriaZnizki flightBuyKryteriaZnizki = new FlightBuyKryteriaZnizki(
                        flightBuyId: flightBuy.FlightBuyId,
                        kryteriaZnizkiId: kryteriaZnizkiId
                        );
                    builder.Entity<FlightBuyKryteriaZnizki>().HasData(flightBuyKryteriaZnizki);
                }
            }




        }





        /// <summary>
        /// Generuje losowy numer jako identyfikato Id rekordu np. "KLM 12345 BCA",
        /// gdzie jedynym zmieniającym się elementem jest środkowa część ciągu z cudzysłowia
        /// </summary>
        private string GenerateFlightId()
        {
            Random random = new Random();
            int randomNumber = random.Next(10000, 100000); // Generuje losową liczbę 5-cyfrową
            string code = $"KLM {randomNumber} BCA";
            return code;
        }

         

    }
}
