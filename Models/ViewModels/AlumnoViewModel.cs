using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlumnosYCursos_ASP.NET_CORE_MVC_.Models.ViewModels
{
    public class AlumnoViewModel
    {

        [Required(ErrorMessage = "Campo obligatorio")]
        public string? nombreAlumno { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        public int edadAlumno { get; set; }
        public int cursoId { get; set; }

        [NotMapped]
        public List<Curso>? cursos { get; set; }


    }
}
