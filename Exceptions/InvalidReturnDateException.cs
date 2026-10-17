/****************/
//Mã sinh viên: 202419021   
//Họ tên  : Phạm Đình Vũ 
/****************/
namespace BorrowingSystem.Exceptions;

public class InvalidReturnDateException : BorrowingException
{
    public DateTime BorrowedDate { get; }
    public DateTime AttemptedReturnDate { get; }

    public InvalidReturnDateException(DateTime borrowedDate, DateTime attemptedReturnDate)
        : base($"Ngày trả ({attemptedReturnDate:dd/MM/yyyy}) không thể trước ngày mượn ({borrowedDate:dd/MM/yyyy}).")
    {
        BorrowedDate = borrowedDate;
        AttemptedReturnDate = attemptedReturnDate;
    }
}