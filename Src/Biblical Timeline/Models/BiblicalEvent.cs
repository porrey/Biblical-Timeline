namespace Biblical.Timeline
{
	public enum EntryType
	{
		Person,
		TimeMarker,
		TimeSpan
	}

	public enum Style
	{
		Dark,
		Light
	}

	public enum TextAlign
	{
		Left,
		Right
	}

	public class BiblicalEvent
	{
		public bool ResetTop { get; set; }
		public int Sequence { get; set; }
		public string Name { get; set; }
		public int EventLength { get; set; }
		public string Predecessor { get; set; }
		public int EventStart { get; set; }
		public bool DisplayEventLength { get; set; } = true;
		public string Reference { get; set; }
		public EntryType EntryType { get; set; } = EntryType.Person;
		public string Comments { get; set; }
		public TextAlign TextAlign { get; set; } = TextAlign.Right;
		public override string ToString() => this.Name;
	}
}
