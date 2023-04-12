using FSH.WebApi.Application.Catalog.Brands;
using FSH.WebApi.Application.Catalog.Categories;

namespace FSH.WebApi.Application.Catalog.Products;

public class ProductDetailsDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Rate { get; set; }
    public string? ImagePath { get; set; }
    public BrandDto Brand { get; set; } = default!;
    public CategoryDto Category { get; set; } = default!;
}