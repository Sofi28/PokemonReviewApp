namespace PokemonReviewApp.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Pokemon
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }   
        public DateTime BirthDate { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<PokemonOwner> PokemonOwners { get; set; }
        public ICollection<PokemonCategory> PokemonCategories { get; set; } 

    }
}
