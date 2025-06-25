using CRUD_API.Interfaces;
using CRUD_API.Models;

namespace CRUD_API.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repo;
    public EmployeeService(IEmployeeRepository repo) => _repo = repo;

    public Task<List<Employee>> GetAllAsync() => _repo.GetAllAsync();
    public Task<Employee?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
    public Task<Employee> AddAsync(Employee employee) => _repo.AddAsync(employee);
    public Task UpdateAsync(Employee employee) => _repo.UpdateAsync(employee);
    public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
    public Task<List<Employee>> SearchAsync(string? name, string? position) => _repo.SearchAsync(name, position);
}
