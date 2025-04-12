namespace Biblical.Timeline
{
    public class LayoutManager
    {
        public async Task ComputeLayoutAsync(IEnumerable<ImageObjectTemplate> imageObjects, TimelineParameters timelineParameters, PageDefinition pageDefinition)
        {
            float currentTopPosition = timelineParameters.TopPosition;

            //
            // Calculate the position of each object.
            //
            foreach (ImageObjectTemplate item in imageObjects)
            {
                currentTopPosition = await this.ComputeItemLayoutAsync(item, currentTopPosition, timelineParameters, pageDefinition);
            }
        }

        public async Task<float> ComputeItemLayoutAsync(ImageObjectTemplate item, float currentTopPosition, TimelineParameters timelineParameters, PageDefinition pageDefinition)
        {
            if (item.Predecessor != null && item.Predecessor.BiblicalEvent.StartYear == 0 && item.Predecessor.Predecessor != null)
            {
                //
                // The predecessor is not yet positioned. Compute the predecessor's position.
                //
                _ = await this.ComputeItemLayoutAsync(item.Predecessor, currentTopPosition, timelineParameters, pageDefinition);
            }

            //
            // If the item is a reset, set the top position to the top of the drawable area.
            //
            if (item.BiblicalEvent.ResetTop)
            {
                currentTopPosition = timelineParameters.TopPosition;
            }

            //
            // Get the item length in pixels.
            //
            float eventLength = await item.MeasureAsync(pageDefinition.Graphics);

            //
            // The item length is unknown (-1) and the predecessor is not null.
            //
            if (item.Predecessor == null)
            {
                //
                // A person without a predecessor is at the left margin.
                //
                item.BiblicalEvent.StartYear = 0;
                item.Rectangle = new(pageDefinition.DrawableArea.Left + 1, currentTopPosition, eventLength, timelineParameters.PersonBarHeight);
            }
            else
            {
                //
                // The left position is dependent on predecessors.
                //
                float left = item.GetLeftPosition(timelineParameters.PixelsPerYear);
                item.Rectangle = new(left, currentTopPosition, eventLength, timelineParameters.PersonBarHeight);
                item.BiblicalEvent.StartYear = (int)(item.Predecessor.BiblicalEvent.StartYear + item.BiblicalEvent.EventStart);
            }

            //
            // Set the top position for the next item.
            //
            currentTopPosition += timelineParameters.BarMargin + timelineParameters.PersonBarHeight;

            return currentTopPosition;
        }
    }
}