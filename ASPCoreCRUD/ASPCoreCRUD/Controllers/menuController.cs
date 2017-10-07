using ASPCoreCRUD.iRepository;
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
    public class menuController : Controller
    {
        private readonly MenuRepository mRepository;
      
        public menuController(IConfiguration configuration)
        {
            mRepository = new MenuRepository(configuration);
        }

        public IActionResult Index()
        {

            return View(mRepository.FindAll());
        }

        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            mRepository.Delete(id.Value);
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(menuModel cust)
        {
            if (ModelState.IsValid)
            {
                mRepository.Insert(cust);
                return RedirectToAction("Index");
            }
            return View(cust);

        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            menuModel obj = mRepository.FindByID(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }

        [HttpPost]
        public IActionResult Edit(menuModel obj)
        {

            if (ModelState.IsValid)
            {
                mRepository.Update(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }


    }
}

