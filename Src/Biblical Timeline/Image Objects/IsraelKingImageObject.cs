using System.Drawing;

namespace Biblical.Timeline
{
	internal class IsraelKingImageObject(BiblicalEvent biblicalEvent, PageDefinition pageDefinition, TimelineParameters parameters) : ImageObjectTemplate(biblicalEvent, pageDefinition, parameters)
	{
		protected override Brush FillBrush => this.PageDefinition.Theme.BarIsraelKingBackgroundBrush;
		protected override Pen BorderPen => this.PageDefinition.Theme.BarIsraelKingBorderPen;
	}
}
