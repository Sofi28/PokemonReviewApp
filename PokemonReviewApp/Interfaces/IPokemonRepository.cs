namespace PokemonReviewApp.Interfaces
{
    using PokemonReviewApp.Models;

    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons();

    }
}
