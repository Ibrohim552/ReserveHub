using AutoMapper;
using AutoMapper.QueryableExtensions;
using ReserveHub.DTO_s;
using ReserveHub.Entities;
using ReserveHub.Filters;
using ReserveHub.Responces;

namespace ReserveHub.Services;

public class ClientService:IClientService
{
    private readonly DataContext _clientContext;
    private readonly IMapper _mapper;

    public ClientService(DataContext clientContext, IMapper mapper)
    {
        _clientContext = clientContext;
        _mapper = mapper;
    }

    public PaginationResponse<IEnumerable<ClientReadInfo>> GetClients(ClientFilter filter)
    {
        IQueryable<Client> clients = _clientContext.Client;
        if (filter.Name != null)
            clients = clients.Where(x => x.Name.ToLower() == filter.Name.ToLower());
        if (filter.Surname != null)
            clients = clients.Where(x => x.Surname.ToLower() == filter.Surname.ToLower());
        int totalRecords = clients.Count();
        IQueryable<ClientReadInfo> res = clients
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ProjectTo<ClientReadInfo>(_mapper.ConfigurationProvider);
        return PaginationResponse<IEnumerable<ClientReadInfo>>.Create(filter.PageNumber,
            filter.PageSize, totalRecords, res);
    }

    public ClientReadInfo GetClientById(int clientId)
    {
        return _clientContext.Client.Where(x => x.Id == clientId)
            .ProjectTo<ClientReadInfo>(_mapper.ConfigurationProvider)
            .FirstOrDefault();
    }

    public bool CreateClient(ClientReadInfo client)
    {
        bool existClient = _clientContext.Client.
            Any(x => x.Name.ToLower() == client.Name.ToLower() 
                     && x.Surname.ToLower()==x.Surname.ToLower()
                     && !x.IsDeleted==false);
        if (existClient)
            return false;
        _clientContext.Client.Add(_mapper.Map<Client>(client));
        _clientContext.SaveChanges();
        return true;
    }

    public bool UpdateClient(ClientUpdateInfo client)
    {
        Client? clientEntity = _clientContext.Client.
            FirstOrDefault(x=>x.Id==client.Id && !x.IsDeleted==false);
        if (clientEntity == null)
            return false;
        _mapper.Map(client, clientEntity);
        _clientContext.SaveChanges();
        return true;
    }

    public bool DeleteClient(int clientId)
    {
        Client? clientEntity = _clientContext.Client.
            FirstOrDefault(x=>x.Id==clientId &&!x.IsDeleted==false);
        if (clientEntity is null)
            return false;
        clientEntity.IsDeleted = true;
        clientEntity.DeletedAt = DateTime.UtcNow;
        _clientContext.SaveChanges();
        return true;
    }
}