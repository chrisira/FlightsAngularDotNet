using System.ComponentModel;

namespace FlightsAngularNet.DTOS
{
    public record FlightSearchParameters
    (
        [DefaultValue("12/05/2023 10:30:00 AM")]
        DateTime? FromDate,

        [DefaultValue("13/05/2023 10:30:00 AM")]
        DateTime? ToDate,

        [DefaultValue ("Los Angeles")]
        string? From,

        [DefaultValue ("Berlin")]
        string? Destination,

        [DefaultValue(1)]
        int? NumberOfPassengers

        );
}
