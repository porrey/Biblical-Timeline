using System.Drawing;

namespace Biblical.Timeline
{
	internal class TimeSpanImageObject(BiblicalEvent biblicalEvent, PageDefinition pageDefinition, TimelineParameters parameters) : ImageObjectTemplate(biblicalEvent, pageDefinition, parameters)
	{
		protected override Brush FillBrush => this.PageDefinition.Theme.BarLightBackgroundBrush;
		protected override Pen BorderPen => this.PageDefinition.Theme.BarLightBorderPen;
	}
}
