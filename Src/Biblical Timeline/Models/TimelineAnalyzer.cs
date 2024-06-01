namespace Biblical.Timeline
{
	public class TimelineAnalyzer
	{
		public TimelineAnalyzer()
		{
		}

		public Task CalculateBc(IEnumerable<BiblicalEvent> biblicalEvents, IEnumerable<GridLine> gridLines)
		{
			//
			// Find the AD 1 event.
			//
			BiblicalEvent item = biblicalEvents.Where(t => t.AdEvent).SingleOrDefault();

			if (item != null)
			{
				//
				// Get the grid line just before this event.
				//
				GridLine gridLine1 = gridLines.Where(t => Math.Abs(t.Year - item.StartYear) < 100).FirstOrDefault();

				//
				// BC
				//
				int bc = item.StartYear - gridLine1.Year;
				gridLine1.BottomLabel = $"{bc} BC";

				foreach (GridLine gridLine in gridLines.Where(t => t.Year < gridLine1.Year).OrderByDescending(t => t.Year))
				{
					bc += 100;
					gridLine.BottomLabel = $"{bc} BC";
				}

				//
				// Get the grid line just after this event.
				//
				GridLine gridLine2 = gridLines.Where(t => t.Year > gridLine1.Year && Math.Abs(t.Year - item.StartYear) < 100).FirstOrDefault();

				//
				// Ad
				//
				int ad = gridLine2.Year - item.StartYear;
				gridLine2.BottomLabel = $"{ad} AD";

				foreach (GridLine gridLine in gridLines.Where(t => t.Year > gridLine2.Year).OrderBy(t => t.Year))
				{
					ad += 100;
					gridLine.BottomLabel = $"{ad} AD";
				}
			}

			return Task.CompletedTask;
		}
	}
}
