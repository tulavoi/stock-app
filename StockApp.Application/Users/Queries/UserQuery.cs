namespace StockApp.Application.Users.Queries;

public class UserQuery : BaseQuery
{
	public string? Role { get; set; }

	public UserQuery()
	{
		SortBy = "UserName";
	}
}
