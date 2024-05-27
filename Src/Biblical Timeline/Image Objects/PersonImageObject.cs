using System.Drawing;

namespace Biblical.Timeline
{
	internal class PersonImageObject(BiblicalEvent biblicalEvent, PageDefinition pageDefinition, TimelineParameters parameters) : ImageObjectTemplate(biblicalEvent, pageDefinition, parameters)
	{
		protected override Brush FillBrush => this.PageDefinition.Theme.BarDarkBackgroundBrush;
		protected override Pen BorderPen => this.PageDefinition.Theme.BarDarkBorderPen;
	}
}
