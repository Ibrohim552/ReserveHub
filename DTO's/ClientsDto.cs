namespace ReserveHub.DTO_s;

public readonly record struct ClientReadInfo
    (
        string Name,
        string Surname,
        string Email,
        string PhoneNumber
    );
    public readonly record struct ClientCreateInfo
        (
            string Name,
            string Surname,
            string Email,
            string PhoneNumber,
            Gender Gender
        );
        public readonly record struct ClientUpdateInfo
            (
                int Id,
                string Name,
                string Surname,
                string Email,
                string PhoneNumber,
                Gender Gender
            );