using RUSH.App.API.Core.IntegrationEvents.Events;
using RUSH.App.Domain.Model;
using RUSH.App.Infrastructure.Repositories;
using RUSH.App.Infrastructure.Services.Integration.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RUSH.App.API.Controllers
{
    [ApiController]
    [Route("ToDo")]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoCatalogDbContext _toDoCatalogDbContext;
        private readonly IIntegrationEventService _integrationEventService;

        public ToDoController(ToDoCatalogDbContext toDoCatalogDbContext,
                                     IIntegrationEventService integrationEventService)
        {
            _toDoCatalogDbContext = toDoCatalogDbContext;
            _integrationEventService = integrationEventService;
        }

        /// <summary>
        /// Gets list with available ToDos
        /// </summary>
        [ProducesResponseType(typeof(IReadOnlyList<ToDo>), (int)HttpStatusCode.OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllToDosAsync()
        {
            var toDoList = await _toDoCatalogDbContext.ToDos.ToListAsync();
            return Ok(toDoList);
        }

        /// <summary>
        /// Gets specific ToDo by id
        /// </summary>
        [ProducesResponseType(typeof(ToDo), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetToDoAsync(Guid id)
        {
            var toDo = await _toDoCatalogDbContext.ToDos.SingleOrDefaultAsync(i => i.Id == id);
            if (toDo == null)
            {
                return NotFound(new { Message = $"TODO with id {id} not found." });
            }
            return Ok(toDo);
        }

        /// <summary>
        /// Add new ToDo with status ToDo
        /// </summary>
        [ProducesResponseType(typeof(ToDo), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost("toDo")]
        public async Task<IActionResult> AddToDoAsync(string toDo)
        {
            var toDoToInsert = new ToDo
            {
                Title = toDo,
                Status = "TODO"
            };

            var addedToDo = _toDoCatalogDbContext.Add(toDoToInsert);
            var pricePerDayChangedEvent = new ToDoCreatedIntegrationEvent(addedToDo.Entity.Id, addedToDo.Entity.Status);

            await _integrationEventService.AddAndSaveEventAsync(pricePerDayChangedEvent);
            await _toDoCatalogDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetToDoAsync), new { id = addedToDo.Entity.Id });
        }

        /// <summary>
        /// Start a existing ToDo. "Mark it as Doing"
        /// </summary>
        [ProducesResponseType(typeof(ToDo), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut("{id}/start")]
        public async Task<IActionResult> StartToDoAsync(Guid id)
        {
            var toDo = await _toDoCatalogDbContext.ToDos.SingleOrDefaultAsync(i => i.Id == id);
            if (toDo == null)
            {
                return NotFound(new { Message = $"TODO with id {id} not found." });
            }

            toDo.Status = "Doing";

            var updatedToDo = _toDoCatalogDbContext.Update(toDo);

            var pricePerDayChangedEvent = new ToDoStartedIntegrationEvent(updatedToDo.Entity.Id, updatedToDo.Entity.Status);

            await _integrationEventService.AddAndSaveEventAsync(pricePerDayChangedEvent);
            await _toDoCatalogDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetToDoAsync), new { id = updatedToDo.Entity.Id });
        }

        /// <summary>
        /// Finish a existing ToDo. "Mark it as Done"
        /// </summary>
        [ProducesResponseType(typeof(ToDo), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut("{id}/finish")]
        public async Task<IActionResult> FinishToDoAsync(Guid id)
        {
            var toDo = await _toDoCatalogDbContext.ToDos.SingleOrDefaultAsync(i => i.Id == id);
            if (toDo == null)
            {
                return NotFound(new { Message = $"TODO with id {id} not found." });
            }

            toDo.Status = "Done";

            var updatedToDo = _toDoCatalogDbContext.Update(toDo);

            var pricePerDayChangedEvent = new ToDoFinishedIntegrationEvent(updatedToDo.Entity.Id, updatedToDo.Entity.Status);

            await _integrationEventService.AddAndSaveEventAsync(pricePerDayChangedEvent);
            await _toDoCatalogDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetToDoAsync), new { id = updatedToDo.Entity.Id });
        }

    }
}
