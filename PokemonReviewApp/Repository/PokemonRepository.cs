namespace PokemonReviewApp.Repository
{
    using PokemonReviewApp.Data;
    using PokemonReviewApp.Interfaces;
    using PokemonReviewApp.Models;

    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;

        public PokemonRepository(DataContext context)
        {
            _context = context;
        }

        public Pokemon GetPokemon(int id)
        {
            return _context.Pokemon.Where(p=> p.Id == id).FirstOrDefault();
        }

        public Pokemon GetPokemon(string name)
        {
            return _context.Pokemon.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetPokemonRating(int pokeId)
        {
            var reviews = _context.Reviews.Where(r => r.Pokemon.Id == pokeId).ToList();
        
            if(reviews.Count == 0)        
                return 0;
            else
                return ((decimal)reviews.Sum(r => r.Rating) / reviews.Count());
        }

        public ICollection<Pokemon> GetPokemons()
        {
             return _context.Pokemon.OrderBy(p => p.Id).ToList();
        }

        public bool PokemonExists(int pokeId)
        {
            return _context.Pokemon.Any(p => p.Id == pokeId);
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            var pokemonOwnerEntity = _context.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
            var categoryEntity = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();   
            
            var pokemonOwner = new PokemonOwner()
            {
                Owner = pokemonOwnerEntity,
                OwnerId = ownerId,
                Pokemon = pokemon,
                PokemonId = pokemon.Id
            };

            var pokemonCategory = new PokemonCategory()
            {
                Category = categoryEntity,
                CategoryId = categoryId,
                Pokemon = pokemon,
                PokemonId = pokemon.Id
            };

            _context.Add(pokemonOwner);
            _context.Add(pokemonCategory);
            _context.Add(pokemon);

            return this.Save();
        }

        public bool updatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            _context.Update(pokemon);
            return this.Save();
        }
    }
}
