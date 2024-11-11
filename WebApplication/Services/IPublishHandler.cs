using System.Threading.Tasks;

namespace WebApplication.Services
{
    public interface IPublishHandler
    {
        Task HandlePostPublish(string author, string text);
    }
}