namespace StockApp.Domain.Enums;

public enum ErrorType
{
	Failure = 0,      // HTTP 500: Lỗi hệ thống, lỗi logic chung
	Validation = 1,   // HTTP 400: Dữ liệu đầu vào sai
	NotFound = 2,     // HTTP 404: Không tìm thấy tài nguyên
	Conflict = 3,     // HTTP 409: Dữ liệu bị trùng, xung đột
	Unauthorized = 4, // HTTP 401: Chưa xác thực (Token thiếu/sai)
	Forbidden = 5     // HTTP 403: Không có quyền truy cập
}

