/****************/
//Mã sinh viên: 202419021   
//Họ tên  : Phạm Đình Vũ 
/****************/
using BorrowingSystem.Enums;

namespace BorrowingSystem.Models;

// Thuộc tính và phương thức
public class Microphone : TeachingDevice
{
    public Microphone(string deviceId, string name, int yearOfUse, decimal purchasePrice, int maxBorrowDays = 1, DeviceStatus status = DeviceStatus.Available)
        : base(deviceId, name, yearOfUse, purchasePrice, maxBorrowDays, status) { }

    public override decimal CalculateAnnualMaintenanceCost() => PurchasePrice * 0.12m;
}