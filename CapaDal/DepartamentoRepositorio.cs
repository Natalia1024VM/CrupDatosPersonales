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
    public class DepartamentoRepositorio
    {
        public async Task<List<Departamento>> ConsultarDepartamentos(ApplicationDbContext conection)
        {
            var DepartamentosRespuesta = conection.Departamento
                .FromSqlInterpolated($"Exec SP_ConsultarDepartamento ")
                .AsAsyncEnumerable();

            List<Departamento> Lista = new List<Departamento>();

            await foreach (var item in DepartamentosRespuesta)
            {
                Lista.Add(item);
            }
            return Lista;
        }
        public async Task<List<Departamento>> ConsultarDepartamentosPorPais(int idPais, ApplicationDbContext conection)
        {
            var DepartamentosRespuesta = conection.Departamento
                .FromSqlInterpolated($"Exec SP_ConsultarDepartamentoPorPais @idPais={idPais} ")
                .AsAsyncEnumerable();

            List<Departamento> Lista = new List<Departamento>();

            await foreach (var item in DepartamentosRespuesta)
            {
                Lista.Add(item);
            }
            return Lista;
        }
        public async Task<Departamento> ConsultarDepartamentoIDAsync(int id,int idPais, ApplicationDbContext conection)
        {
            
            var DepartamentosRespuesta = conection.Departamento
                .FromSqlInterpolated($"Exec SP_ConsultarDepartamentoPorID @idDepartamento={id}, @idPais={idPais}")
                .AsAsyncEnumerable();

            await foreach (var item in DepartamentosRespuesta)
            {
                return item;
            }
            return null;
        }

        public int AgregarDepartamento(Departamento Departamento, ApplicationDbContext conection)
        {
            var id = 0;
            var parametroID = new SqlParameter("@resultado", SqlDbType.Int);
            parametroID.Direction = ParameterDirection.Output;

            conection.Database
                .ExecuteSqlInterpolatedAsync($@"Exec SP_InsertarDepartamento
                                             @Nombre={Departamento.Nombre}, @IdDepartamento={Departamento.IdDepartamento}, @idPais={Departamento.IdPais},  @resultado={parametroID} OUTPUT");

            System.Threading.Thread.Sleep(100);

            if (parametroID.SqlValue != null)
            {
                string valor = parametroID.SqlValue.ToString();
                id = Convert.ToInt32(valor);
            }
            return id;
        }

        public int ModificarDepartamento(int idDepartamento, int idpais, Departamento DepartamentoEntidad, ApplicationDbContext conection)
        {
            var id = 0;
            var parametroID = new SqlParameter("@resultado", SqlDbType.Int);
            parametroID.Direction = ParameterDirection.Output;

            conection.Database
                .ExecuteSqlInterpolatedAsync($@"Exec SP_ModificarDepartamento
                                            @IdDepartamento={idDepartamento} ,@idpais={idpais}, @Nombre={DepartamentoEntidad.Nombre}, @resultado = {parametroID} OUTPUT");
            System.Threading.Thread.Sleep(100);

            if (parametroID.SqlValue != null)
            {
                string valor = parametroID.SqlValue.ToString();
                id = Convert.ToInt32(valor);
            }
            return id;
        }

        public int EliminarDepartamento(int idDepartamento, int idPais, ApplicationDbContext conection)
        {
            var id = 0;
            var parametroID = new SqlParameter("@resultado", SqlDbType.Int);
            parametroID.Direction = ParameterDirection.Output;

            conection.Database
                .ExecuteSqlInterpolatedAsync($@"Exec SP_EliminarDepartamento   
                                            @IdDepartamento={idDepartamento},@idpais ={idPais}, @resultado = {parametroID} OUTPUT");
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
