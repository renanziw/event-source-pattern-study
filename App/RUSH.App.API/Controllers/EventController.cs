using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RUSH.App.Infrastructure.Services.Integration.Interfaces;
using RUSH.EventLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RUSH.App.API.Controllers
{
    [Route("Event")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IIntegrationEventService _integrationEventService;
        private readonly EventLogContext eventLogContext;

        public EventController(IIntegrationEventService integrationEventService, EventLogContext _eventLogContext)
        {
            _integrationEventService = integrationEventService;
            eventLogContext = _eventLogContext;
        }

        /// <summary>
        /// Gets list with available events
        /// </summary>
        [ProducesResponseType(typeof(IReadOnlyList<object>), (int)HttpStatusCode.OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllEventsAsync()
        {
            var list = await eventLogContext.IntegrationEventLog.ToListAsync();

            return Ok(list);
        }

    }
}
