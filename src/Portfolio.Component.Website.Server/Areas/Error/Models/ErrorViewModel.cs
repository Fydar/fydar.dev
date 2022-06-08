namespace Portfolio.Component.Website.Server.Areas.Error.Models;

public class ErrorViewModel
{
	public string RequestId { get; set; } = string.Empty;

	public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
