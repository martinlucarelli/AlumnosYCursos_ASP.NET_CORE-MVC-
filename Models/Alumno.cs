namespace AlumnosYCursos_ASP.NET_CORE_MVC_.Models
{
    public class Alumno
    {
        public int idAlumno { get; set; }
        public string? nombreAlumno { get;set; }
        public int edadAlumno { get; set; }
        public int cursoId { get; set; }

        //Relacion con curso
        public Curso Curso { get; set; }

    }
}
