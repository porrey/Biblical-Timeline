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

		public virtual Task DrawAsync(Graphics g)
		{
			return this.OnDraw(g);
		}

		protected virtual Task OnDraw(Graphics g)
		{
			throw new NotImplementedException();
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

		public override string ToString() => this.BiblicalEvent?.Name;
	}
}
