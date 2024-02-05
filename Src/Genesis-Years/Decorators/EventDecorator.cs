﻿using System.Drawing;

namespace Genesis.Years
{
	public class EventDecorator
	{
		public EventDecorator(BiblicalEvent person)
		{
			this.Person = person;
		}

		public BiblicalEvent Person { get; protected set; }
		public EventDecorator Predecessor { get; set; }

		public RectangleF Rectangle { get; set; }

		public override string ToString() => this.Person?.Name;
	}
}