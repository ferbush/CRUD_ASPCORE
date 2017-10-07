using ASPCoreCRUD.Models;
using ASPCoreCRUD.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreCRUD.Controllers
{
    public class perfilController :Controller
    {
        private readonly perfilRepository pRepository;
        public perfilController(IConfiguration configuration)
        {
            pRepository = new perfilRepository(configuration);
        }

        public IActionResult Index()
        {

            return View(pRepository.FindAll());
        }

        public IActionResult Delete(string id)
        {

            if (id == null)
            {
                return NotFound();
            }
            pRepository.Delete(id);
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(perfilModel cust)
        {
            if (ModelState.IsValid)
            {
                pRepository.Insert(cust);
                return RedirectToAction("Index");
            }
            return View(cust);

        }
        public IActionResult Edit(string id)
        {
            if (id =="")
            {
                return NotFound();
            }
            perfilModel obj = pRepository.FindByID(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }

        [HttpPost]
        public IActionResult Edit(perfilModel obj)
        {

            if (ModelState.IsValid)
            {
                pRepository.Update(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }


    }
}
