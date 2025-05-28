namespace BusinessLogic.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetCategoriesAsync(CancellationToken cancellationToken = default);
    Task CreateCategoryAsync(string title, CancellationToken cancellationToken = default);
}
