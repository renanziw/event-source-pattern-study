using System;

namespace RUSH.App.Domain.Model
{
    public interface IEntity
    {
        public Guid Id { get; set; }
    }
}
