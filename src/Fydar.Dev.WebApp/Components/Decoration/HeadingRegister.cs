using System.ComponentModel;

namespace Fydar.Dev.WebApp.Components.Decoration;

public class HeadingRegister : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public HashSet<HeadingModel> Headings { get; } = [];

	public void Register(HeadingModel heading)
	{
		if (Headings.Add(heading))
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Headings)));
		}
	}
}
