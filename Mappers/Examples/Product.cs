namespace Mappers.Examples;

public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public decimal DiscountPrice { get; set; }
    public bool IsInStock { get; set; }

    public ProductDto ToProductDto()
    {
        return new ProductDto
        {
            Id = Id,
            Name = Name,
            Description = Description,
            Price = Price,
            DiscountPrice = Price - (Price * 0.12M),
            IsInStock = IsInStock
        };
    }

    public static implicit operator ProductDto(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            DiscountPrice = product.Price - (product.Price * 0.12M),
            IsInStock = product.IsInStock
        };
    }
}