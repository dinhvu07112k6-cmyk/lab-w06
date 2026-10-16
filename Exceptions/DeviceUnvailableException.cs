using BorrowingSystem.Enums;

namespace BorrowingSystem.Exceptions;

public class DeviceUnavailableException : BorrowingException
{
    public string DeviceId { get; }
    public DeviceStatus CurrentStatus { get; }

    public DeviceUnavailableException(string deviceId, DeviceStatus currentStatus)
        : base($"Thiết bị {deviceId} hiện không sẵn sàng (Trạng thái: {currentStatus}).")
    {
        DeviceId = deviceId;
        CurrentStatus = currentStatus;
    }
}