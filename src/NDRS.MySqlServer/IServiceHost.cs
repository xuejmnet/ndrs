namespace NDRS.MySqlServer;

public interface IServiceHost
{
    
    Task StartAsync(CancellationToken cancellationToken=default);
    Task StopAsync(CancellationToken cancellationToken=default);
}