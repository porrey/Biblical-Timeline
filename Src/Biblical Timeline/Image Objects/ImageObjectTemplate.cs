using System.Drawing;
using Biblical.Timeline.Themes;

namespace Biblical.Timeline
{
	internal abstract class ImageObjectTemplate(BiblicalEvent biblicalEvent, PageDefinition pageDefinition, TimelineParameters parameters)
	{
		public BiblicalEvent BiblicalEvent { get; protected set; } = biblicalEvent;
		protected PageDefinition PageDefinition { get; } = pageDefinition;
		protected TimelineParameters TimelineParameters { get; } = parameters;
		public ImageObjectTemplate Predecessor { get; set; }
		public RectangleF Rectangle { get; set; }
		public IDictionary<StyleName, IThemeStyle> Styles => this.PageDefinition.Theme.Styles;

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
				SizeF size1 = this.PageDefinition.Graphics.MeasureString(this.BiblicalEvent.Name, this.OnGetFont(StyleName.Header1));
				SizeF size2 = this.PageDefinition.Graphics.MeasureString("(?)", this.OnGetFont(StyleName.Text));

				returnValue = (int)(size1.Width + size2.Width);
			}

			return Task.FromResult(returnValue);
		}

		protected virtual Task OnDraw(Graphics g)
		{
			//
			// Draw the rectangle.
			//
			g.FillRectangle(this.OnGetBrush(StyleName.Item1), this.Rectangle);
			g.DrawRectangle(this.OnGetPen(StyleName.ItemBorder), this.Rectangle);

			//
			// Measure the text.
			//
			SizeF size1 = g.MeasureString(this.BiblicalEvent.Name, this.OnGetFont(StyleName.Header1));

			//
			// Measure the text.
			//
			string lengthText = $" ({(this.BiblicalEvent.EventLength == -1 ? "?" : this.BiblicalEvent.EventLength)})";
			SizeF size2 = g.MeasureString(lengthText, this.OnGetFont(StyleName.Header1));

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
			g.DrawString(this.BiblicalEvent.Name, this.OnGetFont(StyleName.Header1), this.OnGetBrush(StyleName.Header1), new PointF(left, top1));

			//
			// Add the event time span if requested.
			//
			if (this.BiblicalEvent.DisplayEventLength)
			{
				g.DrawString(lengthText, this.OnGetFont(StyleName.Text), this.OnGetBrush(StyleName.Text), new PointF(left + size1.Width - 20, top1 + (3 + (size1.Height - size2.Height) / 2)));
			}

			//
			// Draw the year.
			//
			string yearText = $"Year {this.BiblicalEvent.StartYear}";
			SizeF yearTextSize = g.MeasureString(yearText, this.OnGetFont(StyleName.Header2));
			float yearTextTop = 5 + this.Rectangle.Top + (this.Rectangle.Height - yearTextSize.Height) / 2.0F;
			g.DrawString(yearText, this.OnGetFont(StyleName.Header2), this.OnGetBrush(StyleName.Header2), new PointF(left, yearTextTop));

			//
			// Draw the scripture reference.
			//
			SizeF refTextSize = g.MeasureString(this.BiblicalEvent.Reference, this.OnGetFont(StyleName.Header3));
			float refTextTop = this.Rectangle.Bottom - refTextSize.Height + 2;
			g.DrawString(this.BiblicalEvent.Reference, this.OnGetFont(StyleName.Header3), this.OnGetBrush(StyleName.Header3), new PointF(left, refTextTop));

			//
			// If this item is placed at the top of the page, draw a line from the predecessor
			// to this objct.
			//
			if (this.BiblicalEvent.ResetTop)
			{
				//
				// Draw a line with an arrow from the predecessor to this item.
				//
				g.DrawLine(this.OnGetPen(StyleName.JumpLine), this.Rectangle.Left, this.Rectangle.Bottom, this.Rectangle.Left, this.Predecessor.Rectangle.Top);
			}

			return Task.CompletedTask;
		}

		protected virtual Pen OnGetPen(StyleName styleName)
		{
			return this.Styles[styleName].Pen;
		}

		protected virtual Brush OnGetBrush(StyleName styleName)
		{
			return this.Styles[styleName].Brush;
		}

		protected virtual Font OnGetFont(StyleName styleName)
		{
			return this.Styles[styleName].Font;
		}

		public override string ToString() => this.BiblicalEvent?.Name;
	}
}
