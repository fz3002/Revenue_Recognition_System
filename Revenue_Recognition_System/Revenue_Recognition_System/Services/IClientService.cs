using Revenue_Recognition_System.DTO;

namespace Revenue_Recognition_System.Services;

public interface IClientService
{
    Task<ClientViewDTO> AddClientAsync(ClientDTO clientDto, CancellationToken cancellationToken);
    Task DeleteClientAsync(int id, CancellationToken cancellationToken);
    Task UpdateClientAsync(int id, ClientDTO clientDto, CancellationToken cancellationToken);
    Task<ClientViewDTO> GetClientAsync(int id, CancellationToken cancellationToken);
}