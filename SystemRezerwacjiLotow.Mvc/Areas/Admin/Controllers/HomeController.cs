﻿using Microsoft.AspNetCore.Mvc;

namespace SystemRezerwacjiLotow.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Administrator, Manager")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
