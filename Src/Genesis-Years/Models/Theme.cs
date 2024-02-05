using System.Drawing;

namespace Genesis.Years.Models
{
	/// <summary>
	/// Fonts and Theme.
	/// </summary>
	public class Theme
	{
		public readonly Font TitleFont = new("Cambria", 32F, FontStyle.Regular, GraphicsUnit.Point);
		public readonly Font YearsFont = new("Calibri Light", 9F, FontStyle.Italic, GraphicsUnit.Point);
		public readonly Font NameFont = new("Calibri", 7F, FontStyle.Bold, GraphicsUnit.Point);
		public readonly Font MarkerFont = new("Calibri", 10.5F, FontStyle.Bold, GraphicsUnit.Point);
		public readonly Font ReferenceFont = new("Calibri Light", 5.5F, FontStyle.Italic, GraphicsUnit.Point);

		public readonly Brush PageBackgroundBrush = Brushes.White;
		public readonly Brush TitleBrush = Brushes.DarkOliveGreen;
		public readonly Brush BorderBrush = Brushes.DarkOliveGreen;
		public readonly Brush YearsBrush = Brushes.DarkOliveGreen;
		public readonly Brush VerticalLineBrush = Brushes.DarkGray;
		public readonly Brush MarkerBrush = Brushes.DarkOliveGreen;
		public readonly Brush ReferenceBrush = Brushes.Black;

		public readonly Pen BarDarkBorderPen = Pens.Black;
		public readonly Brush BarDarkTextBrush = Brushes.Black;
		public readonly Brush BarDarkBackgroundBrush = Brushes.LightBlue;
		public readonly Pen BarLightBorderPen = Pens.DarkOliveGreen;
		public readonly Brush BarLightTextBrush = Brushes.Black;
		public readonly Brush BarLightBackgroundBrush = Brushes.LightYellow;

		//
		// Pens
		//
		public Pen BorderPen => new(this.BorderBrush, 2.5F);

		public Pen VerticalLinePen => new(this.VerticalLineBrush, .5F)
		{
			DashPattern = [10, 10]
		};

		public Pen MarkerPen => new(this.MarkerBrush, 3.8F)
		{
			DashPattern = [5, 8]
		};
	}
}
