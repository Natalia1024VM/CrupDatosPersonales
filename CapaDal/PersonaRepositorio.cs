using CapaDal.Entidad;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDal
{
    public class PersonaRepositorio
    {
        public async Task<List<Persona>> ConsultarPersonas(ApplicationDbContext conection)
        {
            var PersonasRespuesta = conection.Persona
                .FromSqlInterpolated($"Exec SP_ConsultarPersona ")
                .AsAsyncEnumerable();

            List<Persona> Lista = new List<Persona>();

            await foreach (var item in PersonasRespuesta)
            {
                Lista.Add(item);
            }
            return Lista;
        }
        public async Task<List<Persona>> ConsultarPersonasNombre(string nombre, ApplicationDbContext conection)
        {
            var PersonasRespuesta = conection.Persona
                .FromSqlInterpolated($"Exec SP_ConsultarPersonaNombre @nombre={nombre}")
                .AsAsyncEnumerable();

            List<Persona> Lista = new List<Persona>();

            await foreach (var item in PersonasRespuesta)
            {
                Lista.Add(item);
            }
            return Lista;
        }
        public async Task<Persona> ConsultarPersonaIDAsync(int id, ApplicationDbContext conection)
        {
            
            var PersonasRespuesta = conection.Persona
                .FromSqlInterpolated($"Exec SP_ConsultarPersonaPorID @idPersona={id}")
                .AsAsyncEnumerable();

            await foreach (var item in PersonasRespuesta)
            {
                return item;
            }
            return null;
        }

        public int AgregarPersona(Persona Persona, ApplicationDbContext conection)
        {
            var id = 0;
            var parametroID = new SqlParameter("@idPersona", SqlDbType.Int);
            parametroID.Direction = ParameterDirection.Output;

            conection.Database
                .ExecuteSqlInterpolatedAsync($@"Exec SP_InsertarPersona
                                             @Nombre={Persona.Nombre},@Telefono={Persona.Telefono}, @Direccion={Persona.Direccion}, @CodigoCiudad={Persona.CodigoCiudad},@CodigoDepartamento={Persona.CodigoDepartamento}, @idpais={Persona.CodigoPais}, @idPersona={parametroID} OUTPUT");

            System.Threading.Thread.Sleep(100);

            if (parametroID.SqlValue != null)
            {
                string valor = parametroID.SqlValue.ToString();
                id = Convert.ToInt32(valor);
            }
            return id;
        }

        public int ModificarPersona(int idPersona, Persona PersonaEntidad, ApplicationDbContext conection)
        {
            var id = 0;
            var parametroID = new SqlParameter("@resultado", SqlDbType.Int);
            parametroID.Direction = ParameterDirection.Output;

            conection.Database
                .ExecuteSqlInterpolatedAsync($@"Exec SP_ModificarPersona
                                            @IdPersona={idPersona} , @Nombre={PersonaEntidad.Nombre}, @Telefono={PersonaEntidad.Telefono}, @Direccion={PersonaEntidad.Direccion},@CodigoCiudad={PersonaEntidad.CodigoCiudad},@CodigoDepartamento={PersonaEntidad.CodigoDepartamento},@idpais={PersonaEntidad.CodigoPais}, @resultado = {parametroID} OUTPUT");
            System.Threading.Thread.Sleep(100);

            if (parametroID.SqlValue != null)
            {
                string valor = parametroID.SqlValue.ToString();
                id = Convert.ToInt32(valor);
            }
            return id;
        }

        public int EliminarPersona(int idPersona, ApplicationDbContext conection)
        {
            var id = 0;
            var parametroID = new SqlParameter("@resultado", SqlDbType.Int);
            parametroID.Direction = ParameterDirection.Output;

            conection.Database
                .ExecuteSqlInterpolatedAsync($@"Exec SP_EliminarPersona   
                                            @IdPersona={idPersona}, @resultado = {parametroID} OUTPUT");
            System.Threading.Thread.Sleep(100);

            if (parametroID.SqlValue != null)
            {
                string valor = parametroID.SqlValue.ToString();
                id = Convert.ToInt32(valor);
            }
            return id;
        }
    }
}
