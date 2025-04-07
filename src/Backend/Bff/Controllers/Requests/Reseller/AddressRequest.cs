namespace Bff.Controllers.Requests.Reseller
{
    /// <summary>
    /// DTO for a reseller's address information.
    /// </summary>
    public class AddressRequest
    {
        /// <summary>
        /// The postal code of the address.
        /// </summary>
        public required int PostalCode { get; set; }

        /// <summary>
        /// The country of the address.
        /// </summary>
        public required string Country { get; set; }

        /// <summary>
        /// The province of the address.
        /// </summary>
        public required string Province { get; set; }

        /// <summary>
        /// The city of the address.
        /// </summary>
        public required string City { get; set; }

        /// <summary>
        /// The street of the address.
        /// </summary>
        public required string Street { get; set; }

        /// <summary>
        /// The number of the address (optional).
        /// </summary>
        public int? Number { get; set; }
    }
}
