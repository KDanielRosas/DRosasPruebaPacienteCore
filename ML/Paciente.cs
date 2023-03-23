namespace ML
{
    public class Paciente
    {
        public int IdPaciente { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string FechaNacimiento { get; set; }
        public ML.TipoSangre TipoSangre { get; set; }
        public string Sexo { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Diagnostico { get; set; }
        public string NombreTipoSangre { get; set; }
    }
}