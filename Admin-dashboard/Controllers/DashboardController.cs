using BLL.DTOs;
using BLL.Helpers;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Admin_dashboard.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class DashboardController : ControllerBase
	{
		private readonly IDashboardService _dashboardService;

		public DashboardController(IDashboardService dashboardService)
		{
			_dashboardService = dashboardService;
		}

		[HttpGet("statistics")]
		public async Task<IActionResult> GetStats()
		{
			var stats = await _dashboardService.GetStatisticsAsync();
			return Ok(new ApiResponse<DashboardStatsDto>(stats));
		}
	}
}
