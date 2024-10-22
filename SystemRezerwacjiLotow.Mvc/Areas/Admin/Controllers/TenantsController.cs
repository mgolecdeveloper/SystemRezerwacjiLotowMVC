using Microsoft.AspNetCore.Mvc;
using SystemRezerwacjiLotow.Application.Abstractions;
using SystemRezerwacjiLotow.Domain;
using SystemRezerwacjiLotow.Domain.Models;
using SystemRezerwacjiLotow.Domain.DTOs.Tenants;
using System.Reflection.Metadata.Ecma335;

namespace SystemRezerwacjiLotow.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Administrator, Manager")]
    public class TenantsController : Controller
    {
        private readonly ITenantsService _tenantsService;

        public TenantsController(ITenantsService tenantsService)
        {
            _tenantsService = tenantsService;
        }


        [HttpGet]
        public async Task<IActionResult> Index(TenantsDto model)
        {
            var tenants = await _tenantsService.GetAll();

            return View(new TenantsDto()
            {
                Tenants = tenants,
                Paginator = Paginator<Tenant>.CreateAsync(tenants, model.PageIndex, model.PageSize), 
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string s, TenantsDto model)
        {
            var tenants = await _tenantsService.GetAll();

            // Wyszukiwanie
            if (!string.IsNullOrEmpty(model.q))
                tenants = tenants.Where(w => w.Nazwisko.Contains(model.q, StringComparison.OrdinalIgnoreCase)).ToList();


            // Sortowanie 
            switch (model.SortowanieOption)
            {
                case "Nazwa A-Z":
                    tenants = tenants.OrderBy(o => o.Nazwisko).ToList();
                    break;

                case "Nazwa Z-A":
                    tenants = tenants.OrderByDescending(o => o.Nazwisko).ToList();
                    break;
            }

            model.Tenants = tenants;
            model.Paginator = Paginator<Tenant>.CreateAsync(tenants, model.PageIndex, model.PageSize);
            return View(model);
        }




        [HttpGet]
        public async Task<IActionResult> Create()
        { 
            return View(new TenantDto () { Id = Guid.NewGuid().ToString() }); // tu trzeba Id przekazać, bo Id musi być zdefiniowane, tego wymaga model Identity
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TenantDto model)
        {
            
            if (ModelState.IsValid)
            {
                var result = await _tenantsService.Create(model);
                if (result.Success)
                    return RedirectToAction("Index", "Tenants");
            }
             

            return View (model);

        }
         
    }
}
