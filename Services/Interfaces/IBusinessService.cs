using ReserveHub.DTO_s;
using ReserveHub.Filters;
using ReserveHub.Responces;

namespace ReserveHub.Services;

public interface IBusinessOwnerService
{
    PaginationResponse<IEnumerable<BusinessOwnerReadInfo>> GetBusinessOwners(BusinessOwnerFilter filter);
    BusinessOwnerReadInfo GetBusinessOwnerById(int ownerId);
    bool CreateBusinessOwner(BusinessOwnerCreateInfo owner);
    bool UpdateBusinessOwner(BusinessOwnerUpdateInfo owner);
    bool DeleteBusinessOwner(int ownerId);
}