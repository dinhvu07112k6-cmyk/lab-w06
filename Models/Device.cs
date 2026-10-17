/****************/
//Mã sinh viên: 202419021   
//Họ tên  : Phạm Đình Vũ 
/****************/
using BorrowingSystem.Enums;

namespace BorrowingSystem.Models;

// Thuộc tính
public abstract class Device
{
    public string DeviceId { get; }
    public string Name { get; set; }
    public int YearOfUse { get; set; }
    public decimal PurchasePrice { get; set; }
    public DeviceStatus Status { get; private set; }
    
    // Constructor
    protected Device(string deviceId, string name, int yearOfUse, decimal purchasePrice, DeviceStatus status = DeviceStatus.Available)
    {
        if (string.IsNullOrWhiteSpace(deviceId))
            throw new ArgumentException("Mã thiết bị không được để trống.", nameof(deviceId));
        if (purchasePrice <= 0)
            throw new ArgumentException("Giá mua phải lớn hơn 0.", nameof(purchasePrice));
        if (yearOfUse > DateTime.Now.Year)
            throw new ArgumentException("Năm đưa vào sử dụng không được lớn hơn năm hiện tại.", nameof(yearOfUse));

        DeviceId = deviceId;
        Name = name;
        YearOfUse = yearOfUse;
        PurchasePrice = purchasePrice;
        Status = status;
    }
    // Phương thức 
    public void MarkAsBorrowed() => Status = DeviceStatus.Borrowed;
    public void MarkAsAvailable() => Status = DeviceStatus.Available;
    public void MarkAsMaintenance() => Status = DeviceStatus.UnderMaintenance;

    public abstract decimal CalculateAnnualMaintenanceCost();

    public override string ToString()
    {
        return $"[{DeviceId}] {Name} | Năm SD: {YearOfUse} | Giá: {PurchasePrice:C0} | TT: {Status}";
    }
}