using CRUD_API.Data;
using CRUD_API.Dto;
using CRUD_API.Interfaces;
using CRUD_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CRUD_API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _service;

    public EmployeesController(IEmployeeService service)
    {
        _service = service;
    }


    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var emp = await _service.GetByIdAsync(id);
        return emp == null ? NotFound() : Ok(emp);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateEmployeeDto dto)
    {
        var emp = new Employee
        {
            FullName = dto.FullName,
            Position = dto.Position,
            Salary = dto.Salary,
            HireDate = dto.HireDate.ToUniversalTime()
        };

        var created = await _service.AddAsync(emp);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateEmployeeDto dto)
    {
        var existing = await _service.GetByIdAsync(id);
        if (existing == null) return NotFound();

        existing.FullName = dto.FullName;
        existing.Position = dto.Position;
        existing.Salary = dto.Salary;
        existing.HireDate = dto.HireDate;

        await _service.UpdateAsync(existing);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await _service.GetByIdAsync(id);
        if (existing == null) return NotFound();

        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? name, [FromQuery] string? position)
    {
        var result = await _service.SearchAsync(name, position);
        return Ok(result);
    }
}
