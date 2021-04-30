
using InmobiliariaGatica.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaGatica.Controllers
{
    public class InmuebleController : Controller
    {
        private RepositorioInmueble iRepositorio;
        private RepositorioPropietario rRepositorio;

        public InmuebleController(IConfiguration configuration)
        {

            iRepositorio = new RepositorioInmueble(configuration);
            rRepositorio = new RepositorioPropietario(configuration);
        }

        // GET: InquilinoController
        public ActionResult Index()
        {
            var listaInmuebles = iRepositorio.ObtenerTodos();
            return View(listaInmuebles);
        }

        // GET: InquilinoController/Details/5
        public ActionResult Details(int id)
        {
            Inmueble inmueble = iRepositorio.ObtenerInmueble(id);
            return View(inmueble);
        }

        // GET: InquilinoController/Create
        public ActionResult Create()
        {
            ViewData["Propietarios"] = rRepositorio.ObtenerTodos();

            return View();
        }

        // POST: InquilinoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inmueble inmueble)
        {
            iRepositorio.Alta(inmueble);
            return RedirectToAction(nameof(Index));
        }

        // GET: InquilinoController/Edit/5
        public ActionResult Edit(int id)
        {
            Inmueble inmueble = iRepositorio.ObtenerInmueble(id);
            ViewData["Propietarios"] = rRepositorio.ObtenerTodos();
            return View(inmueble);
        }

        // POST: InquilinoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Inmueble inmueble)
        {
            iRepositorio.Modificar(inmueble);
            return RedirectToAction(nameof(Index));

        }

        // GET: InquilinoController/Delete/5
        public ActionResult Delete(int id)
        {
            Inmueble inmueble = iRepositorio.ObtenerInmueble(id);
            return View(inmueble);
        }

        // POST: InquilinoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Inmueble inmueble)
        {
            {
                try
                {
                    iRepositorio.Baja(id);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View(inmueble);
                }
            }
        }
    }
}