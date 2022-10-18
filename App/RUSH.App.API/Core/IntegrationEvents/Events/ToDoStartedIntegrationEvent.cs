using RUSH.EventBus.Events;
using System;

namespace RUSH.App.API.Core.IntegrationEvents.Events
{
    public class ToDoStartedIntegrationEvent : IntegrationEvent
    {
        public Guid IdToDo { get; private set; }
        public string Status { get; private set; }

        public ToDoStartedIntegrationEvent(Guid id, string status)
        {
            IdToDo = id;
            Status = status;
        }
    }
}
