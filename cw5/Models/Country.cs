using System.ComponentModel.DataAnnotations;

namespace cw5.Models;

public class Country
{
    [Key, Required] 
    public int IdCountry { get; set; }
    [Required, MaxLength(120)]
    public string Name { get; set; }
    
    public ICollection<Country_Trip> CountryTrips { get; set; }
}