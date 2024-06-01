using System.Drawing;

namespace Biblical.Timeline.Themes
{
	public interface IThemeStyle
	{
		Color Color { get; }
		Font Font { get; }
		Pen Pen { get; }
		Brush Brush { get; }
	}
}
