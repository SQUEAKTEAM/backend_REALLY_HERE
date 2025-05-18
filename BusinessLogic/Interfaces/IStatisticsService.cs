
namespace BusinessLogic.Interfaces;
public interface IStatisticsService
{
    Task<IEnumerable<StatisticsDto>> GetStatisticsAsync(CancellationToken cancellationToken = default);
}

