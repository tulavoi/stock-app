using StockApp.Domain.Errors;

namespace StockApp.Domain.ValueObjects;

public class DateOfBirth : ValueObject
{
	public DateOnly Value { get; }

	private DateOfBirth(DateOnly value)
	{
		Value = value;
	}

	public static Result<DateOfBirth> Create(DateOnly dob)
	{
		var errors = new List<Error>();
		var today = DateOnly.FromDateTime(DateTime.UtcNow);

		// Check 1: Ngày sinh tương lai
		if (dob > today)
			errors.Add(DateOfBirthErrors.CannotBeFuture);

		// Check 2: Tuổi
		int age = today.Year - dob.Year;
		if (today < dob.AddYears(age)) age--;

		if (age < 15)
			errors.Add(DateOfBirthErrors.TooYoung);

		return errors.Any()
			? Result<DateOfBirth>.Failure(errors)
			: Result<DateOfBirth>.Success(new DateOfBirth(dob));
	}

	protected override IEnumerable<object> GetEqualityComponents()
	{
		throw new NotImplementedException();
	}
}
