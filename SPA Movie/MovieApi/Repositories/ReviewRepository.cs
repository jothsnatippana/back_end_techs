using Microsoft.Extensions.Options;
using MovieApi.Domains.Models;
using MovieApi.Domains.RequestModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApi.Repositories
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        
        public ReviewRepository(IOptions<ConnectionString> connectionString) : base(connectionString.Value.DefaultConnection) { }

        public async Task<IEnumerable<Review>> GetAllReviewsAsync()
        {
            const string query = @"
            SELECT 
                  Id,Message,MovieId
            FROM Foundation.Review (NOLOCK)";
            return await GetAllAsync(query);
        }

        public async Task<Review> GetReviewByIdAsync(int id)
        {
            const string query = @"
                                 SELECT Id
                                        ,Message
                                        ,MovieId
                                 FROM Foundation.Review (NOLOCK)
                                 WHERE MovieId=@Id";
            return await GetByIdAsync(query, id);
        }

        public async Task<int> AddReviewAsync(RequestReview reqReview,int movieid)
        {
            string query = @"
                        INSERT INTO Foundation.Review (
	                            Message
                               ,MovieId
	                            )
                        VALUES (
                                @message
                                ,@movieid
	                            );
                       SELECT CAST(scope_identity() AS int);";
            return await AddAsync(query, new {message=reqReview.Message,movieid=movieid });
        }

        public async Task DeleteReviewAsync(int id)
        {
            string query = @"
                           DELETE 
                           FROM Foundation.Review 
                           where Id=@id";
            await DeleteAsync(query, id);
        }

        public async Task UpdateReviewAsync(int id_update, RequestReview reqReview,int movieid)
        {
            string query = @"UPDATE Foundation.Review
                             SET Message = @message,MovieId = @movieid
                             WHERE Id = @id";
            await UpdateAsync(query, new { id = id_update, message = reqReview.Message, movieid = movieid });
        }
    }
}
