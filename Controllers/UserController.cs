using Microsoft.AspNetCore.Mvc;
using WorkflowApi.Contracts;
using WorkflowApi.DTOs;
using WorkflowApi.Models;

namespace WorkflowApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreateDto user)
        {
            var created = await _userService.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<UserDto>>> GetUsers([FromQuery] UserQueryParameters query)
        {
            var result = await _userService.GetUsersAsync(query);


            // Not the final page
            if (result.Page < result.TotalPages)
            {
                result.NextPage = BuildPageUrl(result.Page + 1, query);
            }

            if (result.Page > 1)
            {
                result.PreviousPage = BuildPageUrl(result.Page - 1, query);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _userService.DeleteUserAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserUpdateDto dto)
        {
            await _userService.UpdateUserAsync(id, dto);
            return NoContent();
        }

        private string BuildPageUrl(int page, UserQueryParameters query)
        {
            var queryParameters = new Dictionary<string, string?>
            {
                ["page"] = page.ToString(),
                ["pageSize"] = query.PageSize.ToString(),
                ["search"] = query.Search,
                ["sortBy"] = query.SortBy,
                ["sortDesc"] = query.SortDesc.ToString()
            };

            var queryString = string.Join("&",
                queryParameters
                    .Where(p => !string.IsNullOrWhiteSpace(p.Value))
                    .Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value!)}"));

            var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}";

            return $"{baseUrl}?{queryString}";
        }
    }
}
