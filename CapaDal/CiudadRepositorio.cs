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
    public class CiudadRepositorio
    {
        public async Task<List<Ciudad>> ConsultarCiudades(ApplicationDbContext conection)
        {
            var CiudadsRespuesta = conection.Ciudad
                .FromSqlInterpolated($"Exec SP_ConsultarCiudades")
                .AsAsyncEnumerable();

            List<Ciudad> Lista = new List<Ciudad>();

            await foreach (var item in CiudadsRespuesta)
            {
                Lista.Add(item);
            }
            return Lista;
        }
        public async Task<List<Ciudad>> ConsultarCiudadsPorDep(int idDep, int idPais, ApplicationDbContext conection)
        {
            var CiudadsRespuesta = conection.Ciudad
                .FromSqlInterpolated($"Exec SP_ConsultarCiudadPorDepartamento @idDepartamento={idDep}, @idPais = {idPais} ")
                .AsAsyncEnumerable();

            List<Ciudad> Lista = new List<Ciudad>();

            await foreach (var item in CiudadsRespuesta)
            {
                Lista.Add(item);
            }
            return Lista;
        }
        public async Task<Ciudad> ConsultarCiudadIDAsync(int id, int idDep, int idPais, ApplicationDbContext conection)
        {

            var CiudadsRespuesta = conection.Ciudad
                .FromSqlInterpolated($"Exec SP_ConsultarCiudadPorID @idCiudad={id}, @idDepartamento={idDep}, @idPais = {idPais}")
                .AsAsyncEnumerable();

            await foreach (var item in CiudadsRespuesta)
            {
                return item;
            }
            return null;
        }

        public int AgregarCiudad(Ciudad Ciudad, ApplicationDbContext conection)
        {
            var id = 0;
            var parametroID = new SqlParameter("@resultado", SqlDbType.Int);
            parametroID.Direction = ParameterDirection.Output;

            conection.Database
                .ExecuteSqlInterpolatedAsync($@"Exec SP_InsertarCiudad
                                             @Nombre={Ciudad.Nombre}, @IdCiudad={Ciudad.IdCiudad}, @idDepartamento={Ciudad.IdDepartamento}, @idPais = {Ciudad.idPais}, @resultado={parametroID} OUTPUT");

            System.Threading.Thread.Sleep(100);

            if (parametroID.SqlValue != null)
            {
                string valor = parametroID.SqlValue.ToString();
                id = Convert.ToInt32(valor);
            }
            return id;
        }

        public int ModificarCiudad(int idCiudad, Ciudad CiudadEntidad, ApplicationDbContext conection)
        {
            var id = 0;
            var parametroID = new SqlParameter("@resultado", SqlDbType.Int);
            parametroID.Direction = ParameterDirection.Output;

            conection.Database
                .ExecuteSqlInterpolatedAsync($@"Exec SP_ModificarCiudad
                                            @IdCiudad={idCiudad} ,@idDepartamento={CiudadEntidad.IdDepartamento},@idPais ={CiudadEntidad.idPais}, @Nombre={CiudadEntidad.Nombre}, @resultado = {parametroID} OUTPUT");
            System.Threading.Thread.Sleep(100);

            if (parametroID.SqlValue != null)
            {
                string valor = parametroID.SqlValue.ToString();
                id = Convert.ToInt32(valor);
            }
            return id;
        }

        public int EliminarCiudad(int idCiudad, int idDep, int idPais, ApplicationDbContext conection)
        {
            var id = 0;
            var parametroID = new SqlParameter("@resultado", SqlDbType.Int);
            parametroID.Direction = ParameterDirection.Output;

            conection.Database
                .ExecuteSqlInterpolatedAsync($@"Exec SP_EliminarCiudad   
                                            @IdCiudad={idCiudad},@idDepartamento ={idDep},@idPais ={idPais}, @resultado = {parametroID} OUTPUT");
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
