using System.Threading.Tasks;
using WebApplication.Entities;

namespace WebApplication.Services
{
    public interface IPublishHandler
    {
        Task HandlePostPublish(Post post);
    }
}