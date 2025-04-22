using Application.Contracts.Persistence;
using Application.DTOs;
using Application.Mappings;

namespace Application.Services;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _repository;

    public TodoService(ITodoRepository repository) => _repository = repository;

    public async Task<IEnumerable<TodoReadDto>> GetAllAsync()
    {
        var todos = await _repository.GetAllAsync();
        return todos.Select(t => t.ToReadDto());
    }
}