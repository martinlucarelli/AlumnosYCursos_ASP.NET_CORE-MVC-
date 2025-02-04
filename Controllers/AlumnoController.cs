using AlumnosYCursos_ASP.NET_CORE_MVC_.Context;
using AlumnosYCursos_ASP.NET_CORE_MVC_.Models;
using AlumnosYCursos_ASP.NET_CORE_MVC_.Models.DTO_S;
using AlumnosYCursos_ASP.NET_CORE_MVC_.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AlumnosYCursos_ASP.NET_CORE_MVC_.Controllers
{
    public class AlumnoController : Controller
    {

        AlumnoContext context;
        public ILogger<AlumnoController> logger;

        public AlumnoController(AlumnoContext contextdb,ILogger<AlumnoController> _logger)
        {
            context= contextdb;
            logger= _logger;
        }

        
        
        
        public IActionResult MostrarAlumnos()
        {

            var alumnosConCurso = context.alumnos.Join(context.cursos,
                                    alumno=> alumno.cursoId,
                                    curso=> curso.idCurso,
                                    (alumno,curso) => new
                                    {
                                        alumno.idAlumno,
                                        alumno.nombreAlumno,
                                        alumno.edadAlumno,
                                        cursoNombre=curso.nombreCurso
                                    })
                                    .ToList();  


            return View(alumnosConCurso);

        }

        public IActionResult AgregarAlumno()
        {

           var viewModel= new AlumnoViewModel();

            viewModel.cursos=context.cursos.ToList();

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult AgregarAlumno(AlumnoViewModel alumno)
        {
            
            if(!ModelState.IsValid) 
            {
                var errores = ModelState.Values.SelectMany(v => v.Errors) //Capturar los errores del modelstate
                               .Select(e => e.ErrorMessage);

                foreach (var error in errores)
                {
                    logger.LogError(error);
                }
                alumno.cursos = context.cursos.ToList() ?? new List<Curso>();
                return View(alumno);
            }

            var newAlumno = new Alumno
            {
                nombreAlumno = alumno.nombreAlumno,
                edadAlumno = alumno.edadAlumno,
                cursoId = alumno.cursoId,
                
                
            };

            context.alumnos.Add(newAlumno);
            context.SaveChanges();
            return RedirectToAction("AlumnoAgregadoConExito");
        }

        public IActionResult AlumnoAgregadoConExito()
        {

            return View();
        }

        public IActionResult ModificarAlumno(int id)
        {
            var alumno = context.alumnos.FirstOrDefault(a=> a.idAlumno == id);

            if(alumno == null)
            {
                return NotFound();
            }

            var alumnoViewModel = new AlumnoViewModel
            {
                nombreAlumno = alumno.nombreAlumno,
                edadAlumno = alumno.edadAlumno,
                cursoId = alumno.cursoId,
                cursos = context.cursos.ToList()

            };

            ViewBag.AlumnoId = id;

            return View(alumnoViewModel);
        }
        [HttpPost]
        public IActionResult ModificarAlumno(int id,AlumnoViewModel alumnoUpd)
        {
            var alumno = context.alumnos.FirstOrDefault(a=> a.idAlumno == id);
            if(alumno == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) 
            {
                ViewBag.AlumnoId = id;
                return View(alumnoUpd);
            }

            alumno.nombreAlumno = alumnoUpd.nombreAlumno;
            alumno.edadAlumno=alumnoUpd.edadAlumno;
            alumno.cursoId=alumnoUpd.cursoId;
            

            context.SaveChanges();

            return RedirectToAction("MostrarAlumnos");

        }

        public IActionResult EliminarAlumno(int id)
        {
            var alumno=context.alumnos.FirstOrDefault(a=>a.idAlumno==id);

            if(alumno==null)
            {
                return NotFound();
            }

            return View(alumno);


        }

        [HttpPost]
        public IActionResult EliminarAlumno(Alumno alumnoEliminar)
        {
            var alumno = context.alumnos.FirstOrDefault(a => a.idAlumno == alumnoEliminar.idAlumno);

            if(alumno==null)
            {
                return NotFound();
            }

            context.alumnos.Remove(alumno);
            context.SaveChanges();

            return RedirectToAction("MostrarAlumnos");

        }


    }
}
