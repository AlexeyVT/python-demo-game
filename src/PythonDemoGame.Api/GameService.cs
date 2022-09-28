using PythonDemoGame.Api.WorldClasses;

namespace PythonDemoGame.Api;

public class GameService : IHostedService
{
    public static World? World = null;

    private bool _canceled;


    private async Task Worker()
    {
        while (!_canceled)
        {
            World?.Update();
            await Task.Delay(TimeSpan.FromMilliseconds(400));
        }
    }


    public Task StartAsync(CancellationToken cancellationToken)
    {
        _ = Task.Run(Worker, cancellationToken);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _canceled = true;
        return Task.CompletedTask;
    }
}