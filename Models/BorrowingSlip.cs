/****************/
//Mã sinh viên: 202419021   
//Họ tên  : Phạm Đình Vũ 
/****************/
using BorrowingSystem.Enums;
using BorrowingSystem.Exceptions;

namespace BorrowingSystem.Models;

public class BorrowingSlip
{
    private readonly List<BorrowItem> _items = new();

    public string SlipId { get; }
    public IBorrower Borrower { get; }
    public string TeachingRoom { get; }
    public DateTime BorrowedDate { get; }
    public IReadOnlyList<BorrowItem> Items => _items.AsReadOnly();

    public BorrowingSlip(string slipId, IBorrower borrower, string teachingRoom, DateTime borrowedDate)
    {
        if (string.IsNullOrWhiteSpace(slipId))
            throw new ArgumentException("Mã phiếu không được để trống.", nameof(slipId));
        if (string.IsNullOrWhiteSpace(teachingRoom))
            throw new ArgumentException("Phòng dạy không được để trống.", nameof(teachingRoom));

        SlipId = slipId;
        Borrower = borrower ?? throw new ArgumentNullException(nameof(borrower));
        TeachingRoom = teachingRoom;
        BorrowedDate = borrowedDate;
    }

    public void AddDevice(TeachingDevice device, int borrowDays)
    {
        ValidateBorrowing(device, borrowDays);

        var dueDate = BorrowedDate.AddDays(borrowDays);
        var item = new BorrowItem(device, BorrowedDate, dueDate);

        // Đảm bảo tính nhất quán: chỉ đổi trạng thái khi mọi ràng buộc đều thỏa mãn
        device.MarkAsBorrowed();
        _items.Add(item);
    }

    private void ValidateBorrowing(TeachingDevice device, int borrowDays)
    {
        if (device == null)
            throw new ArgumentNullException(nameof(device));
        if (borrowDays <= 0 || borrowDays > device.MaxBorrowDays)
            throw new InvalidBorrowPeriodException(device.DeviceId, borrowDays, device.MaxBorrowDays);
        if (_items.Any(i => i.Device.DeviceId == device.DeviceId))
            throw new DuplicateDeviceException(device.DeviceId, SlipId);
        if (_items.Count >= Borrower.GetMaximumDevices())
            throw new BorrowLimitExceededException(Borrower.BorrowerId, Borrower.GetMaximumDevices());
        if (device.Status != DeviceStatus.Available)
            throw new DeviceUnavailableException(device.DeviceId, device.Status);
    }

    public decimal ReturnDevice(string deviceId, DateTime returnedDate)
    {
        if (string.IsNullOrWhiteSpace(deviceId))
            throw new ArgumentException("Mã thiết bị không hợp lệ.", nameof(deviceId));

        var item = _items.FirstOrDefault(i => i.Device.DeviceId == deviceId);
        if (item == null)
            throw new DeviceNotInSlipException(deviceId, SlipId);
        if (item.IsReturned)
            throw new DeviceAlreadyReturnedException(deviceId, SlipId);

        item.Return(returnedDate);
        return item.CalculateLateFee();
    }

    public decimal CalculateTotalLateFee() => _items.Sum(item => item.CalculateLateFee());
}