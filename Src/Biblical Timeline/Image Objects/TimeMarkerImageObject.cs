using System.Drawing;

namespace Biblical.Timeline
{
	internal class TimeMarkerImageObject(BiblicalEvent biblicalEvent, PageDefinition pageDefinition, TimelineParameters parameters) : ImageObjectTemplate(biblicalEvent, pageDefinition, parameters)
	{
		protected override Task OnDraw(Graphics g)
		{
			//
			// Draw a vertical line at the time marker.
			//
			g.DrawLine(this.PageDefinition.Theme.MarkerLinePen, this.Rectangle.Left, this.Rectangle.Top, this.Rectangle.Left, this.Rectangle.Bottom);

			//
			// Draw the text
			//
			SizeF size = g.MeasureString(this.BiblicalEvent.Name, this.PageDefinition.Theme.MarkerFont);
			float textLeft = this.BiblicalEvent.TextAlign == TextAlign.Right ? this.Rectangle.Left + 1 : this.Rectangle.Left - 1 - size.Width;
			float top = this.Rectangle.Top + 1;
			g.DrawString(this.BiblicalEvent.Name, this.PageDefinition.Theme.MarkerFont, this.PageDefinition.Theme.MarkerTextBrush, new PointF(textLeft, top));

			//
			// Draw the scripture reference.
			//
			SizeF refTextSize = g.MeasureString(this.BiblicalEvent.Reference, this.PageDefinition.Theme.ReferenceFont);
			float refTextLeft = this.BiblicalEvent.TextAlign == TextAlign.Right ? this.Rectangle.Left + 3 : textLeft;
			float refTextTop = this.Rectangle.Bottom - refTextSize.Height + 2;
			g.DrawString(this.BiblicalEvent.Reference, this.PageDefinition.Theme.ReferenceFont, this.PageDefinition.Theme.ReferenceBrush, new PointF(refTextLeft, refTextTop));

			if (this.BiblicalEvent.ResetTop)
			{
				//
				// Draw a line with an arrow from the predeccors to this item.
				//
				g.DrawLine(this.PageDefinition.Theme.JumpLinePen, this.Rectangle.Left, this.Rectangle.Bottom, this.Rectangle.Left, this.Predecessor.Rectangle.Top);
			}

			return Task.CompletedTask;
		}

		protected override Task<int> OnMeasureAsync(Graphics g)
		{
			return Task.FromResult((int)(this.TimelineParameters.PixelsPerYear * this.BiblicalEvent.EventLength));
		}
	}
}
