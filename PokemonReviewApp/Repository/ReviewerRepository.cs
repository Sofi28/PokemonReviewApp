namespace PokemonReviewApp.Repository
{
    using PokemonReviewApp.Data;
    using PokemonReviewApp.Interfaces;
    using PokemonReviewApp.Models;

    using System.Collections.Generic;

    public class ReviewerRepository : IReviewerRepository
    {

        private readonly DataContext _context;
        public ReviewerRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Review> GetAllReviewsByReviewer(int reviewerId)
        {
            return _context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList();
        }

        public Reviewer GetReviewer(int reviewerId)
        {
            return _context.Reviewers.FirstOrDefault(r => r.Id == reviewerId);
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            _context.Add(reviewer);
            return this.Save();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _context.Reviewers.OrderBy(r=> r.FirstName).ToList();    
        }

        public bool ReviewerExists(int reviewId)
        {
            return _context.Reviewers.Any(r=> r.Id == reviewId);
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool updateReviewer(Reviewer reviewer)
        {
            _context.Update(reviewer);
            return this.Save();
        }
    }
}
