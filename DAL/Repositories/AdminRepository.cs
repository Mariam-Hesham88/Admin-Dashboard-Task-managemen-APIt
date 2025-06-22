using DAL.Data;
using DAL.Entites;
using DAL.Interfaces;
using DAL.Repositoies;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
	public class AdminRepository : GenericRepository<Admin>, IAdminRepository
	{
		private readonly ApplicationDbContext _context;

		public AdminRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<Admin?> GetByEmailAsync(string email)
		{
			return await _context.Admins.FirstOrDefaultAsync(a => a.Email == email);
		}
	}
}
