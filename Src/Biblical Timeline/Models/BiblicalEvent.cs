using Newtonsoft.Json;

namespace Biblical.Timeline
{
    public enum EntryType
    {
        Person,
        TimeMarker,
        TimeSpan,
        JudahKing,
        IsraelKing
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
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Include)]
        public bool ResetTop { get; set; } = false;

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Include)]
        public int Sequence { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Include)]
        public float EventLength { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Include)]
        public string Predecessor { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Include)]
        public float EventStart { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Include)]
        public bool DisplayEventLength { get; set; } = true;

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Include)]
        public string Reference { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Include)]
        public EntryType EntryType { get; set; } = EntryType.Person;

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Include)]
        public string Comments { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Include)]
        public TextAlign TextAlign { get; set; } = TextAlign.Right;

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public bool AdEvent { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public bool Show { get; set; } = true;

        public override string ToString() => this.Name;

        [JsonIgnore]
        public int StartYear { get; set; }
    }
}
