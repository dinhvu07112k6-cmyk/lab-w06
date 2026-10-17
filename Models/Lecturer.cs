namespace BorrowingSystem.Models;

public class Lecturer : IBorrower
{
    public string BorrowerId { get; }
    public string FullName { get; }

    public Lecturer(string borrowerId, string fullName)
    {
        if (string.IsNullOrWhiteSpace(borrowerId))
            throw new ArgumentException("Mã giảng viên không được rỗng.", nameof(borrowerId));
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Tên giảng viên không được rỗng.", nameof(fullName));

        BorrowerId = borrowerId;
        FullName = fullName;
    }

    public int GetMaximumDevices() => 3;
}