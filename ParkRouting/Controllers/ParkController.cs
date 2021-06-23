using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ParkRouting.Controllers
{
    public class ParkController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
