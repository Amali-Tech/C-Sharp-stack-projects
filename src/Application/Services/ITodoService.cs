using Application.DTOs;

namespace Application.Services;

public interface ITodoService
{
    Task<IEnumerable<TodoReadDto>> GetAllAsync();
}