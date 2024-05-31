using System.Drawing;
using System.Drawing.Drawing2D;

namespace Biblical.Timeline.Themes
{
	public class ThemeStyle : IThemeStyle
	{
		public ThemeStyle(Font font, Pen pen, Brush brush, Color color)
		{
			this.Font = font;
			this.Pen = pen;
			this.Brush = brush;
			this.Color = color;
		}

		public ThemeStyle(Font font, Color color)
		{
			this.Font = font;
			this.Color = color;
			this.Pen = new Pen(this.Color);
			this.Brush = new SolidBrush(this.Color);
		}

		public ThemeStyle(Font font, Color color, float penWidth)
		{
			this.Font = font;
			this.Color = color;
			this.Pen = new Pen(this.Color, penWidth);
			this.Brush = new SolidBrush(this.Color);
		}

		public ThemeStyle(Font font, Color color, float penWidth, float[] dashPattern)
		{
			this.Font = font;
			this.Color = color;
			
			this.Pen = new Pen(this.Color, penWidth)
			{
				DashPattern = dashPattern
			};

			this.Brush = new SolidBrush(this.Color);
		}

		public ThemeStyle(Font font, Color color, float penWidth, float[] dashPattern, LineCap startCap, LineCap endCap)
		{
			this.Font = font;
			this.Color = color;

			this.Pen = new Pen(this.Color, penWidth)
			{
				DashPattern = dashPattern,
				StartCap = startCap,
				EndCap = endCap,
			};

			this.Brush = new SolidBrush(this.Color);
		}

		public Color Color { get; }
		public Font Font { get; }
		public Pen Pen { get; }
		public Brush Brush { get; }
	}
}
