using Microsoft.AspNetCore.Http;
using OppJar.Web.Proxies;
using System.Threading.Tasks;
using OppJar.Domain.Enum;
using OppJar.Dto;
using System.Net.Http;

namespace OppJar.Web.Services
{
    public class EventService : ServiceBase, IEventService
    {
        private const string GET_EVENT_DETAIL_BY_USER_SLUG = "/api/event/{0}";
        private const string UPDATE_EVENT = "/api/event/{0}";
        private const string CHANGE_EVENT_STATUS = "/api/event/{0}/change-status/{1}";

        public EventService(IHttpContextAccessor httpContextAccessor, OppJarProxy oppJarProxy) : base(httpContextAccessor, oppJarProxy)
        {
        }

        #region Event
        public async Task<HttpResponseMessage> UpdateEventAsync(AddEditEventDto dto, string id)
        {
            return await _oppJarProxy.PutAsJsonAsync(string.Format(UPDATE_EVENT, id), dto);
        }

        public async Task<HttpResponseMessage> ChangeEventStatusAsync(EventStatus status, string id)
        {
            return await _oppJarProxy.PutAsJsonAsync(string.Format(CHANGE_EVENT_STATUS, id, status), null);
        }

        public async Task<HttpResponseMessage> GetEventDetailByUserSlugAsync(string slug)
        {
            return await _oppJarProxy.GetAsync(string.Format(GET_EVENT_DETAIL_BY_USER_SLUG, slug));
        }
        #endregion
    }
}
