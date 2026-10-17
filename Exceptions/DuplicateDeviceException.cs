/****************/
//Mã sinh viên: 202419021   
//Họ tên  : Phạm Đình Vũ 
/****************/
namespace BorrowingSystem.Exceptions;

public class DuplicateDeviceException : BorrowingException
{
    public string DeviceId { get; }
    public string SlipId { get; }

    public DuplicateDeviceException(string deviceId, string slipId)
        : base($"Thiết bị {deviceId} đã có trong phiếu mượn {slipId}.")
    {
        DeviceId = deviceId;
        SlipId = slipId;
    }
}