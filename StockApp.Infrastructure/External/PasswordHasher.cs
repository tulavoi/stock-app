namespace StockApp.Infrastructure.External;

public class PasswordHasher : IPasswordHasher
{
	private const int SaltSize = 16;
	private const int HashSize = 32;
	private const int Interations = 100000;

	private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;

	public string Hash(string password)
	{
		byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
		byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Interations, Algorithm, HashSize);

		return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
	}

	public bool Verify(string hashedPassword, string providedPassword)
	{
		string[] parts = hashedPassword.Split('-');
		byte[] hash = Convert.FromHexString(parts[0]);
		byte[] salt = Convert.FromHexString(parts[1]);

		byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(providedPassword, salt, Interations, Algorithm, HashSize);

		return hash.SequenceEqual(inputHash);
	}
}
