using BorrowingSystem.Enums;

namespace BorrowingSystem.Models;
// Thuộc tính 
public abstract class TeachingDevice : Device
{
    public int MaxBorrowDays { get; }

    // Constructor
    protected TeachingDevice(string deviceId, string name, int yearOfUse, decimal purchasePrice, int maxBorrowDays, DeviceStatus status = DeviceStatus.Available)
        : base(deviceId, name, yearOfUse, purchasePrice, status)
    {
        if (maxBorrowDays <= 0)
            throw new ArgumentException("Số ngày mượn tối đa phải lớn hơn 0.", nameof(maxBorrowDays));

        MaxBorrowDays = maxBorrowDays;
    }
}