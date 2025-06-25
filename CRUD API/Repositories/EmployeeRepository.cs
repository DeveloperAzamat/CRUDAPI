using CRUD_API.Data;
using CRUD_API.Interfaces;
using CRUD_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_API.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _context;
    public EmployeeRepository(ApplicationDbContext context) => _context = context;

    public async Task<List<Employee>> GetAllAsync() => await _context.Employees.ToListAsync();
    public async Task<Employee?> GetByIdAsync(int id) => await _context.Employees.FindAsync(id);
    public async Task<Employee> AddAsync(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return employee;
    }
    public async Task UpdateAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee != null) _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
    }
    public async Task<List<Employee>> SearchAsync(string? name, string? position)
    {
        var query = _context.Employees.AsQueryable();
        if (!string.IsNullOrEmpty(name)) query = query.Where(e => e.FullName.Contains(name));
        if (!string.IsNullOrEmpty(position)) query = query.Where(e => e.Position == position);
        return await query.ToListAsync();
    }
}
