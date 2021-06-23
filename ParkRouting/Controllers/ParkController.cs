using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkRouting.Models;

namespace ParkRouting.Controllers
{
    public class ParkController : Controller
    {
        private readonly ApiConnector _apiConnector;
        public ParkController(ApiConnector apiConnector)
        {
            _apiConnector = apiConnector;
        }
        public IActionResult PopulatePark(string query)
        {
            var park = _apiConnector.GetPark(query);
            ViewBag.Park = park;
            return View("ParkPage", ViewBag);
        }
    }
}
