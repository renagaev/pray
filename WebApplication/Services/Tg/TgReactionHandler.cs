using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Telegram.Bot.Types;
using WebApplication.Entities;

namespace WebApplication.Services.Tg;

public class TgReactionHandler(PostService postService, AppDbContext dbContext, IConfiguration configuration)
{
    private readonly long _chatId = configuration.GetValue<long>("Tg:ChatId");
    public async Task HandleReactionUpdate(MessageReactionUpdated reactionUpdated)
    {
        if (reactionUpdated.Chat.Id != _chatId || reactionUpdated.User == null)
        {
            return;
        }

        if (reactionUpdated.NewReaction.OfType<ReactionTypeEmoji>().All(x => x.Emoji != "🙏"))
        {
            return;
        }
        

        var postMessage = await dbContext.Set<TelegramPost>()
            .FirstOrDefaultAsync(x => x.ChatId == _chatId && x.MessageId == reactionUpdated.MessageId);

        if (postMessage == null)
        {
            return;
        }

        var reactionHandled = await dbContext.Set<TelegramVote>()
            .AnyAsync(x => x.TelegramPostId == postMessage.PostId && x.UserId == reactionUpdated.User.Id);
        if (reactionHandled)
        {
            return;
        }

        dbContext.Set<TelegramVote>().Add(new TelegramVote
        {
            TelegramPostId = postMessage.Id,
            UserId = reactionUpdated.User.Id
        });

        await dbContext.SaveChangesAsync();
        await postService.IncrementVotes(postMessage.PostId, VoteType.Standard);
    }
}