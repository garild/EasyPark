namespace EasyPark.Domain;

public class PayPalOrder :  BaseEntity<Guid>
{
    public required string PayPalOrderId { get; init; } 
    
    public required string Intent { get; init; }
    
    public required OrderStatus Status { get; init; }
    
    public string? PayerId { get; init; }
    
    public required decimal Amount { get; init; }
    
    public required string Currency { get; init; }
    
    public string? PaymentId { get; init; }
    
    public DateTime? PaymentDate { get; init; }
    
    public string? CaptureId { get; init; }
    
    public string? RefundId { get; init; }
    
    public string? RefundReason { get; init; }
    
    public DateTimeOffset? RefundDate { get; init; }
}

public enum OrderStatus
{
    Created,
    Saved,
    Approved,
    Completed
}