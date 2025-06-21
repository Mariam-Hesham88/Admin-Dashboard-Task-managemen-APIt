using AutoMapper;
using BLL.DTOs;
using BLL.Helpers;
using BLL.Services.Interfaces;
using DAL.Entites;
using Microsoft.AspNetCore.Mvc;

namespace Admin_dashboard.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class DepartmentController : ControllerBase
	{
		private readonly IDepartmentService _departmentService;
		private readonly IMapper _mapper;

		public DepartmentController(IDepartmentService departmentService, IMapper mapper)
		{
			_departmentService = departmentService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var departments = await _departmentService.GetAllAsync();
			var dtoList = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
			return Ok( new ApiResponse<IEnumerable<DepartmentDto>>(dtoList) );
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var department = await _departmentService.GetByIdAsync(id);
			if (department == null) return NotFound(new ApiResponse<string>("Department not found"));

			var dto = _mapper.Map<DepartmentDto>(department);
			return Ok( new ApiResponse<DepartmentDto>(dto) );
		}

		[HttpPost]
		public async Task<IActionResult> CreateDepartment([FromBody] DepartmentDto dto)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var department = _mapper.Map<Department>(dto);
			await _departmentService.AddAsync(department);

			var resultDto = _mapper.Map<DepartmentDto>(department);
			return CreatedAtAction(nameof(GetById),
				new { id = department.Id },
				new ApiResponse<DepartmentDto>(resultDto, "The Department was added successfully."));
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] DepartmentDto dto)
		{
			if (id != dto.Id) return BadRequest();

			var existingDepartment = await _departmentService.GetByIdAsync(id);
			if (existingDepartment == null) return NotFound(new ApiResponse<string>("Department not found"));

			_mapper.Map(dto, existingDepartment);
			await _departmentService.UpdateAsync(existingDepartment);

			var resultDto = _mapper.Map<DepartmentDto>(existingDepartment);
			return Ok(new ApiResponse<DepartmentDto>(resultDto, "The Department was updated successfully."));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var department = await _departmentService.GetByIdAsync(id);
			if (department == null) return NotFound(new ApiResponse<string>("Department not found"));

			await _departmentService.DeleteAsync(id);
			return Ok(new ApiResponse<string>("The Department was deleted successfully."));
		}
	}
}
