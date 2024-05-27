namespace Biblical.Timeline
{
	internal static class ImageObjectDecorator
	{
		public static ImageObjectTemplate GetPredecessor(this IEnumerable<ImageObjectTemplate> imageObjects, ImageObjectTemplate imageObject)
		{
			ImageObjectTemplate returnValue = null;

			foreach (ImageObjectTemplate item in imageObjects.Where(t => t.BiblicalEvent.Sequence != imageObject.BiblicalEvent.Sequence))
			{
				if (item.BiblicalEvent.Name == imageObject.BiblicalEvent.Predecessor)
				{
					returnValue = item;
					break;
				}
			}

			return returnValue;
		}

		public static float GetLeftPosition(this ImageObjectTemplate imageObject, float pixelsPerYear)
		{
			float returnValue = 0;

			ImageObjectTemplate predecessor = imageObject.Predecessor;

			if (imageObject.Predecessor != null)
			{
				returnValue += imageObject.Predecessor.Rectangle.Left;
			}

			returnValue += (int)(imageObject.BiblicalEvent.EventStart * pixelsPerYear);

			return returnValue;
		}
	}
}
