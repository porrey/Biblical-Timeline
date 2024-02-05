namespace Genesis.Years
{
	public enum EntryType
	{
		Person,
		TimeMarker
	}

	public enum Style
	{
		Dark,
		Light
	}

	public class BiblicalEvent
	{
		public int Sequence { get; set; }
		public string Name { get; set; }
		public int EventLength { get; set; }
		public string Predecessor { get; set; }
		public int EventStart { get; set; }
		public bool DisplayYears { get; set; } = true;
		public string Reference { get; set; }
		public EntryType EntryType { get; set; } = EntryType.Person;
		public Style Style { get; set; } = Style.Dark;
		public string Comments { get; set; }

		public override string ToString() => this.Name;
	}
}
