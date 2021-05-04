using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using InmobiliariaGatica.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace InmobiliariaGatica.Controllers
{
    public class ContratoController : Controller
    {
        private RepositorioContrato rContrato;
        private RepositorioInquilino rInquilino;
        private RepositorioInmueble rInmueble;

        public ContratoController(IConfiguration configuration)
        {
            rContrato = new RepositorioContrato(configuration);
            rInmueble = new RepositorioInmueble(configuration);
            rInquilino = new RepositorioInquilino(configuration);
        }
        // GET: ContratoController
        [Authorize]
        public ActionResult Index()
        {
            ViewData["Error"] = TempData["Error"];
            var listaContratos = rContrato.ObtenerTodos();
            return View(listaContratos);
        }

        // GET: ContratoController/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            Contrato contrato = rContrato.ObtenerPorId(id);
            return View(contrato);
        }

        // GET: ContratoController/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContratoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(Contrato contrato)
        {
            ViewData["Inquilinos"] = rInquilino.ObtenerTodos();
            ViewData["Inmuebles"] = rInmueble.ObtenerTodos();
            rContrato.Alta(contrato);
            return RedirectToAction(nameof(Index));
        }

        // GET: ContratoController/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            var contrato = rContrato.ObtenerPorId(id);
            return View(contrato);
        }

        // POST: ContratoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(int id, Contrato contrato)
        {
            rContrato.Modificacion(contrato);
            return RedirectToAction(nameof(Index));

        }

        // GET: ContratoController/Delete/5
        [Authorize(Policy = "Administrador")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ContratoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Administrador")]
        public ActionResult Delete(int id, Contrato contrato)
        {
            {
                try
                {
                    rContrato.Baja(id);

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
