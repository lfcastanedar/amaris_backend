using Amaris.Handlers;
using BLL.Interfaces;
using BLL.Model.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Amaris.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [TypeFilter(typeof(CustomExceptionHandler))]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeBll _employeeBll;
        
        public EmployeeController(IEmployeeBll employeeBll)
        {
            _employeeBll = employeeBll;
        }
        
        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(ResponseDTO<IEnumerable<EmployeesDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetAll()
        {
            var result = await _employeeBll.GetEmployees();
            
            ResponseDto response = new ResponseDto
            {
                Message = "",
                Result = result
            };

            return Ok(response);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseDTO<IEnumerable<EmployeesDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _employeeBll.GetEmployeeById(id);
            
            ResponseDto response = new ResponseDto
            {
                Message = "",
                Result = result
            };

            return Ok(response);
        }

        
    }
}
