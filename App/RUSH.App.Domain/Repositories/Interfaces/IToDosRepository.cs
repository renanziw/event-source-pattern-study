using RUSH.App.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RUSH.App.Domain.Repositories.Interfaces
{
    public interface IToDosRepository
    {
        Task<ToDo> GetByIdAsync(Guid id);
        Task<IReadOnlyList<ToDo>> ListAllAsync();
        ToDo Add(ToDo toDo);
        void Update(ToDo toDo);
    }
}
