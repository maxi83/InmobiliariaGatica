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
    public class PropietariosController : Controller
    {
        private RepositorioPropietario repositorio;
        public PropietariosController(IConfiguration configuration )
        {
            repositorio = new RepositorioPropietario(configuration);  
        }
        // GET: PropietariosController
        public ActionResult Index()
        {
           
            {
                var lista = repositorio.ObtenerTodos();
                return View(lista);
            }
          
           
        }


        // GET: PropietariosController/Details/5
        public ActionResult Details(int id)
        {
            Propietario propietario = repositorio.BuscarPropietario(id);
            return View(propietario);
        }

        // GET: PropietariosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PropietariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Propietario p)
        {
            try
            {
                repositorio.Alta( p);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        // GET: PropietariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Propietario propietario)
        {
            repositorio.Modificacion(propietario);
            return RedirectToAction(nameof(Index));

        }
        // POST: PropietariosController/Edit/5
        

        public ActionResult Edit(int id)
        {
            Propietario propietario = repositorio.BuscarPropietario(id);
            return View(propietario);
        }

        // GET: PropietariosController/Delete/5


        // POST: PropietariosController/Delete/5
        
        public ActionResult Delete(int id)
        {
            Propietario propietario = repositorio.BuscarPropietario(id);
            return View(propietario);
        }

        // POST: PropietarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Propietario propietario)
        {
            {
                try
                {
                    repositorio.Baja(id);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View(propietario);
                }
            }
        }
    }
}
