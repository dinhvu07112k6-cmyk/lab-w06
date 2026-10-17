/****************/
//Mã sinh viên: 202419021   
//Họ tên  : Phạm Đình Vũ 
/****************/  
namespace BorrowingSystem.Models;

public interface IBorrower
{
    string BorrowerId { get; }
    string FullName { get; }
    int GetMaximumDevices();
}