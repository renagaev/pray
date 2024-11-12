using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;

namespace WebApplication.Services
{
    public class PushService : IPublishHandler
    {
        private readonly FirebaseMessaging _messaging;

        public PushService(IConfiguration configuration)
        {
            var app = FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromJson(Regex.Unescape(configuration["FirebaseSecret"]))
            });
            _messaging = FirebaseMessaging.GetMessaging(app);
        }

        public async Task SubscribeToPosts(string deviceToken)
        {
            await FirebaseMessaging.DefaultInstance.SubscribeToTopicAsync(new[] { deviceToken }, "new_posts");
        }

        public async Task SendVote(string deviceToken)
        {
            if (string.IsNullOrEmpty(deviceToken))
                return;
            var message = CreateMessage("Жертвенник | За вас молятся!", "Кто-то помолился за вашу нужду");
            message.Token = deviceToken;
            try
            {
                await FirebaseMessaging.DefaultInstance.SendAsync(message);
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        private static Message CreateMessage(string title, string body) =>
            new()
            {
                Webpush = new WebpushConfig
                {
                    Notification = new WebpushNotification
                    {
                        Icon = "https://pray.russia-church.com/favicon.ico",
                        Title = title,
                        Body = body
                    },
                    FcmOptions = new WebpushFcmOptions
                    {
                        Link = "https://pray.russia-church.com/",
                    }
                }
            };


        public async Task HandlePostPublish(string author, string text)
        {
            var message = CreateMessage("Жертвенник | Новая нужда", text);
            message.Topic = "new_posts";
            await _messaging.SendAsync(message);
        }
    }
}