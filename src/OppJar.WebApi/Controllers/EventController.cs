using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OppJar.Core.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using OppJar.Dto;

namespace OppJar.WebApi.Controllers
{
    public class EventController : BaseApi
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet("{userSlug}")]
        [ProducesResponseType(typeof(IEnumerable<EventDto>), 200)]
        [AllowAnonymous]
        public async Task<IActionResult> GetEventByUserSlug(string userSlug)
        {
            return Ok(await _eventService.GetEventByUserSlugAsync(userSlug));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EventDto), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update([FromBody] AddEditEventDto dto, string id)
        {
            var result = await _eventService.EditEventAsync(id, dto);

            return Ok(result);
        }
    }
}
