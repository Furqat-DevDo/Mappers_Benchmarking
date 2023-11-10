using AutoMapper;
using BenchmarkDotNet.Attributes;
using Mappers.Examples;
using Mapster;

namespace Mappers_Test.Tests;

[MemoryDiagnoser]
[RankColumn]
public class MappersBenchmark
{
    private List<Product> _products;
    private IMapper _mapper;
    
    [Params (10,50,100)]
    public int NumberOfElements { get; set; }
    
    [GlobalSetup]
    public void Init()
    {
        // AutoMapper Config
        var config = new MapperConfiguration(
            cfg => cfg.CreateMap<Product, ProductDto>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.Name}")
                )
                .ForMember(
                    dest => dest.Description,
                    opt => opt.MapFrom(src => $"{src.Description}")
                )
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => $"{src.Id}")
                )
                .ForMember(
                    dest => dest.Price,
                    opt => opt.MapFrom(src => $"{src.Price}")
                )
                .ForMember(
                    dest => dest.DiscountPrice,
                    opt => opt.MapFrom(src => $"{src.Price - (src.Price * 0.12M)}")
                )
                .ForMember(
                    dest => dest.IsInStock,
                    opt => opt.MapFrom(src => $"{src.IsInStock}")
                ));

        _mapper = config.CreateMapper();
        
        // Create  N number Products
        _products = Enumerable.Range(1, NumberOfElements)
            .Select(n => new Product
            {
                Id = Guid.NewGuid(),
                Name = $"Product Name number {n} .",
                Description = $"Product description number {n}",
                Price = 12000M,
                IsInStock = true,
                DiscountPrice = 12000 - (12000 * 0.12M)
            }).ToList();
    }
    
    [Benchmark]
    public void AutoMapper()
    {
        foreach (var product in _products)
        {
            var dto = _mapper.Map<ProductDto>(product);
        }
    }
    
    [Benchmark]
    public void Mapster()
    {
        foreach (var product in _products)
        {
            var dto = product.Adapt<ProductDto>();
        }
    }
    
    [Benchmark]
    public void ByHand()
    {
        foreach (var product in _products)
        {
           var dto = product.ToProductDto();
        }
    }
    
    [Benchmark]
    public void Implicitly()
    {
        foreach (var product in _products)
        {
            ProductDto dto = product;
        }
    }
    
    // [Benchmark]
    // public void Explicitly()
    // {
    //     foreach (var product in _products)
    //     {
    //         var dto = (ProductDto) product;
    //     }
    // }
}