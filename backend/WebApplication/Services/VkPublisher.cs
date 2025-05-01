using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using VkNet;
using VkNet.Model;
using Post = WebApplication.Entities.Post;

namespace WebApplication.Services
{
    public class VkPublisher(IConfiguration configuration) : IPublishHandler
    {
        private readonly string _token = configuration["VK:Token"];
        private readonly VkApi _api = new();
        private readonly long _peerId = configuration.GetValue<long>("VK:PeerId");

        private async Task Authorize()
        {
            if (_api.IsAuthorized) return;
            await _api.AuthorizeAsync(new ApiAuthParams
            {
                AccessToken = _token
            });
        }

        public async Task HandlePostPublish(Post post)
        {
            await Authorize();
            var message = $"Новая нужда 🙏:\n{post.Text}";
            if (post.Author?.Trim().Length > 2)
            {
                message += $"\n—{post.Author}";
            }

            _api.Messages.Send(new MessagesSendParams
            {
                PeerId = _peerId,
                Message = message,
                RandomId = new Random().Next(99999999)
            });
        }
    }
}