namespace ReserveHub.DTO_s;

public readonly record struct BusinessOwnerReadInfo
    (
         int Id,
         string Name,
         string Surname,
         string Email,
         string PhoneNumber,
         string Address,
         string BisnessType,
         int Expirience
        
        );
        
public readonly record struct BusinessOwnerUpdateInfo
    (
        int Id,
        string Name,
        string Surname,
        string Email,
        string PhoneNumber
        );
        
public readonly record struct BusinessOwnerCreateInfo 
(
    string Name,
    string Surname,
    string Email,
    string PhoneNumber,
    string Address,
    string BisnessType,
    int Expirience
);