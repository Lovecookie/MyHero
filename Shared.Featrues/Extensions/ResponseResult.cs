
namespace Shared.Features.Extensions;


public record CommonResponse
{
	[Required, JsonPropertyName("code")]
	public int Code { get; init; }

	[Required, JsonPropertyName("msg")]
	public string Message { get; init; } = "";

	[JsonPropertyName("data")]
	public object? Data { get; init; }
};


public static class ToClientResults
{	
	public static IResult Ok(object? data = null)
	{
		return Results.Ok(
			new CommonResponse
			{
				Code = 0,
				Message = "",
				Data = data
			});
	}

	public static IResult Error(string message)
	{
		return Results.Ok(
			new CommonResponse
			{
				Code = 1000,
				Message = message,
			});
	}

	public static IResult Error(int code, string message)
	{
		return Results.Ok(
			new CommonResponse
			{
				Code = code,
				Message = message
			});
	}
};
