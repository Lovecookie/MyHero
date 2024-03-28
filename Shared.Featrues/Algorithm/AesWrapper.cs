
namespace Shared.Featrues.Algorithm;


public static class AesWrapper
{
	private static readonly byte[] _key = [
		0xc6,
		0xc6,
		0x4e,	
		0x6f,		
		0x0f,
		0xee,
		0x95,
		0xee,
		0x3c,
		0x63,
		0xc6,	
		0x36,
		0x02,
		0xbb,
		0xb0,
		0x53,
		0x9e,
		0xe9,
		0x5d,
		0x64,
		0x21,
		0x26,
		0xdb,
		0xb3,
		0x8f,
		0x5f,
		0x5a,
		0x01,
		0xbd,
		0xcf,
		0x24,
		0x44,
	];

	private static readonly byte[] _iv = [
		0x5b,
		0x74,
		0xcf,
		0xb5,
		0x70,
		0x34,
		0x02,
		0x53,
		0x1f,	
		0x3c,
		0xc0,
		0x61,
		0xdb,
		0x57,
		0x1a,
		0x13,
	];

	public static async Task<string?> EncryptAsString(Int64 value)
	{
		return await AesEncryption.EncryptAsString(value.ToString(), _key, _iv);
	}

	public static async Task<string?> DecryptAsString(string value)
	{
		return await AesEncryption.DecryptAsString(value, _key, _iv);
	}

	public static async Task<Int64?> DecryptAsInt64(string value)
	{
		return await AesEncryption.DecryptAsInt64(value, _key, _iv);		
	}
}
