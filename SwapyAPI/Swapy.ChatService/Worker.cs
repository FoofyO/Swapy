using Microsoft.AspNetCore.SignalR;
using Swapy.ChatService.Hubs;

namespace Swapy.ChatService
{
    //public class Worker : BackgroundService
    //{
    //    private readonly ILogger<Worker> _logger;
    //    private readonly IServiceProvider _serviceProvider;

    //    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
    //    {
    //        _logger = logger;
    //        _serviceProvider = serviceProvider;
    //    }

    //    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    //    {
    //        var hubContext = _serviceProvider.GetRequiredService<IHubContext<ChatHub>>();
    //        await hubContext.Clients.All.SendAsync("ReceiveMessage", "System", "Worker started");

    //        while (!stoppingToken.IsCancellationRequested)
    //        {
    //            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

    //            await Task.Delay(1000, stoppingToken);
    //        }

    //        await hubContext.Clients.All.SendAsync("ReceiveMessage", "System", "Worker stopped");
    //    }
    //}

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHubContext<ChatHub> _chatHubContext;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public Worker(ILogger<Worker> logger, IHubContext<ChatHub> chatHubContext, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _chatHubContext = chatHubContext;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var recipientUserId = "recipientUserId"; // Здесь нужно указать получателя
                    var messageText = "Your message"; // Здесь текст сообщения

                    await _chatHubContext.Clients.User(recipientUserId).SendAsync("ReceiveMessage", "System", messageText);
                }

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}