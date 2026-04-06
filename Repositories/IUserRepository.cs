using WorkflowApi.DTOs;
using WorkflowApi.Models;

namespace WorkflowApi.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<(List<User>, int)> GetUsersAsync(UserQueryParameters query);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task SaveChangesAsync();
    }
}
