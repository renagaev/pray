using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using WebApplication.Entities;

namespace WebApplication.Services.Tg;

public class TgPublisher(ITelegramBotClient client, IConfiguration configuration, AppDbContext dbContext) : IPublishHandler
{
    private readonly long _chatId = configuration.GetValue<long>("Tg:ChatId");
    public async Task HandlePostPublish(Post post)
    {
        var message = $"Новая нужда\n\n{post.Text}";
        if (post.Author?.Trim().Length > 2)
        {
            message += $"\n—{post.Author}";
        }

        var sentMessage = await client.SendMessage(_chatId, message);
        dbContext.Set<TelegramPost>().Add(new TelegramPost
        {
            Post = post,
            ChatId = sentMessage.Chat.Id,
            MessageId = sentMessage.Id
        });
    }


}