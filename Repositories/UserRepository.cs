using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WorkflowApi.Data;
using WorkflowApi.DTOs;
using WorkflowApi.Exceptions;
using WorkflowApi.Models;

namespace WorkflowApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(User user)
        {
            await _db.Users.AddAsync(user);
        }

        public Task DeleteAsync(User user)
        {
            _db.Users.Remove(user);
            return Task.CompletedTask;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _db.Users.FindAsync(id);
        }

        public async Task<(List<User>, int)> GetUsersAsync(UserQueryParameters query)
        {
            var queryable = _db.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                queryable = queryable.Where(u => 
                    u.Name.Contains(query.Search) || 
                    u.Email.Contains(query.Search));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                switch (query.SortBy.ToLower())
                {
                    case "name":
                        queryable = query.SortDesc ? 
                            queryable.OrderByDescending(u => u.Name) : 
                            queryable.OrderBy(u => u.Name);
                        break;
                    case "email":
                        queryable = query.SortDesc ? 
                            queryable.OrderByDescending(u => u.Email) : 
                            queryable.OrderBy(u => u.Email);
                        break;
                    default:
                        queryable = queryable.OrderBy(u => u.Id);
                        break;
                }
            }
            else
            {
                queryable = queryable.OrderBy(u => u.Id);
            }

            var totalCount = await queryable.CountAsync();

            var pagedUsers = await queryable
                .AsNoTracking()
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();

            return (pagedUsers, totalCount);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

        public Task UpdateAsync(User user)
        {
            _db.Users.Update(user);
            return Task.CompletedTask;
        }
    }
}
