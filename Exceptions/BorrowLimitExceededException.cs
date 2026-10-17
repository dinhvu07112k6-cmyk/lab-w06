using BorrowingSystem.Enums;

namespace BorrowingSystem.Exceptions
{
    public class BorrowLimitExceededException : BorrowingException
    {
        public string BorrowerId { get; }
        public int MaximumDevices { get; }

        public BorrowLimitExceededException(string borrowerId, int maxDevices)
            : base($"Người mượn {borrowerId} đã vượt quá giới hạn mượn {maxDevices} thiết bị.")
        {
            BorrowerId = borrowerId;
            MaximumDevices = maxDevices;
        }
    }
}
