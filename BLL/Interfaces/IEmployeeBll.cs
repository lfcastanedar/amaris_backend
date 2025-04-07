using BLL.Model;
using BLL.Model.DTOs;
using BLL.Model.External;

namespace BLL.Interfaces;

public interface IEmployeeBll
{
    public Task<IEnumerable<EmployeesDto>?> GetEmployees();
    public Task<EmployeesDto?> GetEmployeeById(int id);
}