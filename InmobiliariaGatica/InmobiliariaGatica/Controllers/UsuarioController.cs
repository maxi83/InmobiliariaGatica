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
    public class UsuarioController : Controller
    {
        private RepositorioUsuario uRepositorio;

        private readonly IConfiguration configuration;

        public UsuarioController(IConfiguration configuration)
        {
            this.configuration = configuration;
            uRepositorio = new RepositorioUsuario(configuration);
          
        }
        // GET: UsuarioController
        public ActionResult Index()
        {
            var listaUsuarios = uRepositorio.ObtenerTodos();
            return View(listaUsuarios);
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            Usuario usuario = uRepositorio.ObtenerPorId(id);
            return View(usuario);
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario usuario)
        {
            uRepositorio.Alta(usuario);
            return RedirectToAction(nameof(Index));
        }

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            Usuario usuario = uRepositorio.ObtenerPorId(id);
            return View(usuario);
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Usuario usuario)
        {
            uRepositorio.Modificacion(usuario);
            return RedirectToAction(nameof(Index));

        }

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            Usuario usuario = uRepositorio.ObtenerPorId(id);
            return View(usuario);
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Usuario usuario)
        {
            {
                try
                {
                    uRepositorio.Baja(id);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View(usuario);
                }
            }
        }
    }
}
