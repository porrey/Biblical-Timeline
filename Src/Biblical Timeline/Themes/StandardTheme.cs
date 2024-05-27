using System.Drawing;
using System.Drawing.Drawing2D;

namespace Biblical.Timeline
{
	/// <summary>
	/// Fonts and Theme.
	/// </summary>
	public class StandardTheme
	{
		public readonly Font TitleFont = new("Cambria", 30F, FontStyle.Regular, GraphicsUnit.Point);
		public readonly Font YearsFont = new("Calibri Light", 9F, FontStyle.Italic, GraphicsUnit.Point);
		public readonly Font NameFont = new("Calibri", 5F, FontStyle.Bold, GraphicsUnit.Point);
		public readonly Font LengthFont = new("Calibri", 4F, FontStyle.Regular, GraphicsUnit.Point);
		public readonly Font MarkerFont = new("Calibri", 6F, FontStyle.Bold, GraphicsUnit.Point);
		public readonly Font ReferenceFont = new("Calibri Light", 3.5F, FontStyle.Italic, GraphicsUnit.Point);
		public readonly Font StartYearFont = new("Calibri Light", 3.5F, FontStyle.Italic, GraphicsUnit.Point);

		public readonly Brush PageBackgroundBrush = Brushes.White;
		public readonly Brush TitleBrush = Brushes.DarkOliveGreen;
		public readonly Brush BorderBrush = Brushes.DarkOliveGreen;
		public readonly Brush YearsBrush = Brushes.DarkOliveGreen;
		public readonly Brush VerticalLineBrush = Brushes.LightGray;
		public readonly Brush MarkerLineBrush = Brushes.LightBlue;
		public readonly Brush MarkerTextBrush = Brushes.DarkOliveGreen;
		public readonly Brush ReferenceBrush = Brushes.Black;
		public readonly Brush YearBrush = Brushes.DarkOliveGreen;
		public readonly Brush JumpLineBrush = new SolidBrush(Color.FromArgb(50, Color.Red));

		public readonly Pen BarDarkBorderPen = Pens.Black;
		public readonly Brush BarDarkTextBrush = Brushes.Black;
		public readonly Brush BarDarkBackgroundBrush = Brushes.LightBlue;
		public readonly Pen BarLightBorderPen = Pens.DarkOliveGreen;
		public readonly Brush BarLightTextBrush = Brushes.Black;
		public readonly Brush BarLightBackgroundBrush = Brushes.LightYellow;

		public readonly Pen BarJudahKingBorderPen = Pens.MediumPurple;
		public readonly Brush BarJudahKingTextBrush = Brushes.Black;
		public readonly Brush BarJudahKingBackgroundBrush = Brushes.Lavender;

		public readonly Pen BarIsraelKingBorderPen = Pens.Purple;
		public readonly Brush BarIsraelKingTextBrush = Brushes.Black;
		public readonly Brush BarIsraelKingBackgroundBrush = Brushes.LavenderBlush;

		//
		// Pens
		//
		public Pen BorderPen => new(this.BorderBrush, 2.5F);

		public Pen VerticalLinePen => new(this.VerticalLineBrush, .5F)
		{
			DashPattern = [10, 10]
		};

		public Pen MarkerLinePen => new(this.MarkerLineBrush, 2.95F);

		public Pen JumpLinePen => new(this.JumpLineBrush, 2.1F)
		{
			StartCap = LineCap.ArrowAnchor,
			EndCap = LineCap.RoundAnchor,
			DashPattern = [3, 3]
		};
	}
}
