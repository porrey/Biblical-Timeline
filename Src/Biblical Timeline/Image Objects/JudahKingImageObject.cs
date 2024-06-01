using System.Drawing;
using Biblical.Timeline.Themes;

namespace Biblical.Timeline
{
	public class JudahKingImageObject(BiblicalEvent biblicalEvent, PageDefinition pageDefinition, TimelineParameters parameters) : ImageObjectTemplate(biblicalEvent, pageDefinition, parameters)
	{
		protected override Brush OnGetBrush(StyleName styleName)
		{
			if (styleName == StyleName.Item1)
			{
				return this.Styles[StyleName.Item3].Brush;
			}
			else
			{
				return base.OnGetBrush(styleName);
			}
		}
	}
}
