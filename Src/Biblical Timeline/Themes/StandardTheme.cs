using System.Drawing;
using System.Drawing.Drawing2D;

namespace Biblical.Timeline
{
	/// <summary>
	/// Fonts and Theme.
	/// </summary>
	public class StandardTheme
	{
		public readonly Font TitleFont = new("Cambria", 32F, FontStyle.Regular, GraphicsUnit.Point);
		public readonly Font YearsFont = new("Calibri Light", 9F, FontStyle.Italic, GraphicsUnit.Point);
		public readonly Font NameFont = new("Calibri", 6F, FontStyle.Bold, GraphicsUnit.Point);
		public readonly Font LengthFont = new("Calibri", 4F, FontStyle.Regular, GraphicsUnit.Point);
		public readonly Font MarkerFont = new("Calibri", 6F, FontStyle.Bold, GraphicsUnit.Point);
		public readonly Font ReferenceFont = new("Calibri Light", 3.5F, FontStyle.Italic, GraphicsUnit.Point);

		public readonly Brush PageBackgroundBrush = Brushes.White;
		public readonly Brush TitleBrush = Brushes.DarkOliveGreen;
		public readonly Brush BorderBrush = Brushes.DarkOliveGreen;
		public readonly Brush YearsBrush = Brushes.DarkOliveGreen;
		public readonly Brush VerticalLineBrush = Brushes.LightGray;
		public readonly Brush MarkerLineBrush = Brushes.LightBlue;
		public readonly Brush MarkerTextBrush = Brushes.DarkOliveGreen;
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

		public Pen MarkerLinePen => new(this.MarkerLineBrush, 2.95F);

		public Pen JumpLinePen => new(this.MarkerLineBrush, 4.5F)
		{
			StartCap = LineCap.ArrowAnchor,
			EndCap = LineCap.RoundAnchor
		};
	}
}
