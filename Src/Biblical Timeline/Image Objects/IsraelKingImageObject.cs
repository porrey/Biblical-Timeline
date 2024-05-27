using System.Drawing;

namespace Biblical.Timeline
{
	internal class IsraelKingImageObject(BiblicalEvent biblicalEvent, PageDefinition pageDefinition, TimelineParameters parameters) : ImageObjectTemplate(biblicalEvent, pageDefinition, parameters)
	{
		protected override Task OnDraw(Graphics g)
		{
			//
			// Draw the rectangle.
			//
			g.FillRectangle(this.PageDefinition.Theme.BarIsraelKingBackgroundBrush, this.Rectangle);
			g.DrawRectangle(this.PageDefinition.Theme.BarIsraelKingBorderPen, this.Rectangle);

			//
			// Get the minimum txt to be displayed. Optional text may be added.
			//
			string text = this.BiblicalEvent.Name;

			//
			// Measure the text.
			//
			SizeF size1 = g.MeasureString(text, this.PageDefinition.Theme.NameFont);

			//
			// Caclulate the text position and draw the text.
			//
			float textLeft = size1.Width > this.Rectangle.Width ? this.Rectangle.Right + 1 : this.Rectangle.Left + 1;
			float top1 = this.Rectangle.Top + 1;
			g.DrawString(text, this.PageDefinition.Theme.NameFont, this.PageDefinition.Theme.BarIsraelKingTextBrush, new PointF(textLeft, top1));

			//
			// Add the event time span if requested.
			//
			if (this.BiblicalEvent.DisplayEventLength)
			{
				string lengthText = $" ({this.BiblicalEvent.EventLength})";

				if (this.BiblicalEvent.EventLength == -1)
				{
					lengthText = "(?)";
				}

				SizeF size2 = g.MeasureString(lengthText, this.PageDefinition.Theme.LengthFont);
				g.DrawString(lengthText, this.PageDefinition.Theme.LengthFont, this.PageDefinition.Theme.BarIsraelKingTextBrush, new PointF(textLeft + size1.Width - 10, top1 + ((size1.Height - size2.Height) / 2)));
			}

			//
			// Draw the scripture reference.
			//
			SizeF refTextSize = g.MeasureString(this.BiblicalEvent.Reference, this.PageDefinition.Theme.ReferenceFont);
			float refTextLeft = size1.Width > this.Rectangle.Width ? this.Rectangle.Right + 1 : this.Rectangle.Left + 1;
			float refTextTop = this.Rectangle.Bottom - refTextSize.Height + 2;
			g.DrawString(this.BiblicalEvent.Reference, this.PageDefinition.Theme.ReferenceFont, this.PageDefinition.Theme.ReferenceBrush, new PointF(refTextLeft, refTextTop));

			//
			// If this item is placed at the top of the page, draw a line from the predecessor
			// to this objct.
			//
			if (this.BiblicalEvent.ResetTop)
			{
				//
				// Draw a line with an arrow from the predecessor to this item.
				//
				g.DrawLine(this.PageDefinition.Theme.JumpLinePen, this.Rectangle.Left, this.Rectangle.Bottom, this.Rectangle.Left, this.Predecessor.Rectangle.Top);
			}

			return Task.CompletedTask;
		}
	}
}
