using MovieApi.Domains.Models;
using MovieApi.Domains.RequestModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApi.Repositories
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllReviewsAsync();
        Task<Review> GetReviewByIdAsync(int id);
        Task<int> AddReviewAsync(RequestReview reqReview, int movieid);
        Task DeleteReviewAsync(int id);
        Task UpdateReviewAsync(int id, RequestReview reqReview, int movieid);
    }
}