using Data.Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Stores;

public class UserStore
{
    private readonly AppDbContext _context;

    public UserStore(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(int id) =>
        await _context.Users.FindAsync(id);

    public async Task<User?> GetByEmailAsync(string email) =>
        await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<List<User>> GetAllActiveAsync() =>
        await _context.Users.Where(u => u.IsActive).ToListAsync();

    public async Task SaveAsync() =>
        await _context.SaveChangesAsync();
}
