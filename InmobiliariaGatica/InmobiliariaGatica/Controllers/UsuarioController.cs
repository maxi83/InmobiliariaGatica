using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InmobiliariaGatica.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace InmobiliariaGatica.Controllers
{
    public class UsuarioController : Controller
    {
        private RepositorioUsuario uRepositorio;

        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment environment;

        public UsuarioController(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.configuration = configuration;
            this.environment = environment;
            uRepositorio = new RepositorioUsuario(configuration);
          
        }
        // GET: UsuarioController
        [Authorize]
        public ActionResult Index()
        {
            var listaUsuarios = uRepositorio.ObtenerTodos();
            return View(listaUsuarios);
        }

        // GET: UsuarioController/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            Usuario usuario = uRepositorio.ObtenerPorId(id);
            return View(usuario);
        }

        // GET: UsuarioController/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(Usuario usuario)
        {
            usuario.Avatar = "";
            uRepositorio.Alta(usuario);
            if (usuario.AvatarFile != null && usuario.Id > 0)
            {
                string wwwPath = environment.WebRootPath;
                string path = Path.Combine(wwwPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                //Path.GetFileName(u.AvatarFile.FileName); //este nombre se puede repetir
                string fileName = "avatar_" + usuario.Id + Path.GetExtension(usuario.AvatarFile.FileName);
                string pathCompleto = Path.Combine(path, fileName);
                usuario.Avatar = Path.Combine("/Uploads", fileName);
                using (FileStream stream = new FileStream(pathCompleto, FileMode.Create))
                {
                    usuario.AvatarFile.CopyTo(stream);
                }

            }
            uRepositorio.Modificacion(usuario);

            return RedirectToAction(nameof(Index));
        }

        // GET: UsuarioController/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            Usuario usuario = uRepositorio.ObtenerPorId(id);
            return View(usuario);
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(int id, Usuario usuario)
        {
            uRepositorio.Modificacion(usuario);
            return RedirectToAction(nameof(Index));

        }

        // GET: UsuarioController/Delete/5
        [Authorize(Policy = "Administrador")]
        public ActionResult Delete(int id)
        {
            Usuario usuario = uRepositorio.ObtenerPorId(id);
            return View(usuario);
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Administrador")]
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
