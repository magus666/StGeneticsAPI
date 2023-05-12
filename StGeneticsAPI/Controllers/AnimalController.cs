using StGeneticsAPI.Logic;
using StGeneticsAPI.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System.Web.Http;
using System.Collections;

namespace StGeneticsAPI.Controllers
{
    public class AnimalController : ApiController
    {
        Cl_Animal ClAnimal = new Cl_Animal();
        SqlConnection ConexionSqlServer = new SqlConnection(Properties.Settings.Default.StringConnection);

        [AcceptVerbs("POST")]
        public async Task<string> PostAnimal(AnimalModel Animal)
        {
            string response = await ClAnimal.InsertAnimal(Animal);
            return response;
        }

        [AcceptVerbs("GET")]
        public async Task<List<AnimalModel>> GetAnimalsAsync(int? animalid, string name = null, string sex = null, string status = null)
        {
            var animals = new List<AnimalModel>();
            {
                await ConexionSqlServer.OpenAsync();
                using (var command = new SqlCommand("SPR_GET_ANIMAL_FILTER", ConexionSqlServer))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if(animalid.HasValue)
                    {
                        command.Parameters.AddWithValue("@PAR_ANIMALID", animalid.Value);
                    }

                    if (!string.IsNullOrEmpty(name))
                    {
                        command.Parameters.AddWithValue("@PAR_NAME", name);
                    }
                    if (!string.IsNullOrEmpty(sex))
                    {
                        command.Parameters.AddWithValue("@PAR_SEX", sex);
                    }
                    if (!string.IsNullOrEmpty(status))
                    {
                        command.Parameters.AddWithValue("@PAR_STATUS", status);
                    }
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            animals.Add(new AnimalModel
                            {
                                AnimalId = reader.GetInt32(reader.GetOrdinal("AnimalId")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Breed = reader.GetString(reader.GetOrdinal("Breed")),
                                BirthDate = reader.GetDateTime(reader.GetOrdinal("BirthDate")),
                                Sex = reader.GetString(reader.GetOrdinal("Sex")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                Status = reader.GetString(reader.GetOrdinal("Status"))
                            });
                        }
                    }
                }
                return animals;
            }
        }

        [AcceptVerbs("PUT")]
        public async Task<bool> PutAnimal(AnimalModel Animal)
        {
            var response = await ClAnimal.UpdateAnimal(Animal);
            return response;
        }

        [AcceptVerbs("DELETE")]
        //[BasicAuthentication]
        public async Task<bool> DeleteAnimal(int AnimalId)
        {
            var response = await ClAnimal.DeleteAnimal(AnimalId);
            return response;
        }
    }
}