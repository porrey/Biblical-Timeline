using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using Genesis.Years.Models;
using Newtonsoft.Json;

namespace Genesis.Years
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//
			// Load the people
			//
			string file = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}/events.json";
			string json = File.ReadAllText(file);
			IEnumerable<BiblicalEvent> events = JsonConvert.DeserializeObject<IEnumerable<BiblicalEvent>>(json);

			//
			// Define page parameters.
			//
			float pageWidth = 22F;
			float pageHeight = 11F;
			float resolution = 300F;
			float margin = .45F * resolution;
			RectangleF page = new(0, 0, pageWidth * resolution, pageHeight * resolution);
			RectangleF margins = new(margin, margin, page.Width - (2 * margin), page.Height - (2 * margin));

			//
			// Define time line parameters.
			//
			int yearDivisions = 100;
			int totalYears = 46 * yearDivisions;
			int personBarMargin = 15;
			float startingTop = margins.Top + 1 + personBarMargin + 50;
			float personBarHeight = ((margins.Bottom - startingTop) / events.Count()) - personBarMargin;
			float pixelsPerYear = (float)margins.Width / (float)totalYears;

			//
			// Create the theme.
			//
			Theme theme = new();

			using (Bitmap image = new((int)page.Width, (int)page.Height))
			{
				//
				// Set the image resolution.
				//
				image.SetResolution(resolution, resolution);

				//
				// Create a graphics object from the image.
				//
				using (Graphics g = Graphics.FromImage(image))
				{
					//
					// Fill the page background with white.
					//
					g.FillRectangle(theme.PageBackgroundBrush, page);

					//
					// Draw the year marker lines.
					//
					for (int i = yearDivisions; i < totalYears; i += yearDivisions)
					{
						//
						// Get the size of the text.
						//
						SizeF size = g.MeasureString(i.ToString(), theme.YearsFont);

						//
						// Calculate the left position.
						//
						float left = margin + (int)((float)i / (float)totalYears * margins.Width);

						//
						// Draw the vertical line.
						//
						g.DrawLine(theme.VerticalLinePen, new PointF(left, (int)(margin + size.Height + 1)), new PointF(left, margins.Bottom));

						//
						// Draw the year text.
						//
						int textLeft = (int)(left - (size.Width / 2F));
						g.DrawString(i.ToString(), theme.YearsFont, theme.YearsBrush, new PointF(textLeft, margin));
					}

					//
					// Calculate the position of each person.
					//
					IEnumerable<EventDecorator> decoratedPeople = (from tbl in events
																   select new EventDecorator(tbl)
																   {
																   }).ToArray();

					//
					// Assign predecessors.
					//
					foreach (EventDecorator item in decoratedPeople)
					{
						item.Predecessor = decoratedPeople.GetPredecessor(item);
					}

					//
					// Calculate the position of each object.
					//
					foreach (EventDecorator item in decoratedPeople.OrderBy(t => t.EventItem.Sequence))
					{
						//
						// Calculate the width the box.
						//
						int eventLength = (int)(pixelsPerYear * item.EventItem.EventLength);

						//
						// Autio size the box if the event length is unknown (-1).
						//
						if (item.EventItem.EventLength == -1)
						{
							SizeF size1 = g.MeasureString(item.EventItem.Name, theme.NameFont);
							SizeF size2 = g.MeasureString("(?)", theme.LengthFont);

							eventLength = (int)(size1.Width + size2.Width);
						}

						if (item.Predecessor == null)
						{
							//
							// A person without a predecessor is at the left margin.
							//
							item.Rectangle = new(margins.Left + 1, startingTop, eventLength, personBarHeight);
						}
						else
						{
							//
							// The left position is dependent on predecessors.
							//
							float left = item.GetLeftPosition(pixelsPerYear);
							item.Rectangle = new(left, startingTop, eventLength, personBarHeight);
						}

						startingTop += personBarMargin + personBarHeight;
					}

					//
					// Draw the objects.
					//
					DrawObjects(g, decoratedPeople.Where(t => t.EventItem.EntryType == EntryType.TimeMarker), margins, theme);
					DrawObjects(g, decoratedPeople.Where(t => t.EventItem.EntryType == EntryType.Person), margins, theme);

					//
					// Print the page title in the bottom left.
					//
					string title = "The Genesis Years";
					SizeF titleSize = g.MeasureString(title, theme.TitleFont);
					float titleLeft = margin + 50;
					float titleTop = margins.Bottom - titleSize.Height - 50;
					g.DrawString(title, theme.TitleFont, theme.TitleBrush, new PointF(titleLeft, titleTop));

					//
					// Draw a border at the margins.
					//
					g.DrawRectangle(theme.BorderPen, margins);

					//
					// Save the image.
					//
					string imageFile = "Z:/Desktop/Genesis-Years.png";
					image.Save(imageFile, ImageFormat.Png);
					Process.Start(new ProcessStartInfo(imageFile) { UseShellExecute = true });
				}
			}
		}

		static void DrawObjects(Graphics g, IEnumerable<EventDecorator> items, RectangleF margins, Theme theme)
		{
			//
			// Draw the event bars.
			//
			foreach (EventDecorator item in items.OrderBy(t => t.EventItem.Sequence))
			{
				if (item.EventItem.EntryType == EntryType.TimeMarker)
				{
					//
					// Draw a vertical line at the time marker.
					//
					g.DrawLine(theme.MarkerLinePen, item.Rectangle.Left, item.Rectangle.Top, item.Rectangle.Left, item.Rectangle.Bottom);
				}
				else if (item.EventItem.EntryType == EntryType.Person)
				{
					//
					// Draw the rectangle.
					//
					g.FillRectangle(item.EventItem.Style == Style.Dark ? theme.BarDarkBackgroundBrush : theme.BarLightBackgroundBrush, item.Rectangle);
					g.DrawRectangle(item.EventItem.Style == Style.Dark ? theme.BarDarkBorderPen : theme.BarLightBorderPen, item.Rectangle);
				}

				//
				// Draw the text
				//
				float textLeft = 0;
				bool shifted = false;

				if (item.EventItem.EntryType == EntryType.TimeMarker)
				{
					SizeF size = g.MeasureString(item.EventItem.Name, theme.MarkerFont);

					if (item.EventItem.TextAlign == TextAlign.Right)
					{
						textLeft = item.Rectangle.Left + 1;
					}
					else
					{
						textLeft = item.Rectangle.Left - 1 - size.Width;
					}

					float top = item.Rectangle.Top + 1;
					g.DrawString(item.EventItem.Name, theme.MarkerFont, theme.MarkerTextBrush, new PointF(textLeft, top));
				}
				else
				{
					//
					// Display at least the name.
					//
					string text = item.EventItem.Name;

					//
					// Draw the name.
					//
					SizeF size1 = g.MeasureString(text, theme.NameFont);

					//
					// If the text is wider than the box, draw it to the right.
					//
					if (size1.Width > item.Rectangle.Width)
					{
						textLeft = item.Rectangle.Right + 1;
						shifted = true;
					}
					else
					{
						textLeft = item.Rectangle.Left + 1;
					}

					float top1 = item.Rectangle.Top + 1;
					Brush brush1 = item.EventItem.Style == Style.Dark ? theme.BarDarkTextBrush : theme.BarLightTextBrush;
					g.DrawString(text, theme.NameFont, brush1, new PointF(textLeft, top1));

					//
					// Add the event time span if requested.
					//
					if (item.EventItem.DisplayEventLength)
					{
						string lengthText = $" ({item.EventItem.EventLength})";

						if (item.EventItem.EventLength == -1)
						{
							lengthText = "(?)";
						}

						SizeF size2 = g.MeasureString(lengthText, theme.LengthFont);
						g.DrawString(lengthText, theme.LengthFont, brush1, new PointF(textLeft + size1.Width - 10, top1 + ((size1.Height - size2.Height) / 2)));
					}
				}

				//
				// Draw the scripture reference.
				//
				SizeF refTextSize = g.MeasureString(item.EventItem.Reference, theme.ReferenceFont);
				float refTextLeft = 0;

				if (item.EventItem.EntryType == EntryType.TimeMarker)
				{
					if (item.EventItem.TextAlign == TextAlign.Right)
					{
						refTextLeft = item.Rectangle.Left + 3;
					}
					else
					{
						refTextLeft = textLeft;
					}
				}
				else
				{
					if (shifted)
					{
						refTextLeft = item.Rectangle.Right + 1;
					}
					else
					{
						refTextLeft = item.Rectangle.Left + 1;
					}
				}

				float refTextTop = item.Rectangle.Bottom - refTextSize.Height + 2;
				g.DrawString(item.EventItem.Reference, theme.ReferenceFont, theme.ReferenceBrush, new PointF(refTextLeft, refTextTop));
			}
		}
	}
}