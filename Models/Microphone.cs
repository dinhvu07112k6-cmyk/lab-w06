using BorrowingSystem.Enums;

namespace BorrowingSystem.Models;

public class Microphone : TeachingDevice
{
    public Microphone(string deviceId, string name, int yearOfUse, decimal purchasePrice, int maxBorrowDays = 1, DeviceStatus status = DeviceStatus.Available)
        : base(deviceId, name, yearOfUse, purchasePrice, maxBorrowDays, status) { }

    public override decimal CalculateAnnualMaintenanceCost() => PurchasePrice * 0.12m;
}