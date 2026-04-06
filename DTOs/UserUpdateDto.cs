using System.ComponentModel.DataAnnotations;

namespace WorkflowApi.DTOs
{
    public class UserUpdateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public byte[] RowVersion { get; set; }
    }
}
