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
    public class InquilinoController : Controller
    {
        private RepositorioInquilino iRepositorio;

        public InquilinoController(IConfiguration configuration)
        {
            iRepositorio = new RepositorioInquilino(configuration);
        }

        // GET: InquilinoController
        public ActionResult Index()
        {
            var listaInquilinos = iRepositorio.ObtenerTodos();
            return View(listaInquilinos);
        }

        // GET: InquilinoController/Details/5
        public ActionResult Details(int id)
        {
            Inquilino inquilino = iRepositorio.ObtenerPorId(id);
            return View(inquilino);
        }

        // GET: InquilinoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InquilinoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inquilino inquilino)
        {
            iRepositorio.Alta(inquilino);
            return RedirectToAction(nameof(Index));
        }

        // GET: InquilinoController/Edit/5
        public ActionResult Edit(int id)
        {
            Inquilino inquilino = iRepositorio.ObtenerPorId(id);
            return View(inquilino);
        }

        // POST: InquilinoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Inquilino inquilino)
        {
            iRepositorio.Modificacion(inquilino);
            return RedirectToAction(nameof(Index));

        }

        // GET: InquilinoController/Delete/5
        public ActionResult Delete(int id)
        {
            Inquilino inquilino = iRepositorio.ObtenerPorId(id);
            return View(inquilino);
        }

        // POST: InquilinoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Inquilino inquilino)
        {
            {
                try
                {
                    iRepositorio.Baja(id);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View(inquilino);
                }
            }
        }
    }
}
