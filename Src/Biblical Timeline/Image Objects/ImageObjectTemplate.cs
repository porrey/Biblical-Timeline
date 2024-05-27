using System.Drawing;

namespace Biblical.Timeline
{
	internal abstract class ImageObjectTemplate(BiblicalEvent biblicalEvent, PageDefinition pageDefinition, TimelineParameters parameters)
	{
		public BiblicalEvent BiblicalEvent { get; protected set; } = biblicalEvent;
		protected PageDefinition PageDefinition { get; } = pageDefinition;
		protected TimelineParameters TimelineParameters { get; } = parameters;
		public ImageObjectTemplate Predecessor { get; set; }
		public RectangleF Rectangle { get; set; }

		protected virtual Brush FillBrush => this.PageDefinition.Theme.BarDarkBackgroundBrush;
		protected virtual Pen BorderPen => this.PageDefinition.Theme.BarDarkBorderPen;
		protected virtual Pen JumpLinePen => this.PageDefinition.Theme.JumpLinePen;
		protected virtual Font NameFont => this.PageDefinition.Theme.NameFont;
		protected virtual Font ReferenceFont => this.PageDefinition.Theme.ReferenceFont;
		protected virtual Font StartYearFont => this.PageDefinition.Theme.StartYearFont;
		protected virtual Font LengthFont => this.PageDefinition.Theme.LengthFont;

		protected virtual Brush DarkTextBrush => this.PageDefinition.Theme.BarDarkTextBrush;
		protected virtual Brush LightTextBrush => this.PageDefinition.Theme.BarLightTextBrush;
		protected virtual Brush YearBrush => this.PageDefinition.Theme.YearBrush;
		protected virtual Brush ReferenceBrush => this.PageDefinition.Theme.ReferenceBrush;

		public virtual Task DrawAsync(Graphics g)
		{
			return this.OnDraw(g);
		}

		public virtual Task<float> MeasureAsync(Graphics g)
		{
			return this.OnMeasureAsync(g);
		}

		protected virtual Task<float> OnMeasureAsync(Graphics g)
		{
			float returnValue = this.TimelineParameters.PixelsPerYear * this.BiblicalEvent.EventLength;

			//
			// Auto size the box if the event length is unknown (-1).
			//
			if (this.BiblicalEvent.EventLength == -1)
			{
				SizeF size1 = this.PageDefinition.Graphics.MeasureString(this.BiblicalEvent.Name, this.PageDefinition.Theme.NameFont);
				SizeF size2 = this.PageDefinition.Graphics.MeasureString("(?)", this.PageDefinition.Theme.LengthFont);

				returnValue = (int)(size1.Width + size2.Width);
			}

			return Task.FromResult(returnValue);
		}

		protected virtual Task OnDraw(Graphics g)
		{
			//
			// Draw the rectangle.
			//
			g.FillRectangle(this.FillBrush, this.Rectangle);
			g.DrawRectangle(this.BorderPen, this.Rectangle);

			//
			// Measure the text.
			//
			SizeF size1 = g.MeasureString(this.BiblicalEvent.Name, this.NameFont);

			//
			// Measure the text.
			//
			string lengthText = $" ({(this.BiblicalEvent.EventLength == -1 ? "?" : this.BiblicalEvent.EventLength)})";
			SizeF size2 = g.MeasureString(lengthText, this.NameFont);

			//
			// Get the left position.
			//
			float left = 0;

			if (this.BiblicalEvent.DisplayEventLength)
			{
				if ((size1.Width + size2.Width) > this.Rectangle.Width)
				{
					left = this.Rectangle.Right + 1;
				}
				else
				{
					left = this.Rectangle.Left + 1;
				}
			}
			else
			{
				if (size1.Width > this.Rectangle.Width)
				{
					left = this.Rectangle.Right + 1;
				}
				else
				{
					left = this.Rectangle.Left + 1;
				}
			}

			//
			// Caclulate the text position and draw the text.
			//
			float top1 = this.Rectangle.Top + 1;
			g.DrawString(this.BiblicalEvent.Name, this.NameFont, this.DarkTextBrush, new PointF(left, top1));

			//
			// Add the event time span if requested.
			//
			if (this.BiblicalEvent.DisplayEventLength)
			{
				g.DrawString(lengthText, this.LengthFont, this.DarkTextBrush, new PointF(left + size1.Width - 10, top1 + (3 + (size1.Height - size2.Height) / 2)));
			}

			//
			// Draw the year.
			//
			string yearText = $"Year {this.BiblicalEvent.StartYear}";
			SizeF yearTextSize = g.MeasureString(yearText, this.StartYearFont);
			float yearTextTop = 5 + this.Rectangle.Top + (this.Rectangle.Height - yearTextSize.Height) / 2.0F;
			g.DrawString(yearText, this.StartYearFont, this.YearBrush, new PointF(left, yearTextTop));

			//
			// Draw the scripture reference.
			//
			SizeF refTextSize = g.MeasureString(this.BiblicalEvent.Reference, this.PageDefinition.Theme.ReferenceFont);
			float refTextTop = this.Rectangle.Bottom - refTextSize.Height + 2;
			g.DrawString(this.BiblicalEvent.Reference, this.ReferenceFont, this.ReferenceBrush, new PointF(left, refTextTop));

			//
			// If this item is placed at the top of the page, draw a line from the predecessor
			// to this objct.
			//
			if (this.BiblicalEvent.ResetTop)
			{
				//
				// Draw a line with an arrow from the predecessor to this item.
				//
				g.DrawLine(this.JumpLinePen, this.Rectangle.Left, this.Rectangle.Bottom, this.Rectangle.Left, this.Predecessor.Rectangle.Top);
			}

			return Task.CompletedTask;
		}

		public override string ToString() => this.BiblicalEvent?.Name;
	}
}
