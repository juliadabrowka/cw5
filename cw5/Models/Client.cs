using System.ComponentModel.DataAnnotations;

namespace cw5.Models;

public class Client
{
    [Key, Required] 
    public int IdClient  { get; set; }
    [Required, MaxLength(120)]  
    public string FirstName { get; set; }
    [Required, MaxLength(120)]  
    public string LastName { get; set; }
    [Required, MaxLength(120)]  
    public string Email { get; set; }
    [Required, MaxLength(120)]  
    public string Telephone { get; set; }
    [Required, MaxLength(120)]  
    public string Pesel { get; set; }
    
    public ICollection<Client_Trip> ClientTrips { get; set; }
}