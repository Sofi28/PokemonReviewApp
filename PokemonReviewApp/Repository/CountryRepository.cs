namespace PokemonReviewApp.Repository
{
    using PokemonReviewApp.Data;
    using PokemonReviewApp.Interfaces;
    using PokemonReviewApp.Models;

    using System.Collections.Generic;

    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;

        public CountryRepository(DataContext context)
        {
            _context = context; 
        }

        public bool CountryExists(int id)
        {
            return _context.Countries.Any(c=> c.Id == id);
        }

        public bool CreateCountry(Country country)
        {
            _context.Add(country);
            return this.Save();
        }

        public ICollection<Country> GetCountries()
        {
            return _context.Countries.OrderBy(c=> c.Name).ToList();
        }

        public Country getCountry(int id)
        {
            return _context.Countries.FirstOrDefault(c=> c.Id == id);
        }

        public Country getCountryByOwner(int ownerId)
        {
            return _context.Owners.Where(o => o.Id == ownerId).Select(o => o.Country).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnersFromACountry(int countryId)
        {
            return _context.Owners.Where(o => o.Country.Id == countryId).ToList();
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }
    }
}
