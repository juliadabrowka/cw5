using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cw5.Models;

public class Client_Trip
{
    [ForeignKey(nameof(Client)), Required] 
    public int IdClient { get; set; }
    public Client Client { get; set; }
    [ForeignKey(nameof(Trip)), Required] 
    public int IdTrip { get; set; }
    public Trip Trip { get; set; }
    [Required]
    public DateTime RegisteredAt { get; set; }
    public DateTime? PaymentDate { get; set; }
}