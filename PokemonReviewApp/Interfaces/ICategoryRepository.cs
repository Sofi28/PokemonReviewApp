﻿namespace PokemonReviewApp.Interfaces
{
    using PokemonReviewApp.Models;

    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);
        ICollection<Pokemon> GetPokemonsByCategory(int categoryId);
        bool CategoryExists(int id);
    }
}
