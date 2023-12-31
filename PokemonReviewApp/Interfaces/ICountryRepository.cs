﻿namespace PokemonReviewApp.Interfaces
{
    using PokemonReviewApp.Models;

    public interface ICountryRepository
    {
        ICollection<Country> GetCountries();
        Country getCountry(int id);
        Country getCountryByOwner(int ownerId);
        ICollection<Owner> GetOwnersFromACountry(int countryId);
        bool CountryExists(int id);
        bool CreateCountry(Country country);
        bool Save();
        bool updateCountry(Country country);

    }
}
