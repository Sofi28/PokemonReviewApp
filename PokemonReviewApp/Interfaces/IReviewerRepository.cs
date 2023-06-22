namespace PokemonReviewApp.Interfaces
{
    using PokemonReviewApp.Models;

    public interface IReviewerRepository
    {
        ICollection<Reviewer> GetReviewers();
        Reviewer GetReviewer(int reviewerId);
        ICollection<Review> GetAllReviewsByReviewer(int reviewerId);
        bool ReviewerExists(int reviewId);

    }
}
