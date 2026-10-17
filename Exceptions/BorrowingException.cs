/****************/
//Mã sinh viên: 202419021   
//Họ tên  : Phạm Đình Vũ 
/****************/
namespace BorrowingSystem.Exceptions
{
    public class BorrowingException : Exception
    {
        public BorrowingException(string message) : base(message)
        {
        }
        public BorrowingException(string message , Exception innerException) : base(message, innerException)
        {
        }
    }
}
