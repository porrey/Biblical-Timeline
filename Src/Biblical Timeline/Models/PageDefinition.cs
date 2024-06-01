using System.Drawing;
using System.Drawing.Imaging;
using Biblical.Timeline.Themes;

namespace Biblical.Timeline
{
	public class PageDefinition : DisposableObject
	{
		public PageDefinition(string title, float pageWidth, float pageHeight, float resolution, float margin, ITheme theme, LayoutManager layoutManager)
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
		public ITheme Theme { get; }
		public LayoutManager LayoutManager { get; }
		public IEnumerable<GridLine> GridLines { get; set; }

		public RectangleF Page { get; }
		public RectangleF DrawableArea { get; }

		public Bitmap Bitmap { get; protected set; }
		public Graphics Graphics { get; protected set; }

		public Task SaveImageAsync(string imageFile, ImageFormat imageFormat)
		{
			this.Bitmap.Save(imageFile, imageFormat);
			return Task.CompletedTask;
		}

		public async Task BuildAsync(TimelineParameters timelineParameters, IEnumerable<ImageObjectTemplate> imageObjects)
		{
			//
			// Compute the layout.
			//
			await this.LayoutManager.ComputeLayoutAsync(imageObjects, timelineParameters, this);

			//
			// Create the year marker lines.
			//
			this.GridLines = await timelineParameters.CreateGridLinesAsync(this);
		}

		public async Task DrawAsync(TimelineParameters timelineParameters, IEnumerable<ImageObjectTemplate> imageObjects)
		{
			//
			// Fill the page background with white.
			//
			this.Graphics.FillRectangle(this.Theme.Styles[StyleName.Background].Brush, this.Page);

			//
			// Draw the grid lines.
			//
			await this.GridLines.DrawGridLinesAsync(this.Graphics);

			//
			// Print the page title in the bottom left.
			//
			SizeF titleSize = this.Graphics.MeasureString(this.Title, this.Theme.Styles[StyleName.Title].Font);
			float titleLeft = this.Margin + 50;
			float titleTop = this.DrawableArea.Bottom - titleSize.Height - 150;
			this.Graphics.DrawString(this.Title, this.Theme.Styles[StyleName.Title].Font, this.Theme.Styles[StyleName.Title].Brush, new PointF(titleLeft, titleTop));

			//
			// Draw a border at the margins.
			//
			this.Graphics.DrawRectangle(this.Theme.Styles[StyleName.GridBorder].Pen, this.DrawableArea);

			//
			// Draw the objects.
			//
			foreach (ImageObjectTemplate imageObject in imageObjects)
			{
				await imageObject.DrawAsync(this.Graphics);
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
