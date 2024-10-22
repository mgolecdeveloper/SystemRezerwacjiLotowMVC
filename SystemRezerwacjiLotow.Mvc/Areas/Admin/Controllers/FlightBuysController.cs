using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SystemRezerwacjiLotow.Application.Abstractions;
using SystemRezerwacjiLotow.Domain;
using SystemRezerwacjiLotow.Domain.Models;
using SystemRezerwacjiLotow.Domain.DTOs.FlightBuys;
using SystemRezerwacjiLotow.Infrastruktura.Abstractions;

namespace SystemRezerwacjiLotow.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Administrator, Manager")]
    public class FlightBuysController : Controller
    {
        private readonly IFlightBuysRepository _flightBuysRepository;
        private readonly IFlightsRepository _flightsRepository;
        private readonly ITenantsService _tenantsService;

        public FlightBuysController(IFlightBuysRepository flightBuysRepository, IFlightsRepository flightsRepository, ITenantsService tenantsService)
        {
            _flightBuysRepository = flightBuysRepository;
            _flightsRepository = flightsRepository;
            _tenantsService = tenantsService;
        }


        [HttpGet]
        public async Task<IActionResult> Index(FlightBuysDto model)
        {
            var flightBuys = await _flightBuysRepository.GetAll();


            return View(new FlightBuysDto()
            {
                FlightBuys = flightBuys,
                Paginator = Paginator<FlightBuy>.CreateAsync(flightBuys, model.PageIndex, model.PageSize), 
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string s, FlightBuysDto model)
        {
            var flightBuys = await _flightBuysRepository.GetAll();
             
            model.FlightBuys = flightBuys;
            model.Paginator = Paginator<FlightBuy>.CreateAsync(flightBuys, model.PageIndex, model.PageSize);
            return View(model);
        }




        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["FlightsList"] = new SelectList(await _flightsRepository.GetAll(), "FlightId", "FlightId");
            ViewData["TenantsList"] = new SelectList(await _tenantsService.GetAll(), "Id", "Email");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FlightBuyDto model)
        {
            if (ModelState.IsValid)
            {
                await _flightBuysRepository.Create(model);
                return RedirectToAction("Index", "FlightBuys");
            }
            ViewData["FlightsList"] = new SelectList(await _flightsRepository.GetAll(), "FlightId", "FlightId", model.FlightId);
            ViewData["TenantsList"] = new SelectList(await _tenantsService.GetAll(), "Id", "Email", model.TenantEmail);
            return View(model);
        }
         
    }
}
