using AutoMapper;
using BLL.DTOs;
using BLL.Helpers;
using BLL.Services.Interfaces;
using DAL.Entites;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Admin_dashboard.Controllers
{
	[Authorize]
	[Route("api/v1/[controller]")]
	[ApiController]
	public class TaskItemController : ControllerBase
	{
		private readonly ITaskItemService _taskItemService;
		private readonly IMapper _mapper;

		public TaskItemController(ITaskItemService taskItemService, IMapper mapper)
		{
			_taskItemService = taskItemService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var tasks = await _taskItemService.GetAllAsync();
			var dtos = _mapper.Map<IEnumerable<TaskItemDto>>(tasks);
			return Ok(new ApiResponse<IEnumerable<TaskItemDto>>(dtos));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var task = await _taskItemService.GetByIdAsync(id);
			if (task == null) return NotFound(new ApiResponse<string>("TaskItem not found"));

			var dto = _mapper.Map<TaskItemDto>(task);
			return Ok(new ApiResponse<TaskItemDto>(dto));
		}

		[HttpPost]
		public async Task<IActionResult> Create(TaskItemDto dto)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var task = _mapper.Map<TaskItem>(dto);
			await _taskItemService.AddAsync(task);

			var resultDto = _mapper.Map<TaskItemDto>(task);
			return CreatedAtAction(nameof(GetById),
				new { id = task.Id },
				new ApiResponse<TaskItemDto>(resultDto, "The TaskItem was added successfully."));
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, TaskItemDto dto)
		{
			if (id != dto.Id) return BadRequest();

			var existing = await _taskItemService.GetByIdAsync(id);
			if (existing == null) return NotFound(new ApiResponse<string>("TaskItem not found"));

			_mapper.Map(dto, existing);
			await _taskItemService.UpdateAsync(existing);

			return Ok(new ApiResponse<TaskItemDto>(dto, "The TaskItem was updated successfully."));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var task = await _taskItemService.GetByIdAsync(id);
			if (task == null) return NotFound(new ApiResponse<string>("TaskItem not found"));

			await _taskItemService.DeleteAsync(id);

			return Ok(new ApiResponse<string>("The TaskItem was deleted successfully."));
		}

		[HttpGet("by-department/{departmentId}")]
		public async Task<IActionResult> GetTasksByDepartment(int departmentId)
		{
			var tasks = await _taskItemService.GetTasksByDepartmentAsync(departmentId);
			var dtos = _mapper.Map<IEnumerable<TaskItemDto>>(tasks);

			return Ok(new ApiResponse<IEnumerable<TaskItemDto>>(dtos));
		}
	}

}
