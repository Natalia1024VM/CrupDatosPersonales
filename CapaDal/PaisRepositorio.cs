using CapaDal.Entidad;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace CapaDal
{

    public class PaisRepositorio
    {
        public async Task<List<Pais>> ConsultarPaiss(ApplicationDbContext conection)
        {
            var PaissRespuesta = conection.Pais
                .FromSqlInterpolated($"Exec SP_ConsultarPais ")
                .AsAsyncEnumerable();

            List<Pais> Lista = new List<Pais>();

            await foreach (var item in PaissRespuesta)
            {
                Lista.Add(item);
            }
            return Lista;
        }
        public async Task<Pais> ConsultarPaisIDAsync(int id, ApplicationDbContext conection)
        {
            var parametroID = new SqlParameter("@IdPais", SqlDbType.Int);
            parametroID.Direction = ParameterDirection.Output;

            var PaissRespuesta = conection.Pais
                .FromSqlInterpolated($"Exec SP_ConsultarPaisPorID @idPais={id}")
                .AsAsyncEnumerable();

            await foreach (var item in PaissRespuesta)
            {
                return item;
            }
            return null;
        }

        public int AgregarPais(Pais Pais, ApplicationDbContext conection)
        {
            var id = 0;
            var parametroID = new SqlParameter("@resultado", SqlDbType.Int);
            parametroID.Direction = ParameterDirection.Output;

            conection.Database
                .ExecuteSqlInterpolatedAsync($@"Exec SP_InsertarPais
                                             @Nombre={Pais.NombrePais}, @IdPais={Pais.idPais}, @resultado={parametroID} OUTPUT");

            System.Threading.Thread.Sleep(100);

            if (parametroID.SqlValue != null)
            {
                string valor = parametroID.SqlValue.ToString();
                id = Convert.ToInt32(valor);
            }
            return id;
        }

        public int ModificarPais(int idPais, Pais PaisEntidad, ApplicationDbContext conection)
        {
            var id = 0;
            var parametroID = new SqlParameter("@resultado", SqlDbType.Int);
            parametroID.Direction = ParameterDirection.Output;

            conection.Database
                .ExecuteSqlInterpolatedAsync($@"Exec SP_ModificarPais
                                            @IdPais={idPais} , @Nombre={PaisEntidad.NombrePais}, @resultado = {parametroID} OUTPUT");
            System.Threading.Thread.Sleep(100);

            if (parametroID.SqlValue != null)
            {
                string valor = parametroID.SqlValue.ToString();
                id = Convert.ToInt32(valor);
            }
            return id;
        }

        public int EliminarPais(int idPais, ApplicationDbContext conection)
        {
            var id = 0;
            var parametroID = new SqlParameter("@resultado", SqlDbType.Int);
            parametroID.Direction = ParameterDirection.Output;

            conection.Database
                .ExecuteSqlInterpolatedAsync($@"Exec SP_EliminarPais   
                                            @IdPais={idPais}, @resultado = {parametroID} OUTPUT");
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
