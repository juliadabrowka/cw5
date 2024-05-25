using cw5.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace cw5.Repositories;

public interface ITripRepository
{
    Task AssignClient(ClientDTO dto, CancellationToken cancellationToken);
    Task<Models.Trip> GetTrip(int idTrip, CancellationToken cancellationToken);
    Task<IList<Models.Trip>> GetTrips(CancellationToken cancellationToken);
}

public class TripRepository : ITripRepository
{
    private readonly IConfiguration _configuration;
    private readonly Context.Context _appContext;
    
    public TripRepository(IConfiguration configuration, Context.Context appContext)
    {
        _configuration = configuration;
        _appContext = appContext;
        Initialize();
    }

    private void Initialize()
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();
        using var createCommand = new SqlCommand();

        var createFile = new FileInfo("cw5_create.sql");

        createCommand.CommandText = $"IF NOT EXISTS (SELECT 1 FROM sys.schemas WHERE [name] = 'trip') " +
                                    $"BEGIN {createFile.OpenText().ReadToEnd()} END;";
        createCommand.Connection = connection;
        createCommand.ExecuteNonQuery();
    }

    public async Task AssignClient(ClientDTO dto, CancellationToken cancellationToken)
    {
        var clientTrip = new Client_Trip()
        {
            RegisteredAt = DateTime.Now,
            PaymentDate = dto.PaymentDate,
            IdClient = dto.Client.IdClient,
            IdTrip = dto.Trip.IdTrip
        };

        _appContext.Add(clientTrip);
        await _appContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Models.Trip> GetTrip(int idTrip, CancellationToken cancellationToken)
    {
        var trip = await _appContext
            .Trip
            .SingleAsync(trip => trip.IdTrip == idTrip, cancellationToken);

        return trip;
    }

    public async Task<IList<Models.Trip>> GetTrips(CancellationToken cancellationToken)
    {
        var trips = await _appContext
            .Trip
            .OrderByDescending(trip => trip.DateFrom)
            .ToListAsync(cancellationToken);

        return trips;
    }
}