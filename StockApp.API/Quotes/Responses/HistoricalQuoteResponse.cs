namespace StockApp.API.Quotes.Responses;

public class HistoricalQuoteResponse
{
	public Guid StockId { get; set; }

	// Thời điểm của bản ghi (thường theo ngày)
	public DateTime Date { get; set; }

	// Giá đóng cửa trong phiên
	public decimal Close { get; set; }

	// Khối lượng giao dịch trong phiên
	public long Volume { get; set; }
}
