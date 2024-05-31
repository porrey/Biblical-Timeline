using System.Drawing;

namespace Biblical.Timeline
{
	internal class TimelineParameters
	{
		public TimelineParameters(RectangleF drawableArea, int maximumVerticalItemCount, int yearDivisions, int totalYears, int barMargin) 
		{
			this.YearDivisions= yearDivisions;
			this.TotalYears= totalYears;
			this.BarMargin = barMargin;
			this.TopPosition = drawableArea.Top + barMargin + 100;
			this.PersonBarHeight = ((drawableArea.Bottom - this.TopPosition) / maximumVerticalItemCount) - barMargin;
			this.PixelsPerYear = (float)drawableArea.Width / (float)totalYears;
		}

		public int YearDivisions { get; }
		public int TotalYears { get; }
		public int BarMargin { get; }
		public float TopPosition { get; }
		public float PersonBarHeight { get; }
		public float PixelsPerYear { get; }
	}
}
