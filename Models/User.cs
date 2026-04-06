using System.ComponentModel.DataAnnotations;

namespace WorkflowApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        [Timestamp]
        public byte[] RowVersion { get; set; } = default;
    }
}
