
using FSH.WebApi.Application.Catalog.Brands;
using FSH.WebApi.Domain.Common.Events;
namespace FSH.WebApi.Application.Catalog.Categories;


public class UpdateCategoryRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}

public class UpdateCategoryRequestValidator : CustomValidator<UpdateCategoryRequest>
{
    public UpdateCategoryRequestValidator(IRepository<Category> repository, IStringLocalizer<UpdateCategoryRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (category, name, ct) =>
                    await repository.FirstOrDefaultAsync(new CategoryByNameSpec(name), ct)
                        is not Category existingCategory || existingCategory.Id == category.Id)
                .WithMessage((_, name) => T["Category {0} already Exists.", name]);
}

public class UpdateCategoryRequestHandler : IRequestHandler<UpdateCategoryRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Category> _repository;
    private readonly IStringLocalizer _t;

    public UpdateCategoryRequestHandler(IRepositoryWithEvents<Category> repository, IStringLocalizer<UpdateCategoryRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity
        ?? throw new NotFoundException(_t["Category {0} Not Found.", request.Id]);

        entity.Update(request.Name, request.Description);

        await _repository.UpdateAsync(entity, cancellationToken);

        return request.Id;
    }
}

