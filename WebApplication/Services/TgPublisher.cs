using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;

namespace WebApplication.Services;

public class TgPublisher(ITelegramBotClient client, IConfiguration configuration) : IPublishHandler
{
    private readonly string _chatId = configuration["Tg:ChatId"];

    public async Task HandlePostPublish(string author, string text)
    {
        var message = $"Новая нужда\n\n{text}";
        if (author?.Trim().Length > 2)
        {
            message += $"\n—{author}";
        }

        await client.SendMessage(_chatId, message);
    }
}