using BLL;
using BLL.Interfaces;
using BLL.Model.External;
using DAL.External.Interfaces;
using DAL.Model;
using Microsoft.Extensions.Configuration;
using Moq;

namespace UnitTests.BLL;

public class EmployeeBllTests
{
    private readonly Mock<IExternalRequestDal> _externalRequestDal;
    private readonly Mock<IConfiguration> _configuration;
    private readonly IEmployeeBll _employeeBll;
    
    public EmployeeBllTests()
    {
        _configuration = new Mock<IConfiguration>();
        _externalRequestDal = new Mock<IExternalRequestDal>();
        
        var externalUrlSectionMock = new Mock<IConfigurationSection>();
        var apiEndpointSectionMock = new Mock<IConfigurationSection>();
        
        externalUrlSectionMock.Setup(x => x[It.Is<string>(s => s == "Url")]).Returns("http://example.com");
        apiEndpointSectionMock.Setup(x => x[It.Is<string>(s => s == "APIEndpoint")]).Returns("api/v1");

        var mockRootSection = new Mock<IConfigurationSection>();
        mockRootSection.Setup(x => x.GetSection("Url")).Returns(externalUrlSectionMock.Object);
        mockRootSection.Setup(x => x.GetSection("APIEndpoint")).Returns(apiEndpointSectionMock.Object);

        _configuration
            .Setup(x => x.GetSection(It.Is<string>(s => s == "ExternalUrl")))
            .Returns(mockRootSection.Object);
        
        _employeeBll = new EmployeeBll(_externalRequestDal.Object, _configuration.Object);
    }
    
    [Fact]
    public async Task GetEmployees_ShouldReturnMappedDtos_WhenResponseIsValid()
    {
        var mockResponse = new ResponseDto<IEnumerable<EmployeesExternal>?>
        {
            Data = new List<EmployeesExternal>
            {
                new EmployeesExternal
                {
                    Id = 1,
                    EmployeeName = "John Doe",
                    EmployeeSalary = 1000,
                    EmployeeAge = 30,
                    ProfileImage = "profile.jpg"
                }
            }
        };

         _externalRequestDal
            .Setup(x => x.GetRequest<IEnumerable<EmployeesExternal>?>(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(mockResponse);
         
        var result = await _employeeBll.GetEmployees();
        
        Assert.NotNull(result);
        Assert.Single(result);
        var employee = result.First();
        Assert.Equal(1, employee.Id);
        Assert.Equal("John Doe", employee.EmployeeName);
        Assert.Equal(1000, employee.EmployeeSalary);
        Assert.Equal(30, employee.EmployeeAge);
        Assert.Equal("profile.jpg", employee.ProfileImage);
        Assert.Equal(12000, employee.EmployeeAnualSalary);
    }
    
    [Fact]
    public async Task GetEmployees_ShouldReturnNull_WhenResponseIsNull()
    {
        var mockResponse = new ResponseDto<IEnumerable<EmployeesExternal>?>
        {
            Data = null
        };

        _externalRequestDal
            .Setup(x => x.GetRequest<IEnumerable<EmployeesExternal>?>(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(mockResponse);
        
        var result = await _employeeBll.GetEmployees();
        
        Assert.Null(result);
    }
    
    [Fact]
    public async Task GetEmployees_ShouldReturnEmptyList_WhenResponseDataIsEmpty()
    {
        var mockResponse = new ResponseDto<IEnumerable<EmployeesExternal>?>
        {
            Data = new List<EmployeesExternal>()
        };

        _externalRequestDal
            .Setup(x => x.GetRequest<IEnumerable<EmployeesExternal>?>(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(mockResponse);
        
        var result = await _employeeBll.GetEmployees();
        
        Assert.NotNull(result);
        Assert.Empty(result);
    }
    
    [Fact]
    public async Task GetEmployeeById_ShouldReturnEmployeeDto_WhenResponseIsValid()
    {
        var employeeId = 1;
        var mockResponse = new ResponseDto<EmployeesExternal?>
        {
            Data = new EmployeesExternal
            {
                Id = employeeId,
                EmployeeName = "John Doe",
                EmployeeSalary = 1000,
                EmployeeAge = 30,
                ProfileImage = "profile.jpg"
            }
        };

        _externalRequestDal
            .Setup(x => x.GetRequest<EmployeesExternal?>(It.IsAny<string>(), It.Is<string>(url => url.Contains($"/employee/{employeeId}"))))
            .ReturnsAsync(mockResponse);
        
        var result = await _employeeBll.GetEmployeeById(employeeId);
        
        Assert.NotNull(result);
        Assert.Equal(employeeId, result.Id);
        Assert.Equal("John Doe", result.EmployeeName);
        Assert.Equal(1000, result.EmployeeSalary);
        Assert.Equal(30, result.EmployeeAge);
        Assert.Equal("profile.jpg", result.ProfileImage);
        Assert.Equal(12000, result.EmployeeAnualSalary);
    }


}