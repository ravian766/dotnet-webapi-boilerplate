using FSH.WebApi.Application.Catalog.Categories;

namespace FSH.WebApi.Host.Controllers.Catalog;

public class CategoriesController : VersionedApiController
{
    ////[HttpPost("search")]
    ////[MustHavePermission(FSHAction.Search, FSHResource.Categories)]
    ////[OpenApiOperation("Search brands using available filters.", "")]
    ////public Task<PaginationResponse<CategoryDto>> SearchAsync(SearchCategoriesRequest request)
    ////{
    ////    return Mediator.Send(request);
    ////}

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Categories)]
    [OpenApiOperation("Get Category details.", "")]
    public Task<CategoryDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetCategoryRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Categories)]
    [OpenApiOperation("Create a new Category.", "")]
    public Task<Guid> CreateAsync(CreateCategoryRequest request)
    {
        return Mediator.Send(request);
    }

    ////[HttpPut("{id:guid}")]
    ////[MustHavePermission(FSHAction.Update, FSHResource.Categories)]
    ////[OpenApiOperation("Update a brand.", "")]
    ////public async Task<ActionResult<Guid>> UpdateAsync(UpdateBrandRequest request, Guid id)
    ////{
    ////    return id != request.Id
    ////        ? BadRequest()
    ////        : Ok(await Mediator.Send(request));
    ////}

    ////[HttpDelete("{id:guid}")]
    ////[MustHavePermission(FSHAction.Delete, FSHResource.Categories)]
    ////[OpenApiOperation("Delete a brand.", "")]
    ////public Task<Guid> DeleteAsync(Guid id)
    ////{
    ////    return Mediator.Send(new DeleteBrandRequest(id));
    ////}

    ////[HttpPost("generate-random")]
    ////[MustHavePermission(FSHAction.Generate, FSHResource.Categories)]
    ////[OpenApiOperation("Generate a number of random brands.", "")]
    ////public Task<string> GenerateRandomAsync(GenerateRandomBrandRequest request)
    ////{
    ////    return Mediator.Send(request);
    ////}

    ////[HttpDelete("delete-random")]
    ////[MustHavePermission(FSHAction.Clean, FSHResource.Categories)]
    ////[OpenApiOperation("Delete the brands generated with the generate-random call.", "")]
    ////[ApiConventionMethod(typeof(FSHApiConventions), nameof(FSHApiConventions.Search))]
    ////public Task<string> DeleteRandomAsync()
    ////{
    ////    return Mediator.Send(new DeleteRandomBrandRequest());
    ////}
}