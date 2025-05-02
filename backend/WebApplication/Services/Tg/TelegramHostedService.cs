using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace WebApplication.Services.Tg;

public class TelegramHostedService(ITelegramBotClient client, TgUpdateHandler updateHandler) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var receiverOptions = new ReceiverOptions { DropPendingUpdates = true, AllowedUpdates = [UpdateType.MessageReaction] };
        await client.ReceiveAsync(updateHandler, receiverOptions, stoppingToken);
    }
}