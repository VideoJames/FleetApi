using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorkflowApi.Contracts;
using WorkflowApi.DTOs;
using WorkflowApi.Exceptions;
using WorkflowApi.Models;
using WorkflowApi.Repositories;


namespace WorkflowApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserResponseDto> CreateUserAsync(UserCreateDto dto)
        {
            var newUser = _mapper.Map<User>(dto);

            await _userRepository.AddAsync(newUser);
            await _userRepository.SaveChangesAsync();

            return _mapper.Map<UserResponseDto>(newUser);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            await _userRepository.DeleteAsync(user);
            await _userRepository.SaveChangesAsync();
            return true;
        }

        public async Task<PagedResult<UserDto>> GetUsersAsync(UserQueryParameters query)
        {
            var (users, totalCount) = await _userRepository.GetUsersAsync(query);
            return new PagedResult<UserDto>
            {
                Items = _mapper.Map<List<UserDto>>(users),
                Page = query.Page,
                PageSize = query.PageSize,
                TotalCount = totalCount
            };
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User {id} not found.");
            }
            return _mapper.Map<UserDto>(user);
        }

        public async Task UpdateUserAsync(int id, UserUpdateDto dto)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);

            if (existingUser == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            _mapper.Map(dto, existingUser);

            existingUser.RowVersion = dto.RowVersion;

            await _userRepository.UpdateAsync(existingUser);            

            try
            {
                await _userRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new ConcurrencyConflictException("Concurrency conflict detected.");
            }
        }

        
    }
}
