/****************/
//Mã sinh viên: 202419021   
//Họ tên  : Phạm Đình Vũ 
/****************/
namespace BorrowingSystem.Exceptions;

public class DeviceNotInSlipException : BorrowingException
{
    public string DeviceId { get; }
    public string SlipId { get; }

    public DeviceNotInSlipException(string deviceId, string slipId)
        : base($"Thiết bị {deviceId} không tồn tại trong phiếu mượn {slipId}.")
    {
        DeviceId = deviceId;
        SlipId = slipId;
    }
}