using Microsoft.Extensions.Options;
using MovieApi.Domains.Models;
using MovieApi.Domains.RequestModels;
using MovieApi.Domains.ResponseModels;
using MovieApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _ReviewRepository;
        readonly Validations v;
        public ReviewService(IReviewRepository reviewrepository)
        {
            _ReviewRepository = reviewrepository;
             v = new Validations();
        }

        public async Task<IEnumerable<ResponseReview>> GetAllReviewsAsync()
        {
            var ReviewsList =await  _ReviewRepository.GetAllReviewsAsync();
            var result = ReviewsList.Select(o => new ResponseReview(o.Id, o.MovieId, o.Message));
            return result;
        }

        public async Task<ResponseReview> GetReviewByIdAsync(int id)
        {
            var review = await _ReviewRepository.GetReviewByIdAsync(id);
            if (review == null)
                return null;
            return new ResponseReview(review.Id, review.MovieId, review.Message);
        }
        public async Task<int> AddReviewAsync(RequestReview reqReview,int movieid)
        {
             return await _ReviewRepository.AddReviewAsync(reqReview,movieid);
        }

        public async Task DeleteReviewAsync(int id)
        {
            
            await _ReviewRepository.DeleteReviewAsync(id);
        }

        public async  Task UpdateReviewAsync(int id, RequestReview reqReview,int movieid)
        {
            v.InputValidate(reqReview.Message);
            await _ReviewRepository.UpdateReviewAsync(id, reqReview,movieid);
        }
    }
}
