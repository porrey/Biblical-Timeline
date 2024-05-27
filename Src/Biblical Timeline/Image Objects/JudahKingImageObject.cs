using System.Drawing;

namespace Biblical.Timeline
{
	internal class JudahKingImageObject(BiblicalEvent biblicalEvent, PageDefinition pageDefinition, TimelineParameters parameters) : ImageObjectTemplate(biblicalEvent, pageDefinition, parameters)
	{
		protected override Brush FillBrush => this.PageDefinition.Theme.BarJudahKingBackgroundBrush;
		protected override Pen BorderPen => this.PageDefinition.Theme.BarJudahKingBorderPen;
	}
}
