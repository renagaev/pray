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
    public async Task HandleReactionUpdate(MessageReactionCountUpdated reactionUpdated)
    {
        if (reactionUpdated.Chat.Id != _chatId)
        {
            return;
        }

        var prayReaction = reactionUpdated.Reactions
            .FirstOrDefault(x => (x.Type as ReactionTypeEmoji)?.Emoji == "🙏");
        if (prayReaction == null)
        {
            return;
        }

        var postMessage = await dbContext.Set<TelegramPost>()
            .FirstOrDefaultAsync(x => x.ChatId == _chatId && x.MessageId == reactionUpdated.MessageId);
        if (postMessage == null)
        {
            return;
        }

        if (postMessage.ReactionCount <= prayReaction.TotalCount)
        {
            return;
        }

        var increment = prayReaction.TotalCount - postMessage.ReactionCount;

        await dbContext.SaveChangesAsync();
        await postService.IncrementVotes(postMessage.PostId, VoteType.Standard, increment);
    }
}