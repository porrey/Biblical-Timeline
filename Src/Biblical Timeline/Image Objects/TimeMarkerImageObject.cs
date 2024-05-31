using System.Drawing;
using Biblical.Timeline.Themes;

namespace Biblical.Timeline
{
	internal class TimeMarkerImageObject(BiblicalEvent biblicalEvent, PageDefinition pageDefinition, TimelineParameters parameters) : ImageObjectTemplate(biblicalEvent, pageDefinition, parameters)
	{
		protected override Task OnDraw(Graphics g)
		{
			//
			// Draw a vertical line at the time marker.
			//
			Pen p = new(this.Styles[StyleName.Item1].Pen.Color, 10F);
			g.DrawLine(p, this.Rectangle.Left, this.Rectangle.Top, this.Rectangle.Left, this.Rectangle.Bottom);
			return base.OnDraw(g);
		}

		protected override Task<float> OnMeasureAsync(Graphics g)
		{
			return Task.FromResult(this.TimelineParameters.PixelsPerYear * this.BiblicalEvent.EventLength);
		}

		protected override Pen OnGetPen(StyleName styleName)
		{
			if (styleName == StyleName.ItemBorder)
			{
				return this.Styles[StyleName.Item1].Pen;
			}
			else
			{
				return base.OnGetPen(styleName);
			}
		}

		protected override Brush OnGetBrush(StyleName styleName)
		{
			if (styleName == StyleName.Header1)
			{
				return this.Styles[StyleName.Title].Brush;
			}
			else
			{
				return base.OnGetBrush(styleName);
			}
		}
	}
}
