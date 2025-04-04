namespace Challenge.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
