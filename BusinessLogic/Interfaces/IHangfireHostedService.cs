namespace BusinessLogic.Interfaces;

public interface IHangfireHostedService
{
    void ScheduleHangfireTasks(CancellationToken cancellationToken);
}
