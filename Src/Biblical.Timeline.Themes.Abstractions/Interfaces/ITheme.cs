using System.Drawing;

namespace Biblical.Timeline.Themes
{
	public enum StyleName
	{
		Background,
		GridLines,
		GridBorder,
		Title,
		SubTitle,
		Header1,
		Header2,
		Header3,
		Text,
		SmallText,
		MarkerLine,
		MarkerText,
		JumpLine,
		ItemBorder,
		Item1,
		Item2,
		Item3,
		Extra1
	}

	public interface ITheme
	{
		Font DefaultFont { get; }
		IDictionary<StyleName, IThemeStyle> Styles { get; }
	}
}
