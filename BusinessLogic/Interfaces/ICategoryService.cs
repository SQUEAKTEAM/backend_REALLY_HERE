namespace BusinessLogic.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<string>> GetCategoriesTitleAsync(CancellationToken cancellationToken = default);
}
