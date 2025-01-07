namespace AlumnosYCursos_ASP.NET_CORE_MVC_.Models
{
    public class Curso
    {
        public int idCurso { get; set; }
        public string nombreCurso { get; set; }
        public string descripcionCurso { get; set; }

        //Relacion con alumno
        public List<Alumno> alumnos { get; set; }


    }
}
