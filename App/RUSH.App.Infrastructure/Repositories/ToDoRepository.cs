using RUSH.App.Domain.Model;
using RUSH.App.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RUSH.App.Infrastructure.Repositories
{
    public sealed class ToDoRepository : IToDosRepository
    {
        private readonly ToDoCatalogDbContext _sqlDbContext;
        private readonly ILogger<ToDoRepository> _logger;

        public ToDoRepository(ToDoCatalogDbContext sqlDbContext, ILogger<ToDoRepository> logger)
        {
            _sqlDbContext = sqlDbContext;
            _logger = logger;
        }

        public ToDo Add(ToDo toDo)
        {
            toDo.Id = Guid.NewGuid();

            return _sqlDbContext.ToDos.Add(toDo).Entity;
        }

        public async Task<ToDo> GetByIdAsync(Guid id)
        {
            var toDo = await _sqlDbContext.ToDos
                                    .Where(e => e.Id == id)
                                    .FirstOrDefaultAsync();

            return toDo;
        }

        public void Update(ToDo toDo)
        {
            _sqlDbContext.Update(_sqlDbContext);
        }

        public async Task<IReadOnlyList<ToDo>> ListAllAsync()
        {
            var toDo = await _sqlDbContext.ToDos
                             .ToListAsync();
            return toDo;
        }
    }
}
