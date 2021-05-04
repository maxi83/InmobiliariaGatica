
using InmobiliariaGatica.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        [Authorize]
        public ActionResult Index()
        {
            ViewData["Error"] = TempData["Error"];
            var listaInmuebles = iRepositorio.ObtenerTodos();
            return View(listaInmuebles);
        }

        // GET: InquilinoController/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            Inmueble inmueble = iRepositorio.ObtenerInmueble(id);
            return View(inmueble);
        }

        // GET: InquilinoController/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewData["Propietarios"] = rRepositorio.ObtenerTodos();

            return View();
        }

        // POST: InquilinoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(Inmueble inmueble)
        {
            iRepositorio.Alta(inmueble);
            return RedirectToAction(nameof(Index));
        }

        // GET: InquilinoController/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            Inmueble inmueble = iRepositorio.ObtenerInmueble(id);
            ViewData["Propietarios"] = rRepositorio.ObtenerTodos();
            return View(inmueble);
        }

        // POST: InquilinoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(int id, Inmueble inmueble)
        {
            iRepositorio.Modificar(inmueble);
            return RedirectToAction(nameof(Index));

        }

        // GET: InquilinoController/Delete/5
        [Authorize(Policy = "Administrador")]
        public ActionResult Delete(int id)
        {
            Inmueble inmueble = iRepositorio.ObtenerInmueble(id);
            return View(inmueble);
        }

        // POST: InquilinoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Administrador")]
        public ActionResult Delete(int id, Inmueble inmueble)
        {
            {
                try
                {
                    iRepositorio.Baja(id);

                    return RedirectToAction(nameof(Index));
                }
                catch (SqlException ex)
                {
                    TempData["Error"] = ex.Number == 547 ? "No se puede borrar el tipo Persona porque esta utilizado" : "Ocurrio un error.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Ocurrio un error." + ex.ToString();
                    return RedirectToAction(nameof(Index));
                }
            }
        }
    }
}