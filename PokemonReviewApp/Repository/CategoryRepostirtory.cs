namespace PokemonReviewApp.Repository
{
    using PokemonReviewApp.Interfaces;
    using PokemonReviewApp.Data;
    using System.Collections.Generic;
    using PokemonReviewApp.Models;

    public class CategoryRepostirtory : ICategoryRepository
    {

        private readonly DataContext _context;

        public CategoryRepostirtory(DataContext context)
        {
            _context = context;
        }

        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }

        public bool CreateCategory(Category category)
        {
            _context.Add(category);
            return this.Save();
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.OrderBy(c=> c.Name).ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }

        public ICollection<Pokemon> GetPokemonsByCategory(int categoryId)
        {
            //return _context.Pokemon.Where(p=> p.PokemonCategories.Any(pc => pc.CategoryId == categoryId)).ToList();
            return _context.PokemonCategories.Where(pc => pc.CategoryId == categoryId).Select(c=> c.Pokemon).ToList();
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false ;
        }

        public bool updateCategory(Category category)
        {
            _context.Update(category);
            return this.Save();
        }
    }
}
