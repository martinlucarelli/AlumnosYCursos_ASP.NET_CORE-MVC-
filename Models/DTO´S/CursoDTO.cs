using System.ComponentModel.DataAnnotations;

namespace AlumnosYCursos_ASP.NET_CORE_MVC_.Models.DTO_S
{
    public class CursoDTO
    {
        [Required(ErrorMessage = "Campo obligatorio")]
        public string nombreCurso { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public int cargaHoraria { get; set; }

        public string? descripcionCurso { get; set; }
    }
}
