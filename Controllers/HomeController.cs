using AlumnosYCursos_ASP.NET_CORE_MVC_.Context;
using AlumnosYCursos_ASP.NET_CORE_MVC_.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AlumnosYCursos_ASP.NET_CORE_MVC_.Controllers
{

    
    public class HomeController : Controller
    {
        AlumnoContext context; //Prueba de conexion a la base de datos
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, AlumnoContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            //Prueba de conexion a la base de datos. Crea un base de datos si no hay una creada al llamar al metodo index
            // y elimina la base de datos si hay una creada.
            if (context.Database.EnsureCreated())
            {
                _logger.LogInformation("BASE DE DATOS CREADA ANIMALLL");
            }
            else
            { 
                context.Database.EnsureDeleted();
                _logger.LogInformation("BASE DE DATOS ELIMINADA");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
