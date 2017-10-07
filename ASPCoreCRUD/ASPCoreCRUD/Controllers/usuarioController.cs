using ASPCoreCRUD.iRepository;
using ASPCoreCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreCRUD.Controllers
{
    public class usuarioController : Controller
    {
        private readonly usuarioRepository uRepository;
        public usuarioController(IConfiguration configuration)
        {
            uRepository = new usuarioRepository(configuration);
        }

        public IActionResult Index()
        {
            var a = uRepository.FindAll();
            return View(a);
        }

        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            uRepository.Delete(id.Value);
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(usuarioModel cust)
        {
            if (ModelState.IsValid)
            {
                uRepository.Insert(cust);
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
            usuarioModel obj = uRepository.FindByID(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }

        [HttpPost]
        public IActionResult Edit(usuarioModel obj)
        {

            if (ModelState.IsValid)
            {
                uRepository.Update(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }


    }
}
