using AutoMapper;
using BLL.DTOs;
using BLL.Helpers;
using BLL.Services.Interfaces;
using DAL.Entites;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Admin_dashboard.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		private readonly IEmployeeService _employeeService;
		private readonly IMapper _mapper;

		public EmployeeController(IEmployeeService employeeService, IMapper mapper)
		{
			_employeeService = employeeService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var employees = await _employeeService.GetAllAsync();
			var dtoList = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
			return Ok(new ApiResponse<IEnumerable<EmployeeDto>>(dtoList));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var emp = await _employeeService.GetByIdAsync(id);
			if (emp == null) return NotFound(new ApiResponse<string>("Employee not found"));

			var dto = _mapper.Map<EmployeeDto>(emp);
			return Ok(new ApiResponse<EmployeeDto>(dto));
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] EmployeeDto dto)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var emp = _mapper.Map<Employee>(dto);
			await _employeeService.AddAsync(emp);

			var resultDto = _mapper.Map<EmployeeDto>(emp);
			return CreatedAtAction(nameof(GetById),
				new { id = emp.Id },
				new ApiResponse<EmployeeDto>(resultDto, "The Employee was added successfully."));
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] EmployeeDto dto)
		{
			if (id != dto.Id) return BadRequest();

			var exist = await _employeeService.GetByIdAsync(id);
			if (exist == null) return NotFound(new ApiResponse<string>("Employee not found"));

			_mapper.Map(dto, exist);
			_employeeService.UpdateAsync(exist);

			return Ok(new ApiResponse<EmployeeDto>(dto, "The Employee was updated successfully."));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var emp = await _employeeService.GetByIdAsync(id);
			if (emp == null) return NotFound(new ApiResponse<string>("Employee not found"));

			_employeeService.DeleteAsync(id);

			return Ok(new ApiResponse<string>("The Employee was deleted successfully."));
		}
	}
}