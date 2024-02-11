using System.Drawing;

namespace Genesis.Years
{
	public class EventDecorator
	{
		public EventDecorator(BiblicalEvent eventItem)
		{
			this.EventItem = eventItem;
		}

		public BiblicalEvent EventItem { get; protected set; }
		public EventDecorator Predecessor { get; set; }

		public RectangleF Rectangle { get; set; }

		public override string ToString() => this.EventItem?.Name;
	}
}
