/****************/
//Mã sinh viên: 202419021   
//Họ tên  : Phạm Đình Vũ 
/****************/  
using BorrowingSystem.Enums;
using BorrowingSystem.Exceptions;
using BorrowingSystem.Models;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("================ LUỒNG THÀNH CÔNG ================");
RunSuccessFlow();

Console.WriteLine("\n================ LUỒNG NGOẠI LỆ ================");
RunExceptionTests();

static void RunSuccessFlow()
{
    var lecturer = new Lecturer("GV01", "TS. Nguyễn Văn A");
    var projectorRemote = new ProjectorRemote("PR01", "ĐK Máy chiếu Sony", 2022, 300000, maxBorrowDays: 2);
    var mic = new Microphone("MC01", "Micro Shure", 2023, 1200000, maxBorrowDays: 3);

    var slip = new BorrowingSlip("PH001", lecturer, "A2-301", DateTime.Today);

    slip.AddDevice(projectorRemote, 2);
    slip.AddDevice(mic, 1);
    Console.WriteLine($"Đã thêm 2 thiết bị vào phiếu {slip.SlipId} cho giảng viên {lecturer.FullName}");

    // Trả điều khiển máy chiếu đúng hạn
    decimal fee1 = slip.ReturnDevice("PR01", DateTime.Today.AddDays(1));
    Console.WriteLine($"Trả PR01 thành công. Phí trễ hạn: {fee1:N0} đ");

    // Trả micro muộn 2 ngày (hạn là mượn 1 ngày -> trả ở ngày thứ 3)
    decimal fee2 = slip.ReturnDevice("MC01", DateTime.Today.AddDays(3));
    Console.WriteLine($"Trả MC01 thành công. Phí trễ hạn (2 ngày trễ): {fee2:N0} đ");
    Console.WriteLine($"Tổng phí phạt trễ phiếu {slip.SlipId}: {slip.CalculateTotalLateFee():N0} đ");
}

static void RunExceptionTests()
{
    var ta = new TeachingAssistant("TA01", "Trợ giảng Lê B");
    var acRemote = new AirConditionerRemote("AC01", "ĐK Điều hòa Daikin", 2021, 250000, maxBorrowDays: 1, status: DeviceStatus.UnderMaintenance);
    var mic1 = new Microphone("MC02", "Micro Không dây 1", 2023, 800000, maxBorrowDays: 1);
    var mic2 = new Microphone("MC03", "Micro Không dây 2", 2023, 800000, maxBorrowDays: 1);
    var mic3 = new Microphone("MC04", "Micro Không dây 3", 2023, 800000, maxBorrowDays: 1);

    var slip = new BorrowingSlip("PH002", ta, "B1-205", DateTime.Today);

    // Test 1: Mượn thiết bị đang UnderMaintenance
    ExecuteTestCase("1. Mượn thiết bị đang bảo trì", () => slip.AddDevice(acRemote, 1));

    // Test 2: Mượn micro trong 3 ngày dù giới hạn là 1 ngày
    ExecuteTestCase("2. Vượt quá số ngày mượn quy định", () => slip.AddDevice(mic1, 3));

    // Chuẩn bị thêm 2 thiết bị hợp lệ (chạm trần mức tối đa của Trợ giảng = 2)
    slip.AddDevice(mic1, 1);
    slip.AddDevice(mic2, 1);

    // Test 3: Trợ giảng mượn thiết bị thứ 3 (vượt giới hạn)
    ExecuteTestCase("3. Vượt quá hạn mức người mượn (TA max 2)", () => slip.AddDevice(mic3, 1));

    // Test 4: Thêm cùng một thiết bị 2 lần vào phiếu
    ExecuteTestCase("4. Thiết bị trùng lặp trong phiếu", () => slip.AddDevice(mic1, 1));

    // Test 5: Trả thiết bị không có trong phiếu
    ExecuteTestCase("5. Trả thiết bị không thuộc phiếu", () => slip.ReturnDevice("NOT_EXIST_ID", DateTime.Today));

    // Test 6: Trả cùng một thiết bị 2 lần (Trả MC02 lần 1 thành công, lần 2 sẽ phát sinh lỗi)
    slip.ReturnDevice("MC02", DateTime.Today);
    ExecuteTestCase("6. Trả cùng một thiết bị hai lần", () => slip.ReturnDevice("MC02", DateTime.Today));

    // Test 7: Trả trước ngày mượn
    ExecuteTestCase("7. Trả trước ngày mượn", () =>
    {
        var tempSlip = new BorrowingSlip("PH003", ta, "C1-101", DateTime.Today);
        var tempMic = new Microphone("MC05", "Micro Test", 2024, 500000, maxBorrowDays: 2);
        tempSlip.AddDevice(tempMic, 1);
        tempSlip.ReturnDevice("MC05", DateTime.Today.AddDays(-1));
    });
}

static void ExecuteTestCase(string testName, Action action)
{
    Console.Write($"[{testName}] -> ");
    try
    {
        action();
        Console.WriteLine("Thất bại (Không phát sinh ngoại lệ mong muốn)!");
    }
    catch (BorrowingException ex)
    {
        Console.WriteLine($"Bắt được {ex.GetType().Name}: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ngoại lệ hệ thống khác: {ex.Message}");
    }
    finally
    {
        // Khối finally luôn chạy dù test thành công hay bắt được exception
        Console.WriteLine("   --> [Hoàn tất ca kiểm thử]");
    }
}