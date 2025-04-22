using Application.DTOs;
using Domain.Entities;

namespace Application.Mappings;

public static class TodoMappingExtensions
{
    public static TodoReadDto ToReadDto(this Todo todo) => new(todo.Id, todo.Name);
}