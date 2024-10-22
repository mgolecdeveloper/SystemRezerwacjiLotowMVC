using Microsoft.AspNetCore.Mvc;
using SystemRezerwacjiLotow.Domain;
using SystemRezerwacjiLotow.Domain.Models;
using SystemRezerwacjiLotow.Domain.DTOs.KryteriaZnizek;
using SystemRezerwacjiLotow.Infrastruktura.Abstractions;

namespace SystemRezerwacjiLotow.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Administrator, Manager")]
    public class KryteriaZnizekController : Controller
    {
        private readonly IKryteriaZnizekRepository _kryteriaZnizekRepository;

        public KryteriaZnizekController(IKryteriaZnizekRepository kryteriaZnizekRepository)
        {
            _kryteriaZnizekRepository = kryteriaZnizekRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Index(KryteriaZnizekDto model)
        {
            var kryteriaZnizek = await _kryteriaZnizekRepository.GetAll();


            return View(new KryteriaZnizekDto()
            {
                KryteriaZnizki = kryteriaZnizek,
                Paginator = Paginator<KryteriaZnizki>.CreateAsync(kryteriaZnizek, model.PageIndex, model.PageSize), 
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string s, KryteriaZnizekDto model)
        {
            var kryteriaZnizek = await _kryteriaZnizekRepository.GetAll();
             

            model.KryteriaZnizki = kryteriaZnizek;
            model.Paginator = Paginator<KryteriaZnizki>.CreateAsync(kryteriaZnizek, model.PageIndex, model.PageSize);
            return View(model);
        }




        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KryteriaZnizkiDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _kryteriaZnizekRepository.Create(model);
                if (result.Success)
                    return RedirectToAction("Index", "KryteriaZnizek");
            }
            return View(model);
        }
    }
}
