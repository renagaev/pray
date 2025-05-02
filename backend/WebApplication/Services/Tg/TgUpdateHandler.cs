using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace WebApplication.Services.Tg;

public class TgUpdateHandler(ILogger<TgUpdateHandler> logger, IServiceScopeFactory scopeFactory): IUpdateHandler
{
    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Type == UpdateType.MessageReaction)
        {
            using var scope = scopeFactory.CreateScope();
            var tgService = scope.ServiceProvider.GetRequiredService<TgReactionHandler>();
            await tgService.HandleReactionUpdate(update.MessageReaction);
        }
    }

    public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "error during tg update processing");
        return Task.CompletedTask;
    }
}