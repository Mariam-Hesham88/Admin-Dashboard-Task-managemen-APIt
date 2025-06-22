using DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
	public interface IAdminRepository : IGenericRepository<Admin>
	{
		Task<Admin?> GetByEmailAsync(string email);
	}
}
