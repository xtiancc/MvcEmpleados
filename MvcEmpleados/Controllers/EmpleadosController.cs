using LibraryEmpleados;
using Microsoft.AspNetCore.Mvc;
using MvcEmpleados.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcEmpleados.Controllers
{
    public class EmpleadosController : Controller
    {
        ServiceApiEmpleados service;

        public EmpleadosController(ServiceApiEmpleados service) {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Empleado> empleados = await this.service.GetEmpleadosAsync();
            List<String> oficios = await this.service.GetOficiosAsync();
            ViewData["OFICIOS"] = oficios;
            return View(empleados);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string oficio)
        {
            List<Empleado> empleados = await this.service.GetEmpleadosOficioAsync(oficio);
            List<String> oficios = await this.service.GetOficiosAsync();
            ViewData["OFICIOS"] = oficios;
            return View(empleados);
        }
    }
}
