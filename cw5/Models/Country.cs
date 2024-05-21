namespace cw5.Models;

public class Country
{
    public int IdCountry { get; set; }
    public string Name { get; set; }
    
    public ICollection<Country_Trip> CountryTrips { get; set; }
}