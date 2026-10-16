namespace BorrowingSystem.Exceptions;

public class InvalidBorrowPeriodException : BorrowingException
{
    public string DeviceId { get; }
    public int RequestedDays { get; }
    public int MaxBorrowDays { get; }

    public InvalidBorrowPeriodException(string deviceId, int requestedDays, int maxBorrowDays)
        : base($"Số ngày mượn ({requestedDays} ngày) không hợp lệ hoặc vượt quá giới hạn ({maxBorrowDays} ngày) của thiết bị {deviceId}.")
    {
        DeviceId = deviceId;
        RequestedDays = requestedDays;
        MaxBorrowDays = maxBorrowDays;
    }
}