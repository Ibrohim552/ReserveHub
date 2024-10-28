using AutoMapper;
using ReserveHub.DTO_s;
using ReserveHub.Entities;

namespace ReserveHub;

public class Mapper:Profile
{
    public Mapper()
    {
        CreateMap<ClientReadInfo, Client>();
        CreateMap<Client, ClientReadInfo>();
        CreateMap<ClientUpdateInfo, Client>();
        CreateMap<BusinessOwner, BusinessOwnerReadInfo>();
        CreateMap<BusinessOwnerCreateInfo, BusinessOwner>();
        CreateMap<BusinessOwnerUpdateInfo, BusinessOwner>();
        CreateMap<BookingCreateInfo, Booking>();
    }
}