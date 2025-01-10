using AlumnosYCursos_ASP.NET_CORE_MVC_.Models;
using Microsoft.EntityFrameworkCore;

namespace AlumnosYCursos_ASP.NET_CORE_MVC_.Context
{
    public class AlumnoContext : DbContext
    {
        public DbSet<Alumno> alumnos { get; set; }
        public DbSet<Curso> cursos { get; set; }

        public AlumnoContext(DbContextOptions<AlumnoContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Curso>(curso =>
            {
                curso.ToTable("curso");
                curso.HasKey(c=>c.idCurso);
                curso.Property(c => c.nombreCurso).IsRequired();
                curso.Property(c=> c.cargaHoraria).IsRequired();
                curso.Property(c=>c.descripcionCurso).IsRequired(false);

            });

            modelBuilder.Entity<Alumno>(alumno =>
            {
                alumno.ToTable("alumno");
                alumno.HasKey(a => a.idAlumno);
                alumno.Property(a => a.idAlumno).ValueGeneratedOnAdd(); //Generar id automaticamente al agregar registro.
                alumno.Property(a => a.idAlumno).UseIdentityColumn(1000, 1);//Iniciar en 1000 e incrementar en 1.

                //Se debe incrementar el id en cada registro a partir del 100.
                alumno.Property(a => a.nombreAlumno).IsRequired().HasMaxLength(80);
                alumno.Property(a => a.edadAlumno).IsRequired();
                //clave foranea
                alumno.HasOne(a => a.Curso).WithMany(c => c.alumnos).HasForeignKey(a => a.cursoId);
            });

        }
    }
}
