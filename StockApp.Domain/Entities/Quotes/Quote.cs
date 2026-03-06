namespace StockApp.Domain.Entities.Quotes;

public class Quote
{
	public long Id { get; set; }
	public Guid StockId { get; set; }
	public decimal Price { get; set; }
	public decimal Change { get; set; }
	public decimal PercentChange { get; set; }
	public int Volume { get; set; }
	public DateTime TimeStamp { get; set; }
}
