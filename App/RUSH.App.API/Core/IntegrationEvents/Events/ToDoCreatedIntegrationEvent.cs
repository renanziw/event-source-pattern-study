using RUSH.EventBus.Events;
using System;

namespace RUSH.App.API.Core.IntegrationEvents.Events
{
    public class ToDoCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid IdToDo { get; private set; }
        public string Status { get; private set; }

        public ToDoCreatedIntegrationEvent(Guid id, string status)
        {
            IdToDo = id;
            Status = status;
        }
    }
}
