using StGeneticsAPI.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace StGeneticsAPI.Logic
{
    public class Cl_Animal
    {
        SqlConnection ConexionSqlServer = new SqlConnection(Properties.Settings.Default.StringConnection);
        SqlCommand Comando;
        SqlDataAdapter AdaptadorSql = new SqlDataAdapter();
        string RespuestaBaseDatos;

        public async Task<string> InsertAnimal(AnimalModel Animal)
        {
            try
            {
                await ConexionSqlServer.OpenAsync();
                Comando = new SqlCommand("SPR_INS_ANIMAL", ConexionSqlServer);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.AddWithValue("@PAR_NAME", Animal.Name);
                Comando.Parameters.AddWithValue("@PAR_BREED", Animal.Breed);
                Comando.Parameters.AddWithValue("@PAR_BIRTHDATE", Animal.BirthDate);
                Comando.Parameters.AddWithValue("@PAR_SEX", Animal.Sex);
                Comando.Parameters.AddWithValue("@PAR_PRICE", Animal.Price);
                Comando.Parameters.AddWithValue("@PAR_STATUS", Animal.Status);

                using (var RespuestaReader = Comando.ExecuteReader())
                {
                    if (RespuestaReader.Read())
                    {
                        RespuestaBaseDatos = RespuestaReader.GetString(0);
                        return RespuestaBaseDatos;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ConexionSqlServer.Close();
            }
        }

        public async Task<bool> UpdateAnimal(AnimalModel Animal)
        {
            try
            {
                await ConexionSqlServer.OpenAsync();
                Comando = new SqlCommand("SPR_UPD_ANIMAL", ConexionSqlServer);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.AddWithValue("@PAR_ANIMALID", Animal.AnimalId);
                Comando.Parameters.AddWithValue("@PAR_NAME", Animal.Name);
                Comando.Parameters.AddWithValue("@PAR_BREED", Animal.Breed);
                Comando.Parameters.AddWithValue("@PAR_SEX", Animal.Sex);

                if (await Comando.ExecuteNonQueryAsync() == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ConexionSqlServer.Close();
            }
        }

        public async Task<bool> DeleteAnimal(int AnimalId)
        {
            try
            {
                await ConexionSqlServer.OpenAsync();
                Comando = new SqlCommand("SPR_DEL_ANIMAL", ConexionSqlServer);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.AddWithValue("@PAR_ANIMALID", AnimalId);

                if (await Comando.ExecuteNonQueryAsync() == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ConexionSqlServer.Close();
            }
        }
    }
}