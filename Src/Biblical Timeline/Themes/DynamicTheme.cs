using System.Drawing;
using System.Drawing.Drawing2D;
using Biblical.Timeline.Themes;

namespace Biblical.Timeline
{
	public class DynamicTheme : ITheme
	{
		public DynamicTheme()
		{
			this.DefaultFont = new("Cambria", 12.5F, FontStyle.Regular, GraphicsUnit.Point);

			this.Styles = new Dictionary<StyleName, IThemeStyle>
			{
				{ StyleName.Background, new ThemeStyle(this.DefaultFont, Color.White) },
				{ StyleName.GridLines, new ThemeStyle(this.DefaultFont, Color.FromArgb(90, Color.DarkOliveGreen.R,Color.DarkOliveGreen.G,Color.DarkOliveGreen.B), 2.5F, [10F, 10F], LineCap.NoAnchor, LineCap.NoAnchor) },
				{ StyleName.GridBorder, new ThemeStyle(this.DefaultFont, Color.DarkOliveGreen, 5F) },
				{ StyleName.Title, new ThemeStyle(new("Cambria", 70F, FontStyle.Regular, GraphicsUnit.Point), Color.DarkOliveGreen) },
				{ StyleName.SubTitle, new ThemeStyle(new("Calibri Light", 18F, FontStyle.Italic, GraphicsUnit.Point), Color.DarkOliveGreen) },
				{ StyleName.Text, new ThemeStyle(this.DefaultFont, Color.FromArgb(255, 80, 80, 80)) },
				{ StyleName.SmallText, new ThemeStyle(new("Cambria", 5F, FontStyle.Regular, GraphicsUnit.Point), Color.Black) },
				{ StyleName.MarkerLine, new ThemeStyle(this.DefaultFont, Color.DarkOliveGreen) },
				{ StyleName.MarkerText, new ThemeStyle(new("Calibri", 6F, FontStyle.Bold, GraphicsUnit.Point), Color.DarkOliveGreen) },
				{ StyleName.Header1, new ThemeStyle(new("Calibri", 14F, FontStyle.Bold, GraphicsUnit.Point), Color.Black) },
				{ StyleName.Header2, new ThemeStyle(new("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point), Color.FromArgb(128, Color.DarkOliveGreen.R,Color.DarkOliveGreen.G,Color.DarkOliveGreen.B)) },
				{ StyleName.Header3, new ThemeStyle(new("Calibri", 8F, FontStyle.Italic, GraphicsUnit.Point), Color.FromArgb(255, 50, 50, 50)) },
				{ StyleName.JumpLine, new ThemeStyle(this.DefaultFont, Color.FromArgb(50, Color.Red), 6.5F, [3F, 3F], LineCap.ArrowAnchor, LineCap.RoundAnchor ) },
				{ StyleName.ItemBorder, new ThemeStyle(this.DefaultFont, Color.Black) },
				{ StyleName.Item1, new ThemeStyle(this.DefaultFont, Color.LightBlue) },
				{ StyleName.Item2, new ThemeStyle(this.DefaultFont, Color.LightYellow) },
				{ StyleName.Item3, new ThemeStyle(this.DefaultFont, Color.Lavender) },
				{ StyleName.Extra1, new ThemeStyle(this.DefaultFont, Color.LavenderBlush) }
			};
		}

		public IDictionary<StyleName, IThemeStyle> Styles { get; }
		public Font DefaultFont { get; }
	}
}
