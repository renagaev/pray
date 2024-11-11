using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication.Entities;

namespace WebApplication.Services
{
    public enum VoteType
    {
        Standard,
        SmallGroup,
        LargeGroup
    }

    public class PostService(
        AppDbContext context,
        PushService pushService,
        IEnumerable<IPublishHandler> publishHandlers)
    {
        private readonly DbSet<Post> _set = context.Set<Post>();

        public async Task IncrementVotes(int id, VoteType voteType)
        {
            var post = await _set.FirstOrDefaultAsync(x => x.Id == id);
            if (post == null) return;
            switch (voteType)
            {
                case VoteType.Standard:
                    post.Votes++;
                    break;
                case VoteType.SmallGroup:
                    post.SmallGroupVotes++;
                    break;
                case VoteType.LargeGroup:
                    post.LargeGroupVotes++;
                    break;
            }

            await context.SaveChangesAsync();
            if (!string.IsNullOrEmpty(post.DeviceToken))
                await pushService.SendVote(post.DeviceToken);
        }

        public async Task Create(string text, string author, string token)
        {
            var post = Post.Create(text, author);
            if (!string.IsNullOrEmpty(token))
            {
                post.DeviceToken = token;
            }

            _set.Add(post);
            await context.SaveChangesAsync();
        }

        public async Task Update(int id, PostEditModel model)
        {
            var entity = await _set.FirstOrDefaultAsync(x => x.Id == id);
            entity.Hidden = model.Hidden;
            entity.Author = model.Author;
            entity.Text = model.Text;
            var published = model.Published && !entity.Published;

            if (published) entity.Publish();
            await context.SaveChangesAsync();
            if (published)
            {
                foreach (var publishHandler in publishHandlers)
                {
                    await publishHandler.HandlePostPublish(entity.Author, entity.Text);
                }
            }
        }

        public async Task<Post[]> GetForUsers()
        {
            return await _set.Where(x => x.Published && !x.Hidden).OrderByDescending(x => x.PublishDate).ToArrayAsync();
        }

        public async Task<Post[]> GetForAdmin()
        {
            return await _set.OrderByDescending(x => x.PublishDate).ToArrayAsync();
        }
    }
}