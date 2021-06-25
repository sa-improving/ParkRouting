using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkRouting.Models;
using ParkRouting.ViewModel;

namespace ParkRouting.Controllers
{
    public class ParkController : Controller
    {
        private readonly ApiConnector _apiConnector;
        public ParkController(ApiConnector apiConnector)
        {
            _apiConnector = apiConnector;
        }
        
        public IActionResult PopulateParks(string search)
        {
            var vm = new ParksViewModel();
            if (String.IsNullOrWhiteSpace(search))
            {
                vm.ParksListed = _apiConnector.GetAllParks();
            }
            else
            {
                vm.ParksListed = _apiConnector.GetParks(search);
            }   
            return View("ParkPage", vm);
        }

        public IActionResult JavaScriptParks()
        {
            return View("Javascriptparks");
        }

        public IActionResult JavascriptJson(string search)
        {
            if(String.IsNullOrWhiteSpace(search))
            {
                return Json(_apiConnector.GetAllParks());
            }
            else
            {
                return Json(_apiConnector.GetParks(search));
            }
        }
    }
}
