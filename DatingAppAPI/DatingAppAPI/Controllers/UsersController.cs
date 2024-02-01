using DatingAppAPI.Data;
using DatingAppAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingAppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // /api/users
    public class UsersController : Controller
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<IEnumerable<AppUser>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        [HttpGet("{id}")]
        public async Task<AppUser> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}
