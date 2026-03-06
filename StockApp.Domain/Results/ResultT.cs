namespace StockApp.Domain.Results;

public class Result<T> : Result
{
	private readonly T? _value;

	public T Value => IsSuccess
		? _value!
		: throw new InvalidOperationException("The value of a failure result can not be accessed.");

	protected Result(T? value, bool isSuccess, List<Error> errors)
		: base(isSuccess, errors)
	{
		_value = value;
	}

	public static Result<T> Success(T value) => new(value, true, new());

	public new static Result<T> Failure(Error error) => new(default, false, new(){ error });

	public new static Result<T> Failure(IEnumerable<Error> errors) => new(default, false, errors.ToList());
}