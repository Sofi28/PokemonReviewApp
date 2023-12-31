﻿namespace PokemonReviewApp.Interfaces
{
    using PokemonReviewApp.Models;

    public interface IOwnerRepository
    {
        ICollection<Owner> GetOwners();
        Owner GetOwner(int ownerId);
        ICollection<Owner> GetOwnerOfAPokemon(int pokeId);
        ICollection<Pokemon> GetPokemonByOwner(int ownerId);
        bool OwnerExists(int ownerId);
        bool CreateOwner(Owner owner);
        bool Save();
        bool updateOwner(Owner owner);

    }
}
