using BLL.Interfaces;
using BLL.Model;
using BLL.Model.DTOs;
using BLL.Model.External;
using DAL.External.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BLL;

public class EmployeeBll: IEmployeeBll
{
    
    private readonly IExternalRequestDal _externalRequestDal;
    private readonly IConfiguration _configuration;

    public EmployeeBll(IExternalRequestDal externalRequestDal, IConfiguration configuration)
    {
        _externalRequestDal = externalRequestDal;
        _configuration = configuration;
        
    }

    public async Task<IEnumerable<EmployeesDto>?> GetEmployees()
    {
        var externalUrl = ExternalUrl();
        var response = await _externalRequestDal.GetRequest<IEnumerable<EmployeesExternal>?>(externalUrl.Url, $"{externalUrl.APIEndpoint}/employees");
        
        return response.Data?.Select(x => new EmployeesDto
        {
            Id = x.Id,
            EmployeeName = x.EmployeeName,
            EmployeeSalary = x.EmployeeSalary,
            EmployeeAge = x.EmployeeAge,
            ProfileImage = x.ProfileImage,
            EmployeeAnualSalary = x.EmployeeSalary * 12
        });
    }

    public async Task<EmployeesDto?> GetEmployeeById(int id)
    {
        var externalUrl = ExternalUrl();
        var response = await _externalRequestDal.GetRequest<EmployeesExternal?>(externalUrl.Url, $"{externalUrl.APIEndpoint}/employee/{id}");

        if (response.Data is null)
        {
            return null;
        }

        return new EmployeesDto
        {
            Id = response.Data.Id,
            EmployeeName = response.Data.EmployeeName,
            EmployeeSalary = response.Data.EmployeeSalary,
            EmployeeAge = response.Data.EmployeeAge,
            ProfileImage = response.Data.ProfileImage,
            EmployeeAnualSalary = response.Data.EmployeeSalary * 12
        };
    }

    public ExternalUrlDto ExternalUrl()
    {
        IConfigurationSection externalUrl = _configuration.GetSection("ExternalUrl");

        return new ExternalUrlDto
        {
            Url = externalUrl.GetSection("Url").Value ?? "",
            APIEndpoint = externalUrl.GetSection("APIEndpoint").Value?? ""
        };
    }
}