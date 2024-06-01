using System.Drawing;
using Biblical.Timeline.Themes;

namespace Biblical.Timeline
{
	public class GridLine : IDrawableObject
	{
		public GridLine(int year, PageDefinition pageDefinition, TimelineParameters parameters)
		{
			this.Year = year;
			this.TopLabel = year.ToString();
			this.BottomLabel = year.ToString();
			this.PageDefinition = pageDefinition;
			this.TimelineParameters = parameters;
		}

		public PageDefinition PageDefinition { get; set; }
		public TimelineParameters TimelineParameters { get; set; }
		public int Year { get; set; }
		public string TopLabel { get; set; }
		public string BottomLabel { get; set; }
		public RectangleF Rectangle { get; set; }
		public IDictionary<StyleName, IThemeStyle> Styles => this.PageDefinition.Theme.Styles;

		public Task DrawAsync(Graphics g)
		{
			//
			// Get the size of the text.
			//
			SizeF size1 = g.MeasureString(this.TopLabel, this.Styles[StyleName.SubTitle].Font);
			SizeF size2 = g.MeasureString(this.BottomLabel, this.Styles[StyleName.SubTitle].Font);

			//
			// Calculate the left position.
			//
			float left = this.PageDefinition.Margin + (int)((float)this.Year / (float)this.TimelineParameters.TotalYears * this.PageDefinition.DrawableArea.Width);

			//
			// Draw the vertical line.
			//
			g.DrawLine(this.Styles[StyleName.GridLines].Pen, new PointF(left, (int)(this.PageDefinition.Margin + size1.Height + 1)), new PointF(left, this.PageDefinition.DrawableArea.Bottom - size2.Height));

			//
			// Draw the year text.
			//
			int textLeft1 = (int)(left - (size1.Width / 2F));
			g.DrawString(this.TopLabel, this.Styles[StyleName.SubTitle].Font, this.Styles[StyleName.SubTitle].Brush, new PointF(textLeft1, this.PageDefinition.Margin));

			int textLeft2 = (int)(left - (size2.Width / 2F));
			g.DrawString(this.BottomLabel, this.Styles[StyleName.SubTitle].Font, this.Styles[StyleName.SubTitle].Brush, new PointF(textLeft2, this.PageDefinition.DrawableArea.Bottom - 1 - size2.Height));

			return Task.CompletedTask;
		}

		public Task<float> MeasureAsync(Graphics g)
		{
			throw new NotSupportedException();
		}

		public override string ToString() => $"{this.Year}";
	}
}
