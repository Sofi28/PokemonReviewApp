namespace PokemonReviewApp.Repository
{
    using PokemonReviewApp.Data;
    using PokemonReviewApp.Interfaces;
    using PokemonReviewApp.Models;

    using System.Collections.Generic;

    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;

        public ReviewRepository(DataContext context)
        {
            _context = context;
        }

        public Review GetReview(int reviewId)
        { 
            return _context.Reviews.Where(r => r.Id == reviewId).FirstOrDefault();
        }

        public bool CreateReview(Review review)
        {
            _context.Add(review);
            return this.Save();
        }

        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.OrderBy(r=> r.Reviewer.Id).ToList();
        }

        public ICollection<Review> GetReviewsOfAPokemon(int pokeId)
        {
            return _context.Reviews.Where(r => r.Pokemon.Id == pokeId).ToList();
        }

        public bool ReviewExists(int reviewId)
        {
            return _context.Reviews.Any(r=> r.Id == reviewId);
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool updateReview(Review review)
        {
            _context.Update(review);
            return this.Save();
        }
    }
}
