using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using WebApplication.Entities;

namespace WebApplication.Services.Tg;

public class TgPostUpdateService(ITelegramBotClient botClient, AppDbContext dbContext, IConfiguration configuration)
{
    private long AdminChatId => configuration.GetValue<long>("Tg:AdminChatId");

    public async Task NotifyNewPost(string text, string author)
    {
        await botClient.SendMessage(AdminChatId, $"Новый запрос:\n\n{text}\n\n - {author}");
    }

    public async Task MarkPostAsUnActual(int postId)
    {
        var post = await dbContext.Set<Post>()
            .FirstAsync();
        var tgPosts = await dbContext.Set<TelegramPost>()
            .Where(x => x.PostId == postId)
            .ToListAsync();
        
        var message = post.Text;
        if (post.Author?.Trim().Length > 2)
        {
            message += $"\n—{post.Author}";
        }

        message += "\n\n(неактуально)";
        
        foreach (var tgPost in tgPosts)
        {
            await botClient.EditMessageText(tgPost.ChatId, tgPost.MessageId, message);
        }
    }
}