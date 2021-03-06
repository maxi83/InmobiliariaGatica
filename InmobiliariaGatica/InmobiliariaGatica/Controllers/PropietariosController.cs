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
    public class PropietariosController : Controller
    {
        private RepositorioPropietario repositorio;
        public PropietariosController(IConfiguration configuration )
        {
            repositorio = new RepositorioPropietario(configuration);  
        }
        // GET: PropietariosController
        [Authorize]

        public ActionResult Index()
        {
           
            {
                ViewData["Error"] = TempData["Error"];

                var lista = repositorio.ObtenerTodos();
                return View(lista);
            }
          
           
        }


        // GET: PropietariosController/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            Propietario propietario = repositorio.BuscarPropietario(id);
            return View(propietario);
        }

        // GET: PropietariosController/Create
        [Authorize]

        public ActionResult Create()
        {
            return View();
        }

        // POST: PropietariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
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
        [Authorize]
        public ActionResult Edit(int id, Propietario propietario)
        {
            repositorio.Modificacion(propietario);
            return RedirectToAction(nameof(Index));

        }
        // POST: PropietariosController/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            Propietario propietario = repositorio.BuscarPropietario(id);
            return View(propietario);
        }

        


        // POST: PropietariosController/Delete/5
        [Authorize(Policy = "Administrador")]
        public ActionResult Delete(int id)
        {
            Propietario propietario = repositorio.BuscarPropietario(id);
            return View(propietario);
        }

        // POST: PropietarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Administrador")]
        public ActionResult Delete(int id, Propietario propietario)
        {
            
                try
                {
                    repositorio.Baja(id);

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
