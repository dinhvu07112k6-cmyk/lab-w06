
/****************/
//Mã sinh viên: 202419021   
//Họ tên  : Phạm Đình Vũ 
/****************/  namespace BorrowingSystem.Models;

public class TeachingAssistant : IBorrower
{
    public string BorrowerId { get; }
    public string FullName { get; }

    public TeachingAssistant(string borrowerId, string fullName)
    {
        if (string.IsNullOrWhiteSpace(borrowerId))
            throw new ArgumentException("Mã trợ giảng không được rỗng.", nameof(borrowerId));
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Tên trợ giảng không được rỗng.", nameof(fullName));

        BorrowerId = borrowerId;
        FullName = fullName;
    }

    public int GetMaximumDevices() => 2;
}