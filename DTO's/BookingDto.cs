namespace ReserveHub.DTO_s;

public readonly record struct BookingCreateInfo
(
   int ClientId,
   int BusinessOwnerId,
   DateTime BookingDate
        );
        public readonly record struct BookingUpdateInfo
            (
                int Id,
                int ClientId,
                int BusinessOwnerId,
                DateTime BookingDate
                );
                public readonly record struct BookingReadInfo
                    (
                        int Id,
                        string ClientName,
                        string BusinessOwnerName,
                        DateTime BookingDate,
                        string Status
                    );