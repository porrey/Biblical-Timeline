using System.Drawing;

namespace Biblical.Timeline
{
	internal class TimeMarkerImageObject(BiblicalEvent biblicalEvent, PageDefinition pageDefinition, TimelineParameters parameters) : ImageObjectTemplate(biblicalEvent, pageDefinition, parameters)
	{
		protected override Brush FillBrush => Brushes.Transparent;
		protected override Pen BorderPen => Pens.Transparent;
		protected override Brush DarkTextBrush => this.PageDefinition.Theme.MarkerTextBrush;

		protected override Task OnDraw(Graphics g)
		{
			//
			// Draw a vertical line at the time marker.
			//
			g.DrawLine(this.PageDefinition.Theme.MarkerLinePen, this.Rectangle.Left, this.Rectangle.Top, this.Rectangle.Left, this.Rectangle.Bottom);
			return base.OnDraw(g);
		}

		protected override Task<float> OnMeasureAsync(Graphics g)
		{
			return Task.FromResult(this.TimelineParameters.PixelsPerYear * this.BiblicalEvent.EventLength);
		}
	}
}
