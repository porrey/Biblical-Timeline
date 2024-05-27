using System.Drawing;

namespace Biblical.Timeline
{
	internal class LayoutManager
	{
		public async Task ComputeLayoutAsync(IEnumerable<ImageObjectTemplate> imageObjects, TimelineParameters timelineParameters, PageDefinition pageDefinition)
		{
			float currentTopPosition = timelineParameters.TopPosition;

			//
			// Calculate the position of each object.
			//
			foreach (ImageObjectTemplate item in imageObjects.OrderBy(t => t.BiblicalEvent.Sequence))
			{
				if (item.BiblicalEvent.ResetTop)
				{
					currentTopPosition = timelineParameters.TopPosition;
				}

				//
				// Get the item length in pixels.
				//
				float eventLength = await item.MeasureAsync(pageDefinition.Graphics);

				if (item.Predecessor == null)
				{
					//
					// A person without a predecessor is at the left margin.
					//
					item.Rectangle = new(pageDefinition.DrawableArea.Left + 1, currentTopPosition, eventLength, timelineParameters.PersonBarHeight);
				}
				else
				{
					//
					// The left position is dependent on predecessors.
					//
					float left = item.GetLeftPosition(timelineParameters.PixelsPerYear);
					item.Rectangle = new(left, currentTopPosition, eventLength, timelineParameters.PersonBarHeight);
				}

				currentTopPosition += timelineParameters.BarMargin + timelineParameters.PersonBarHeight;
			}
		}
	}
}
