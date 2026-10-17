using BorrowingSystem.Exceptions;

namespace BorrowingSystem.Models;

// Thuộc tính 
public class BorrowItem
{
    public TeachingDevice Device { get; }
    public DateTime BorrowedDate { get; }
    public DateTime DueDate { get; }
    public DateTime? ReturnedDate { get; private set; }
    public bool IsReturned => ReturnedDate.HasValue;

    // Constructor
    public BorrowItem(TeachingDevice device, DateTime borrowedDate, DateTime dueDate)
    {
        Device = device ?? throw new ArgumentNullException(nameof(device));
        if (dueDate < borrowedDate)
            throw new ArgumentException("Hạn trả phải sau hoặc cùng ngày mượn.");

        BorrowedDate = borrowedDate;
        DueDate = dueDate;
        ReturnedDate = null;
    }

    // Phương thức trả thiết bị
    public void Return(DateTime returnedDate)
    {
        if (IsReturned)
            throw new BorrowingException($"Mục {Device.DeviceId} đã được trả trước đó.");
        if (returnedDate < BorrowedDate)
            throw new InvalidReturnDateException(BorrowedDate, returnedDate);

        ReturnedDate = returnedDate;
        Device.MarkAsAvailable();
    }

    // Phương thức tính phí nộp muộn ( tự thêm^^ )
    public decimal CalculateLateFee(decimal dailyRate = 10000m)
    {
        if (!IsReturned || ReturnedDate <= DueDate)
            return 0m;

        int lateDays = (ReturnedDate.Value.Date - DueDate.Date).Days;
        return lateDays > 0 ? lateDays * dailyRate : 0m;
    }
}