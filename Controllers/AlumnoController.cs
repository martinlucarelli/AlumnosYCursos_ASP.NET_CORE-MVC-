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
            alumno.cursos = null;
            if(!ModelState.IsValid) 
            {
                var errores = ModelState.Values.SelectMany(v => v.Errors) //Capturar los errores del modelstate
                               .Select(e => e.ErrorMessage);

                foreach (var error in errores)
                {
                    logger.LogError(error);
                }

                return BadRequest();
            }

            var newAlumno = new Alumno
            {
                nombreAlumno = alumno.nombreAlumno,
                edadAlumno = alumno.edadAlumno,
                cursoId = alumno.cursoId,
                
                
            };

            context.alumnos.Add(newAlumno);
            //try
            //{
                context.SaveChanges();
            /*}
            catch(Exception ex) 
            {
                logger.LogError(ex.Message + "ERROR AL AGREGAR ALUMNO");
            }*/
            
            
            return RedirectToAction("AlumnoAgregadoConExito");
        }

        public IActionResult AlumnoAgregadoConExito()
        {

            return View();
        }
    }
}
