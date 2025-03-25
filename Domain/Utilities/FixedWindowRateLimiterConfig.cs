namespace Domain.Utilities;

public class FixedWindowRateLimiterConfig
{
    public int PermitLimit { get; set; }
    public string Window { get; set; } = null!; // Time window
    public int QueueLimit { get; set; }
    public string QueueProcessingOrder { get; set; } = null!; // OldestFirst or NewestFirst
}