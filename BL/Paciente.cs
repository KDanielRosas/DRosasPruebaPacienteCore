using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;

namespace BL
{
    public class Paciente
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new();

            try
            {
                using (DL.DrosasPruebaPacienteContext context = new())
                {
                    var query = context.Pacientes.FromSqlRaw("PacienteGetAll").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();                        
                        foreach (var item in query)
                        {
                            ML.Paciente paciente = new ML.Paciente();

                            paciente.IdPaciente = item.IdPaciente;
                            paciente.Nombre = item.Nombre;
                            paciente.ApellidoPaterno = item.ApellidoPaterno;
                            paciente.ApellidoMaterno = item.ApellidoMaterno;
                            paciente.FechaNacimiento = item.FechaNaciemiento.Value.ToString("dd-MM-yyyy");                            
                            paciente.TipoSangre = new ML.TipoSangre();
                            paciente.TipoSangre.IdTipoSangre = item.IdTipoSangre.Value;
                            paciente.NombreTipoSangre = item.NombreTipoSangre;
                            paciente.Sexo = item.Sexo;
                            paciente.FechaIngreso = item.FechaIngreso.Value;
                            paciente.Diagnostico = item.Diagnostico;                            
                            result.Objects.Add(paciente);                            
                        }//foreach
                        result.Correct = true;
                    }//if
                }//context
            }//try
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.Correct = false;
                throw;
            }
            return result;
        }//GetAll

        public static ML.Result GetById(int idPaciente)
        {
            ML.Result result = new();

            try
            {
                using (DL.DrosasPruebaPacienteContext context = new())
                {
                    var query = context.Pacientes.FromSqlRaw($"PacienteGetById {idPaciente}").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        
                            ML.Paciente paciente = new ML.Paciente();

                            paciente.IdPaciente = query.IdPaciente;
                            paciente.Nombre = query.Nombre;
                            paciente.ApellidoPaterno = query.ApellidoPaterno;
                            paciente.ApellidoMaterno = query.ApellidoMaterno;
                            paciente.FechaNacimiento = query.FechaNaciemiento.Value.ToString("dd-MM-yyyy");
                            paciente.TipoSangre = new ML.TipoSangre();
                            paciente.TipoSangre.IdTipoSangre = query.IdTipoSangre.Value;
                            paciente.NombreTipoSangre = query.NombreTipoSangre;
                            paciente.Sexo = query.Sexo;
                            paciente.FechaIngreso = query.FechaIngreso.Value;
                            paciente.Diagnostico = query.Diagnostico;

                            result.Object = paciente;
                        
                        result.Correct = true;
                    }//if
                }//context
            }//try
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.Correct = false;
                throw;
            }
            return result;
        }//GetAll

        public static ML.Result Add(ML.Paciente paciente)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DrosasPruebaPacienteContext context = new())
                {
                    var query = context.Database.ExecuteSqlRaw($"PacienteAdd '{paciente.Nombre}', " +
                        $"'{paciente.ApellidoPaterno}', '{paciente.ApellidoMaterno}', '{paciente.FechaNacimiento}'," +
                        $"{paciente.TipoSangre.IdTipoSangre}, '{paciente.Sexo}', '{paciente.Diagnostico}'");

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                throw;
            }
            return result;
        }

        public static ML.Result Update(ML.Paciente paciente)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DrosasPruebaPacienteContext context = new())
                {
                    var query = context.Database.ExecuteSqlRaw($"PacienteUpdate {paciente.IdPaciente} '{paciente.Nombre}', " +
                        $"'{paciente.ApellidoPaterno}', '{paciente.ApellidoMaterno}', '{paciente.FechaNacimiento}'," +
                        $"{paciente.TipoSangre.IdTipoSangre}, '{paciente.Sexo}', '{paciente.Diagnostico}'");

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                throw;
            }
            return result;
        }
        public static ML.Result Delete(int idPaciente)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.DrosasPruebaPacienteContext context = new DL.DrosasPruebaPacienteContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"PacienteDelete {idPaciente}");

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                throw;
            }
            return result;
        }
    }
}