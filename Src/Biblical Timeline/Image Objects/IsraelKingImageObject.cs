using System.Drawing;
using Biblical.Timeline.Themes;

namespace Biblical.Timeline
{
	public class IsraelKingImageObject(BiblicalEvent biblicalEvent, PageDefinition pageDefinition, TimelineParameters parameters) : ImageObjectTemplate(biblicalEvent, pageDefinition, parameters)
	{
		protected override Brush OnGetBrush(StyleName styleName)
		{
			if (styleName == StyleName.Item1)
			{
				return this.Styles[StyleName.Extra1].Brush;
			}
			else
			{
				return base.OnGetBrush(styleName);
			}
		}
	}
}
