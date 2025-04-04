﻿namespace Bff.Controllers.Response.Product
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public required string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; } = 0;
    }
}
