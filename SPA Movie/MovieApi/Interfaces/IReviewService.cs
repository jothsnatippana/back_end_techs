using MovieApi.Domains.RequestModels;
using MovieApi.Domains.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApi.Services
{
    public interface IReviewService
    {
        Task<ResponseReview> GetReviewByIdAsync(int id);
        Task<IEnumerable<ResponseReview>> GetAllReviewsAsync();
        Task<int> AddReviewAsync(RequestReview reqReview,int movieid);
        Task DeleteReviewAsync(int id);
        Task UpdateReviewAsync(int id, RequestReview reqReview, int movieid);
    }
}