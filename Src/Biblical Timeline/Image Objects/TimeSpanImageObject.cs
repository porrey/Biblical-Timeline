using System.Drawing;
using Biblical.Timeline.Themes;

namespace Biblical.Timeline
{
	internal class TimeSpanImageObject(BiblicalEvent biblicalEvent, PageDefinition pageDefinition, TimelineParameters parameters) : ImageObjectTemplate(biblicalEvent, pageDefinition, parameters)
	{
		protected override Brush OnGetBrush(StyleName styleName)
		{
			if (styleName == StyleName.Item1)
			{
				return this.Styles[StyleName.Item2].Brush;
			}
			else
			{
				return base.OnGetBrush(styleName);
			}
		}
	}
}
