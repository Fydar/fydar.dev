using System.ComponentModel;

namespace Fydar.Dev.WebApp.Components.Decoration;

public class HeadingRegister : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	public HashSet<Heading> Headings { get; } = [];

	public void Register(Heading heading)
	{
		if (Headings.Add(heading))
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Heading)));
		}
	}
}
