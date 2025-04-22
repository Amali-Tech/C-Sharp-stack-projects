using Domain.Entities;

namespace Application.Contracts.Persistence;

public interface ITodoRepository
{
    Task<IEnumerable<Todo>> GetAllAsync();
}