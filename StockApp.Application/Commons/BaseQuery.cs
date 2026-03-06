namespace StockApp.Application.Commons;

public abstract class BaseQuery
{
	public bool IsDecsending { get; set; } = false;
	public string SortBy { get; set; } = "Id";
	public int PageNumber { get; set; } = 1;
	public int PageSize { get; set; } = 20;
	public string? Keyword { get; set; }
}
