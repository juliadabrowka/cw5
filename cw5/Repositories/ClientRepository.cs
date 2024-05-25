using cw5.Models;
using Microsoft.EntityFrameworkCore;

namespace cw5.Repositories;

public interface IClientRepository
{
    Task AddClient(Client client, CancellationToken cancellationToken);
    Task<Client> GetClient(int idClient, CancellationToken cancellationToken);
    Task DeleteClient(Client client, CancellationToken cancellationToken);
}

public class ClientRepository(Context.Context context) : IClientRepository
{
    public async Task AddClient(Client client, CancellationToken cancellationToken)
    {
        var isAlreadyAdded = await context.Client.AnyAsync(existingClient => existingClient.Pesel == client.Pesel, cancellationToken);
        if (isAlreadyAdded)
        {
            return;
        }

        context.Client.Add(client);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Client> GetClient(int idClient, CancellationToken cancellationToken)
    {
        var client = await context.Client.SingleAsync(client => client.IdClient == idClient, cancellationToken);

        return client;
    }

    public async Task DeleteClient(Client client, CancellationToken cancellationToken)
    {
        context.Client.Remove(client);

        await context.SaveChangesAsync(cancellationToken);
    }
}