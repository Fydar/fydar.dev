using Microsoft.AspNetCore.Components;

namespace Fydar.Dev.WebApp.Components.Blocks;

public class HeadingModel
{
	public string Identifier { get; set; } = "";
	public RenderFragment? Text { get; set; }
	public int Depth { get; set; } = 1;
}
