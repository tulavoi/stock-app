namespace StockApp.Application.Commons;

public class PagedList<T>
{
	public List<T> Items { get; }
	public int PageNumber { get; }
	public int PageSize { get; }
	public long TotalCount { get; }
	public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
	public bool HasNextPage => PageNumber < TotalPages;
	public bool HasPreviousPage => PageNumber > 1;

	private PagedList(List<T> items, int pageNumber, int pageSize, long totalCount)
	{
		Items = items;
		PageNumber = pageNumber;
		PageSize = pageSize;
		TotalCount = totalCount;
	}

	public static PagedList<T> Create(List<T> items, int pageNumber, int pageSize, long totalCount) 
		=> new(items, pageNumber, pageSize, totalCount);
}
