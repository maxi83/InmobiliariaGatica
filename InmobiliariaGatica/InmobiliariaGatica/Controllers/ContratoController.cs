using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InmobiliariaGatica.Models;
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
        public ActionResult Index()
        {
            var listaContratos = rContrato.ObtenerTodos();
            return View(listaContratos);
        }

        // GET: ContratoController/Details/5
        public ActionResult Details(int id)
        {
            Contrato contrato = rContrato.ObtenerPorId(id);
            return View(contrato);
        }

        // GET: ContratoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContratoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contrato contrato)
        {
            ViewData["Inquilinos"] = rInquilino.ObtenerTodos();
            ViewData["Inmuebles"] = rInmueble.ObtenerTodos();
            rContrato.Alta(contrato);
            return RedirectToAction(nameof(Index));
        }

        // GET: ContratoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ContratoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Contrato contrato)
        {
            rContrato.Modificacion(contrato);
            return RedirectToAction(nameof(Index));

        }

        // GET: ContratoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ContratoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Contrato contrato)
        {
            {
                try
                {
                    rContrato.Baja(id);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View(contrato);
                }
            }
        }
    }
}
