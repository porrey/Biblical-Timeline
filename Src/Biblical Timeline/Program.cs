using System.Diagnostics;
using System.Drawing.Imaging;
using System.Reflection;
using Biblical.Timeline.Themes;
using Newtonsoft.Json;

namespace Biblical.Timeline
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			//
			// Load the people
			//
			string file = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}/Data/biblical-events.json";
			string json = File.ReadAllText(file);
			IEnumerable<BiblicalEvent> biblicalEvents = JsonConvert.DeserializeObject<IEnumerable<BiblicalEvent>>(json);

			//
			// Set the title of the sheet and the output image file name.
			//
			string title = "Biblical Timeline";
			string imageFile = $"{Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}/{title}.png";

			//
			// Define page parameters.
			//
			using (PageDefinition pageDefinition = new(title, 44F, 34F, 300F, .45F * 300F, new DefaultTheme(), new()))
			{
				//
				// Define timeline parameters. Each line division is 100 years. The total
				// width of the image is 4,600 years.
				//
				TimelineParameters timelineParameters = new(pageDefinition.DrawableArea, biblicalEvents.MaximumVerticalCount(), 100, 4400, 15);

				//
				// Wrap the Biblical timeline events in Image Objects. Must use ToArray()
				// here in order to preserve the objects when compute layout is run.
				//
				IEnumerable<ImageObjectTemplate> imageObjects = (from tbl in biblicalEvents
																 select tbl.ToImageObjectTemplate(pageDefinition, timelineParameters))
																 .ToArray();

				//
				// Assign predecessors.
				//
				foreach (ImageObjectTemplate item in imageObjects)
				{
					item.Predecessor = imageObjects.GetPredecessor(item);

					if (item.BiblicalEvent.Sequence > 1 && item.Predecessor == null)
					{
					}
				}

				//
				// Build the image.
				//
				await pageDefinition.BuildAsync(timelineParameters, imageObjects);

				//
				// Calculate the BC/AD markings.
				//
				await (new TimelineAnalyzer()).CalculateBc(biblicalEvents, pageDefinition.GridLines);

				//
				// Draw the image.
				//
				await pageDefinition.DrawAsync(timelineParameters, imageObjects);

				//
				// Save the image.
				//
				await pageDefinition.SaveImageAsync(imageFile, ImageFormat.Png);
				Process.Start(new ProcessStartInfo(imageFile) { UseShellExecute = true });
			}
		}
	}
}