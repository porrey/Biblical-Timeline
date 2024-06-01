using System.Drawing;
using Biblical.Timeline.Themes;

namespace Biblical.Timeline
{
	public interface IDrawableObject
	{
		RectangleF Rectangle { get; set; }
		IDictionary<StyleName, IThemeStyle> Styles { get; }
		Task<float> MeasureAsync(Graphics g);
		Task DrawAsync(Graphics g);
	}
}
