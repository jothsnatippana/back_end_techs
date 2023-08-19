using Microsoft.Extensions.Options;
using MovieApi.Domains.Models;
using MovieApi.Domains.RequestModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.Repositories
{
    public class ProducerRepository :BaseRepository<Producer>, IProducerRepository
    {
      
        

        public ProducerRepository(IOptions<ConnectionString> connectionString) : base(connectionString.Value.DefaultConnection) { }

        public async Task<IEnumerable<Producer>> GetAllProducersAsync()
        {
            const string query = @"SELECT
                                         Id,
	                                    Name
	                                    ,Bio
	                                    ,Dob
                                        ,Gender
                                   FROM Foundation.Producers (NOLOCK)";
            return await GetAllAsync(query);
        }


        public async Task<Producer> GetProducerByIdAsync(int id)
        {
            const string query = @"
                                 SELECT
                                        Id
	                                    ,Name
	                                    ,Bio
	                                    ,Dob
                                        ,Gender
                                 FROM  Foundation.Producers (NOLOCK)
                                 WHERE Id=@Id";
            return await GetByIdAsync(query, id);
        }


        public async Task<int> AddProducerAsync(RequestProducer reqProducer)
        {
            string query = @"
                       INSERT INTO Foundation.Producers (
	                            Name
	                            ,Dob
	                            ,Bio
	                            ,Gender
	                            )
                       VALUES (
	                            @Name
	                            ,@Dob
	                            ,@Bio
	                            ,@Gender
	                            );
                       SELECT CAST(scope_identity() AS int); ";
            return await AddAsync(query, reqProducer);
        }

        public async Task DeleteProducerAsync(int id)
        {
            string query = @"
                           DELETE FROM Foundation.Producers where Id=@Id;
                           DELETE From Foundation.Movies where Id=@Id;";
            await DeleteAsync(query, id);
        }

        public async Task UpdateProducerAsync(int id, RequestProducer reqProducer)
        {
            string query = @"UPDATE Foundation.Producers
                             SET Name = @Name
	                            ,Dob = @Dob
	                            ,Bio = @Bio
	                            ,Gender = @Gender
                             WHERE Id = @id";
            await UpdateAsync(query, new { id = id, Name = reqProducer.Name, Dob = reqProducer.DOB, Bio = reqProducer.Bio, Gender = reqProducer.Gender });
        }
    }
}
