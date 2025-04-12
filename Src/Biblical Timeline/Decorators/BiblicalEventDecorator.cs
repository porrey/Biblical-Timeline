namespace Biblical.Timeline
{
    internal static class BiblicalEventDecorator
    {
        public static void Renumber(this IEnumerable<BiblicalEvent> items)
        {
            int number = 1;
            int expectedNumber = 1;

            foreach (BiblicalEvent item in items)
            {
                item.Sequence = number++;
                expectedNumber++;
            }
        }

        public static int MaximumVerticalCount(this IEnumerable<BiblicalEvent> items)
        {
            int returnValue = 0;

            int count = 0;

            foreach (BiblicalEvent item in items)
            {
                if (item.ResetTop)
                {
                    if (count > returnValue)
                    {
                        returnValue = count;
                    }

                    count = 1;
                }
                else
                {
                    count++;
                }
            }

            return returnValue;
        }

        public static ImageObjectTemplate ToImageObjectTemplate(this BiblicalEvent biblicalEvent, PageDefinition pageDefinition, TimelineParameters parameters)
        {
            ImageObjectTemplate returnValue = null;

            switch (biblicalEvent.EntryType)
            {
                case EntryType.Person:
                    returnValue = new PersonImageObject(biblicalEvent, pageDefinition, parameters);
                    break;
                case EntryType.TimeMarker:
                    returnValue = new TimeMarkerImageObject(biblicalEvent, pageDefinition, parameters);
                    break;
                case EntryType.TimeSpan:
                    returnValue = new TimeSpanImageObject(biblicalEvent, pageDefinition, parameters);
                    break;
                case EntryType.IsraelKing:
                    returnValue = new IsraelKingImageObject(biblicalEvent, pageDefinition, parameters);
                    break;
                case EntryType.JudahKing:
                    returnValue = new JudahKingImageObject(biblicalEvent, pageDefinition, parameters);
                    break;
            }

            return returnValue;
        }
    }
}
