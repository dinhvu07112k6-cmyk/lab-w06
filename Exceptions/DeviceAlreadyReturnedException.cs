namespace BorrowingSystem.Exceptions;

public class DeviceAlreadyReturnedException : BorrowingException
{
    public string DeviceId { get; }
    public string SlipId { get; }

    public DeviceAlreadyReturnedException(string deviceId, string slipId)
        : base($"Thiết bị {deviceId} trong phiếu {slipId} đã được trả trước đó.")
    {
        DeviceId = deviceId;
        SlipId = slipId;
    }
}