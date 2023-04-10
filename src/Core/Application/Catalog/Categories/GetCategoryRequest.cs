namespace FSH.WebApi.Application.Catalog.Categories;
public class GetCategoryRequest : IRequest<CategoryDto>
{
    public Guid Id { get; set; }

    public GetCategoryRequest(Guid id) => Id = id;
}

public class GetCategoryRequestHandler : IRequestHandler<GetCategoryRequest, CategoryDto>
{
    private readonly IRepository<Category> _repository;
    private readonly IStringLocalizer _t;

    public GetCategoryRequestHandler(IRepository<Category> repository, IStringLocalizer<GetCategoryRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<CategoryDto> Handle(GetCategoryRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<Category,   CategoryDto>)new CategoryByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Category {0} Not Found.", request.Id]);
}

public class CategoryByIdSpec : Specification<Category, CategoryDto>, ISingleResultSpecification
{
    public CategoryByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}


