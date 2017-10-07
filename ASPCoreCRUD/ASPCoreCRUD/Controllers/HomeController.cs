using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPCoreCRUD.Models;
using ASPCoreCRUD.Repository;
using Microsoft.Extensions.Configuration;

namespace ASPCoreCRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly homeRepository hRepository;

        public HomeController(IConfiguration configuration)
        {
          
        }
        public IActionResult Index()
        {
           
            return View();
        }

      
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
