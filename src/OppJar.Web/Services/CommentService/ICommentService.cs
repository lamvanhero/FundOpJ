using OppJar.Web.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace OppJar.Web.Services
{
    public interface ICommentService
    {
        Task<HttpResponseMessage> CreatePost(CreateEditCommentViewModel vm);
    }
}
