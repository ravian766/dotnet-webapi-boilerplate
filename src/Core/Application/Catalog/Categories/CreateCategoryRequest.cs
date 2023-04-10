namespace FSH.WebApi.Application.Catalog.Categories;

public class CreateCategoryRequest : IRequest<Guid>
{

    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}

public class CategoryByNameSpec : Specification<Category>, ISingleResultSpecification
{
    public CategoryByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}

public class CreateCategoryRequestValidator : CustomValidator<CreateCategoryRequest>
{
    public CreateCategoryRequestValidator(IReadRepository<Category> repository, IStringLocalizer<CreateCategoryRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.GetBySpecAsync(new CategoryByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["Category {0} already Exists.", name]);
}

public class CreateCategoryRequestHandler : IRequestHandler<CreateCategoryRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Category> _repository;

    public CreateCategoryRequestHandler(IRepositoryWithEvents<Category> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var entity = new Category(request.Name, request.Description);

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}
