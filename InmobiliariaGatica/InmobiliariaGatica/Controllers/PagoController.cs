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
    public class PagoController : Controller
    {
        private RepositorioPago pRepositorio;
        private RepositorioContrato cRepositorio;

        private readonly IConfiguration configuration;

        public PagoController(IConfiguration configuration)
        {
            this.configuration = configuration;
            pRepositorio = new RepositorioPago(configuration);
            cRepositorio = new RepositorioContrato(configuration);
        }
        // GET: InmuebleController
        public ActionResult Index()
        {
            List<Pago> lista = pRepositorio.ObtenerTodos();
            return View(lista);
        }

        // GET: InmuebleController/Details/5
        public ActionResult Details(int id)
        {
            Pago pago = pRepositorio.BuscarPago(id);
            return View(pago);
        }

        // GET: InmuebleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PagoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pago pago
            )
        {
            ViewData["Contratos"] = cRepositorio.ObtenerTodos();
            pRepositorio.Alta(pago);
            return RedirectToAction(nameof(Index));
        }

        // GET: PagoController/Edit/5
        public ActionResult Edit(int id)
        {
            Pago pago = pRepositorio.BuscarPago(id);
            ViewData["Contratos"] = cRepositorio.ObtenerTodos();
            return View(pago);
        }

        // POST: InmuebleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Pago pago)
        {
            try
            {
                pRepositorio.Modificacion(pago);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(pago);
            }
        }

        // GET: InmuebleController/Delete/5
        public ActionResult Delete(int id)
        {
            Pago pago = pRepositorio.BuscarPago(id);
            return View(pago);
        }

        // POST: InmuebleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Pago pago)
        {
            try
            {
                pRepositorio.Baja(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(pago);
            }
        }
    }
}
