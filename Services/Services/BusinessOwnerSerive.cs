using AutoMapper;
using AutoMapper.QueryableExtensions;
using ReserveHub.DTO_s;
using ReserveHub.Entities;
using ReserveHub.Filters;
using ReserveHub.Responces;

namespace ReserveHub.Services
{
    public class BusinessOwnerService : IBusinessOwnerService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public BusinessOwnerService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public PaginationResponse<IEnumerable<BusinessOwnerReadInfo>> GetBusinessOwners(BusinessOwnerFilter filter)
        {
            IQueryable<BusinessOwner> businessOwners = _context.BusinessOwner;
            if (filter.Name != null)
                businessOwners = businessOwners.Where(x => x.Name.ToLower() == filter.Name.ToLower());
            if (filter.Surname!= null)
                businessOwners = businessOwners.Where(x => x.Surname.ToLower() == filter.Surname.ToLower());
            
            int totalRecords = businessOwners.Count();

            var result = businessOwners
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ProjectTo<BusinessOwnerReadInfo>(_mapper.ConfigurationProvider);

            return PaginationResponse<IEnumerable<BusinessOwnerReadInfo>>.Create(filter.PageNumber,
                filter.PageSize, totalRecords, result);
        }

        public BusinessOwnerReadInfo GetBusinessOwnerById(int businessOwnerId)
        {
            return _context.BusinessOwner
                .Where(b => b.Id == businessOwnerId)
                .ProjectTo<BusinessOwnerReadInfo>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
        }

        public bool CreateBusinessOwner(BusinessOwnerCreateInfo businessOwner)
        {
            if (_context.BusinessOwner.Any(b => b.Name == businessOwner.Name && b.Surname == businessOwner.Surname))
                return false;
            var newBusinessOwner = _mapper.Map<BusinessOwner>(businessOwner);
            _context.BusinessOwner.Add(newBusinessOwner);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateBusinessOwner(BusinessOwnerUpdateInfo businessOwner)
        {
            var existingBusinessOwner = _context.BusinessOwner.FirstOrDefault(b => b.Id == businessOwner.Id);
            if (existingBusinessOwner == null) return false;

            _mapper.Map(businessOwner, existingBusinessOwner);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteBusinessOwner(int businessOwnerId)
        {
            if (!_context.BusinessOwner.Any(b => b.Id == businessOwnerId)) return false;
            var businessOwner = _context.BusinessOwner.FirstOrDefault(b => b.Id == businessOwnerId);
            if (businessOwner == null) return false;

            _context.BusinessOwner.Remove(businessOwner);
            _context.SaveChanges();
            return true;
        }
    }
}
