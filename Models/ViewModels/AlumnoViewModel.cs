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

        [Required(ErrorMessage ="Debe seleccionar un curso")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un curso válido")]
        public int cursoId { get; set; }

        [NotMapped]
        public List<Curso>? cursos { get; set; }


    }
}
