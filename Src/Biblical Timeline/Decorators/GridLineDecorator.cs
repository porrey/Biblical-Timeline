using System.Drawing;

namespace Biblical.Timeline
{
	public static class GridLineDecorator
	{
		public static Task<IEnumerable<GridLine>> CreateGridLinesAsync(this TimelineParameters timelineParameters, PageDefinition pageDefinition)
		{
			IList<GridLine> returnValue = [];

			//
			// Create the year marker lines.
			//
			int y = 0;

			for (int i = timelineParameters.YearDivisions; i < timelineParameters.TotalYears; i += timelineParameters.YearDivisions)
			{
				GridLine gridLine = new(i, pageDefinition, timelineParameters);
				returnValue.Add(gridLine);
				y++;
			}

			return Task.FromResult<IEnumerable<GridLine>>(returnValue);
		}

		public static async Task DrawGridLinesAsync(this IEnumerable<GridLine> items, Graphics graphics)
		{
			//
			// Create the year marker lines.
			//
			foreach (GridLine gridLine in items.OrderBy(t => t.Year))
			{
				await gridLine.DrawAsync(graphics);
			}
		}
	}
}
