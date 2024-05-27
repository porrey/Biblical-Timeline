using System.Drawing;
using System.Drawing.Imaging;

namespace Biblical.Timeline
{
	internal class PageDefinition : DisposableObject
	{
		public PageDefinition(string title, float pageWidth, float pageHeight, float resolution, float margin, StandardTheme theme, LayoutManager layoutManager)
		{
			this.Title = title;
			this.Width = pageWidth;
			this.Height = pageHeight;
			this.Resolution = resolution;
			this.Margin = margin;
			this.Page = new(0, 0, pageWidth * resolution, pageHeight * resolution);
			this.DrawableArea = new(margin, margin, this.Page.Width - (2 * margin), this.Page.Height - (2 * margin));
			this.Theme = theme;
			this.LayoutManager = layoutManager;

			this.Bitmap = new((int)this.Page.Width, (int)this.Page.Height);
			this.Bitmap.SetResolution(this.Resolution, this.Resolution);
			this.Graphics = Graphics.FromImage(this.Bitmap);
		}

		public string Title { get; }
		public float Width { get; }
		public float Height { get; }
		public float Resolution { get; }
		public float Margin { get; }
		public StandardTheme Theme { get; }
		public LayoutManager LayoutManager { get; }

		public RectangleF Page { get; }
		public RectangleF DrawableArea { get; }

		public Bitmap Bitmap { get; protected set; }
		public Graphics Graphics { get; protected set; }

		public Task SaveImageAsync(string imageFile, ImageFormat imageFormat)
		{
			this.Bitmap.Save(imageFile, imageFormat);
			return Task.CompletedTask;
		}

		public async Task DrawAsync(TimelineParameters timelineParameters, IEnumerable<ImageObjectTemplate> imageObjects)
		{
			//
			// Compute the layout.
			//
			await this.LayoutManager.ComputeLayoutAsync(imageObjects, timelineParameters, this);

			//
			// Fill the page background with white.
			//
			this.Graphics.FillRectangle(this.Theme.PageBackgroundBrush, this.Page);

			//
			// Draw the year marker lines.
			//
			for (int i = timelineParameters.YearDivisions; i < timelineParameters.TotalYears; i += timelineParameters.YearDivisions)
			{
				//
				// Get the size of the text.
				//
				SizeF size = this.Graphics.MeasureString(i.ToString(), this.Theme.YearsFont);

				//
				// Calculate the left position.
				//
				float left = this.Margin + (int)((float)i / (float)timelineParameters.TotalYears * this.DrawableArea.Width);

				//
				// Draw the vertical line.
				//
				this.Graphics.DrawLine(this.Theme.VerticalLinePen, new PointF(left, (int)(this.Margin + size.Height + 1)), new PointF(left, this.DrawableArea.Bottom));

				//
				// Draw the year text.
				//
				int textLeft = (int)(left - (size.Width / 2F));
				this.Graphics.DrawString(i.ToString(), this.Theme.YearsFont, this.Theme.YearsBrush, new PointF(textLeft, this.Margin));
			}

			//
			// Print the page title in the bottom left.
			//
			SizeF titleSize = this.Graphics.MeasureString(this.Title, this.Theme.TitleFont);
			float titleLeft = this.Margin + 50;
			float titleTop = this.DrawableArea.Bottom - titleSize.Height - 50;
			this.Graphics.DrawString(this.Title, this.Theme.TitleFont, this.Theme.TitleBrush, new PointF(titleLeft, titleTop));

			//
			// Draw a border at the margins.
			//
			this.Graphics.DrawRectangle(this.Theme.BorderPen, this.DrawableArea);

			//
			// Draw the objects.
			//
			foreach (ImageObjectTemplate i in imageObjects)
			{
				await i.DrawAsync(this.Graphics);
			}
		}

		protected override void OnDisposeManagedObjects()
		{
			if (this.Graphics != null)
			{
				this.Graphics.Dispose();
				this.Graphics = null;
			}

			if (this.Bitmap != null)
			{
				this.Bitmap.Dispose();
				this.Bitmap = null;
			}
		}
	}
}
