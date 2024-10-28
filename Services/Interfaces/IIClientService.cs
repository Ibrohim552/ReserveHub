using ReserveHub.DTO_s;
using ReserveHub.Filters;
using ReserveHub.Responces;

namespace ReserveHub.Services;

public interface IClientService
{
    PaginationResponse<IEnumerable<ClientReadInfo>> GetClients(ClientFilter filter);
    ClientReadInfo GetClientById(int clientId);
    bool CreateClient(ClientReadInfo client);
    bool UpdateClient(ClientUpdateInfo client);
    bool DeleteClient(int clientId);
}