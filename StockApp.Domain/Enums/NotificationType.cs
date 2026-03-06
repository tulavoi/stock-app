namespace StockApp.Domain.Enums;

public enum NotificationType
{
	OrderExecuted, // Khi lệnh được khớp
	PriceAlert,    // Khi giá vượt ngưỡng
	NewsEvent      // Khi có tin tức liên quan đến cổ phiếu
}
