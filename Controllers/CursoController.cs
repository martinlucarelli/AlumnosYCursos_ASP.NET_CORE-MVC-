using AlumnosYCursos_ASP.NET_CORE_MVC_.Context;
using AlumnosYCursos_ASP.NET_CORE_MVC_.Models;
using AlumnosYCursos_ASP.NET_CORE_MVC_.Models.DTO_S;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AlumnosYCursos_ASP.NET_CORE_MVC_.Controllers
{
    public class CursoController : Controller
    {
        AlumnoContext  context;
        public ILogger<CursoController> logger;
       

        public CursoController(AlumnoContext dbContext,ILogger<CursoController> _logger) 
        {
            context=dbContext;
            logger=_logger;
            
        }


        public IActionResult MostrarCursos()
        {
            
        
            var listaDeCursos = context.cursos.ToList();
            return View(listaDeCursos);
        }

        
        
        public IActionResult AgregarCurso() //Metodo encargado de devolver la vista
        {
            
            return View();
            
        }

        [HttpPost]
        public IActionResult AgregarCurso(CursoDTO cursoDTO)
        {
            if(!ModelState.IsValid)
            {
                var errores = ModelState.Values.SelectMany(v => v.Errors) //Capturar los errores del modelstate
                               .Select(e => e.ErrorMessage);

                foreach (var error in errores)
                {
                    logger.LogError(error);
                }


                return View(cursoDTO);
            }

            var curso = new Curso
            {
                nombreCurso = cursoDTO.nombreCurso,
                cargaHoraria = cursoDTO.cargaHoraria,
                descripcionCurso =cursoDTO .descripcionCurso
            };

            context.cursos.Add(curso);
            context.SaveChanges();

            return RedirectToAction("CursoAgregadoExitosamenteVista");
        }
        
        public IActionResult CursoAgregadoExitosamenteVista()
        {
            return View();
        }

        public IActionResult ModificarCurso(int id)
        {
            var curso = context.cursos.FirstOrDefault(c => c.idCurso == id);
            if (curso == null)
            {
                return NotFound();
            }

            var CursoDTO = new CursoDTO
            {
                nombreCurso = curso.nombreCurso,
                cargaHoraria = curso.cargaHoraria,
                descripcionCurso = curso.descripcionCurso
            };

            ViewBag.CursoId = id;
      
            return View(CursoDTO);
        }
        [HttpPost]
        public IActionResult ModificarCurso(int id, CursoDTO cursoUpd)
        {
            var curso = context.cursos.FirstOrDefault(c => c.idCurso == id);
            if(curso== null)
            {
                return NotFound();
            }

            if(!ModelState.IsValid) 
            {
                ViewBag.CursoId = id;
                return View(cursoUpd);
            }

            curso.nombreCurso=cursoUpd.nombreCurso;
            curso.cargaHoraria = cursoUpd.cargaHoraria;
            curso.descripcionCurso = cursoUpd.descripcionCurso;

            context.SaveChanges();

            return RedirectToAction("MostrarCursos");
        }

        public IActionResult EliminarCurso(int id)
        {
           var curso= context.cursos.FirstOrDefault(c=>c.idCurso == id);
           
            if(curso==null)
            {
                return NotFound();
            }
            return View(curso);
        }

        [HttpPost]
        public IActionResult EliminarCurso(Curso cursoEliminar)
        {
            var curso = context.cursos.FirstOrDefault(c => c.idCurso == cursoEliminar.idCurso);

            if(curso==null)
            {
                return NotFound();
            }

            context.cursos.Remove(curso);
            context.SaveChanges();

            return RedirectToAction("MostrarCursos");

        }


    }
}
