using Newtonsoft.Json;

namespace BLL.Model.DTOs;

public class EmployeesDto
{
    public int Id { get; set; }
    public string EmployeeName { get; set; }
    public int EmployeeSalary { get; set; }
    public int EmployeeAnualSalary { get; set; }
    public int EmployeeAge { get; set; }
    public string ProfileImage { get; set; } = string.Empty;
}