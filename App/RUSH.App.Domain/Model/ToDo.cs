using System;

namespace RUSH.App.Domain.Model
{
    public class ToDo : IEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
    }
}
