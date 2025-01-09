using System.ComponentModel.DataAnnotations;

namespace AlumnosYCursos_ASP.NET_CORE_MVC_.Models
{
    public class Curso
    {
        public int idCurso { get; set; }

        [Required(ErrorMessage ="Campo obligatorio")]
        public string nombreCurso { get; set; }
    
        [Required(ErrorMessage = "Campo obligatorio")]
        public int cargaHoraria {  get; set; }

        public string? descripcionCurso { get; set; }

        //Relacion con alumno
        public List<Alumno> alumnos { get; set; }


    }
}
