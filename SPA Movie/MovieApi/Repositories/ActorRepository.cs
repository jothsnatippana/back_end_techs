using MovieApi.Domains.Models;
using MovieApi.Domains.RequestModels;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Microsoft.Azure.Amqp.Transaction;

namespace MovieApi.Repositories
{
    public class ActorRepository:BaseRepository<Actor>,IActorRepository
    {
        
        public ActorRepository(IOptions<ConnectionString> connectionString):base(connectionString.Value.DefaultConnection)
        {
        }
      
        public async Task<IEnumerable<Actor>> GetAllActorsAsync()
        {
            const string query = @"
            SELECT Id
	                ,Name
	                ,Bio
	                ,Dob
	                ,Gender
                FROM Foundation.Actors(NOLOCK)";
            return await GetAllAsync(query);
        }

        public async Task<Actor> GetActorByIdAsync(int id)
        {
            const string query = @"
                                 SELECT Id
	                                    ,Name
	                                    ,Bio
	                                    ,Dob
	                                    ,Gender
                                 FROM Foundation.Actors
                                 WHERE Id = @Id";
            return await GetByIdAsync(query,id);
        }
        public async Task<int> AddActorAsync(RequestActor reqactor)
        {
            string query = @"
                        INSERT INTO Foundation.Actors (
	                        Name
	                        ,Dob
	                        ,Bio
	                        ,Gender
	                        )
                        VALUES (
	                        @Name
	                        ,@DOB
	                        ,@Bio
	                        ,@Gender
	                        ) ;
                        SELECT CAST(scope_identity() AS int);";
            
            return await AddAsync(query, reqactor);

        }
        public async Task DeleteActorAsync(int id) 
        {
            string query =
                           @"DELETE FROM Foundation.Actors where Id=@Id;
                             DELETE From Foundation.Actors_Movies where ActorId=@Id;
                            ";
            await DeleteAsync(query,id);
        }

        public async Task UpdateActorAsync(int id_update,RequestActor reqactor)
        {
            string query = @"UPDATE Foundation.Actors
                             SET Name = @Name
	                            ,Dob = @DOB
	                            ,Bio = @Bio
	                            ,Gender = @Gender
                             WHERE Id = @Id";
            await UpdateAsync(query, new { Id = id_update, Name = reqactor.Name, Bio = reqactor.Bio, DOB = reqactor.DOB, Gender = reqactor.Gender });
        }
    }
}
