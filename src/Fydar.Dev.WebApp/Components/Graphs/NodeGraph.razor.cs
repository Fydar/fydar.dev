using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Fydar.Dev.WebApp.Components.Graphs;

public partial class NodeGraph : ComponentBase
{
	[Inject]
	public IJSRuntime JSRuntime { get; set; } = default!;

	public string PositionPixelsX { get; set; }
	public string PositionPixelsY { get; set; }

	protected override Task OnInitializedAsync()
	{
		return base.OnInitializedAsync();
	}
}
