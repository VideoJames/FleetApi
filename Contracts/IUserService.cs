using WorkflowApi.DTOs;
using WorkflowApi.Models;

namespace WorkflowApi.Contracts
{
    public interface IUserService
    {
        Task<UserResponseDto> CreateUserAsync(UserCreateDto user);
        Task<PagedResult<UserDto>> GetUsersAsync(UserQueryParameters query);
        Task<UserDto?> GetByIdAsync(int id);
        Task<bool> DeleteUserAsync(int id);
        Task UpdateUserAsync(int id, UserUpdateDto user);
    }
}
