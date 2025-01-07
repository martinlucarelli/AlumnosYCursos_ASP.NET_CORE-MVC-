namespace AlumnosYCursos_ASP.NET_CORE_MVC_.Models
{
    public class Curso
    {
        public int idCurso { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        //Relacion con alumno
        public List<Alumno> alumnos { get; set; }


    }
}
