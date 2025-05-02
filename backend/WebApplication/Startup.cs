using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;
using WebApplication.Auth;
using WebApplication.Services;
using WebApplication.Services.Tg;

namespace WebApplication;

public class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAuthentication("scheme")
            .AddScheme<AdminAuthOptions, AdminAuthHandler>("scheme", options =>
            {
                options.Login = configuration["AdminAuth:Login"];
                options.Password = configuration["AdminAuth:Password"];
            })
            .AddScheme<TgAuthOptions, TgAuthHandler>("twa", options => { options.Token = configuration["Tg:Token"]; });

        services.AddDbContext<AppDbContext>();
        services.AddSingleton<ITelegramBotClient>(_ => new TelegramBotClient(configuration["Tg:Token"]));
        services.AddScoped<TgPostUpdateService>();
        services.AddScoped<IPublishHandler, TgPublisher>();
        services.AddScoped<TgReactionHandler>();
        services.AddScoped<PostService>();
        services.AddHostedService<TelegramHostedService>();
        services.AddSingleton<TgUpdateHandler>();
        services.AddControllers();
        services.AddSingleton<PushService>();
        services.AddScoped<IPublishHandler>(s => s.GetService<PushService>());
        services.AddResponseCompression();
    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseResponseCompression();
        app.UseDefaultFiles();
        app.UseStaticFiles(new StaticFileOptions
        {
            OnPrepareResponse = ctx =>
            {
                if (ctx.File.Name == "index.html")
                {
                    ctx.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
                    ctx.Context.Response.Headers.Add("Expires", "-1");
                }
                else
                {
                    ctx.Context.Response.Headers["Cache-Control"] = "public, max-age=604800";
                }
            }
        });

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseCors(options =>
        {
            options.SetIsOriginAllowed(_ => true);
            options.AllowAnyMethod();
            options.AllowAnyHeader();
            options.AllowCredentials();
        });

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}