namespace WorkflowApi.Models
{
    public class DeviceReading
    {
        public int Id { get; set; }
        public string DeviceName { get; set; } = string.Empty;
        public decimal BatteryVoltage { get; set; }
        public decimal Temperature { get; set; }
        public DateTime RecordedAt { get; set; } = DateTime.UtcNow;
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
