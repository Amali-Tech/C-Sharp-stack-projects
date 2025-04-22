using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class TodosController : BaseController
{
    private readonly ITodoService _service;

    public TodosController(ITodoService service) => _service = service;


    [HttpGet]
    public async Task<ActionResult<ApiSuccessResponse<IEnumerable<TodoReadDto>>>> GetAll()
    {
        var todos = await _service.GetAllAsync();

        return Ok(new ApiSuccessResponse<IEnumerable<TodoReadDto>>(200, "Todos retrieved successfully", todos));
    }
}