namespace Genesis.Years
{
	public static class EventsDecorator
	{
		public static EventDecorator GetPredecessor(this IEnumerable<EventDecorator> events, EventDecorator eventItem)
		{
			EventDecorator returnValue = null;

			foreach (EventDecorator item in events.Where(t => t.EventItem.Sequence != eventItem.EventItem.Sequence))
			{
				if (item.EventItem.Name == eventItem.EventItem.Predecessor)
				{
					returnValue = item;
					break;
				}
			}

			return returnValue;
		}

		public static float GetLeftPosition(this EventDecorator eventItem, float pixelsPerYear)
		{
			float returnValue = 0;

			EventDecorator predecessor = eventItem.Predecessor;

			if (eventItem.Predecessor != null)
			{
				returnValue += eventItem.Predecessor.Rectangle.Left;
			}

			returnValue += (int)(eventItem.EventItem.EventStart * pixelsPerYear);

			return returnValue;
		}
	}
}
