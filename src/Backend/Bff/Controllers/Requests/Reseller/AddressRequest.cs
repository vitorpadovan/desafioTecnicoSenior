namespace Bff.Controllers.Requests.Reseller
{
    public class AddressRequest
    {
        public required int PostalCode { get; set; }
        public required string Country { get; set; }
        public required string Province { get; set; }
        public required string City { get; set; }
        public required string Street { get; set; }
        public int? Number { get; set; }
    }
}
