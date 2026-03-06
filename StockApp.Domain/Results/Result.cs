namespace StockApp.Domain.Results;

public class Result
{
	public bool IsSuccess { get; }
	public bool IsFailure => !IsSuccess;
	public IReadOnlyList<Error> Errors { get; }

	protected Result(bool isSuccess, List<Error> errors)
	{
		if (isSuccess && errors.Any())
			throw new ArgumentException("Success result cannot contain errors");

		if (!isSuccess && !errors.Any())
			throw new ArgumentException("Failure result must contain at least one error");

		IsSuccess = isSuccess;
		Errors = errors;
	}

	public static Result Success() => new(true, new());

	public static Result Failure(Error error) => new(false, new() { error });

	public static Result Failure(IEnumerable<Error> errors) => new(false, errors.ToList());
}
