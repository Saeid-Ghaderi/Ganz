namespace Ganz.Domain.Shared
{
    public record class AddressInfoData
    {
        public string PostalCode { get; init; }
        public string Address { get; init; }
        public string Title { get; init; }
    }
}
