namespace Mappers.Examples;

public class ProductDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public decimal DiscountPrice { get; set; }
    public bool IsInStock { get; set; }

    public static ProductDto FromProduct(Product product)
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
    
    // public static explicit operator ProductDto(Product product)
    // {
    //     return new ProductDto
    //     {
    //         Id = product.Id,
    //         Name = product.Name,
    //         Description = product.Description,
    //         Price = product.Price,
    //         DiscountPrice = product.Price - (product.Price * 0.12M),
    //         IsInStock = product.IsInStock
    //     };
    // }
}