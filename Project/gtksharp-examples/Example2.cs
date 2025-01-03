using System;
using GLib;
using Gtk;
using Cairo;

/*

Now we can add also other widgets. Note that methods to be attached to events can be defined
inline (if short) or in a more canonical way (like OnDraw for the DrawingArea). 

 What if we want to animate the gree circle?

*/

class Example2
{
	static void Main()
	{
		Gtk.Application.Init();

		// Create the main window
		Gtk.Window window = new Gtk.Window("DVA-222");
		window.SetDefaultSize(640, 480);
		window.Resizable = false; // Make the window non-resizable
		
		window.DeleteEvent += (sender, args) => Gtk.Application.Quit();
		window.KeyPressEvent += (sender, args) =>	Console.WriteLine($"KeyPressEvent: {args.Event.Key.ToString()}");
		window.ButtonPressEvent += (sender, args) =>	Console.WriteLine($"ButtonPressEvent: {args.Event.Button.ToString()} at ({(int)args.Event.X},{(int)args.Event.Y})");
		window.ButtonReleaseEvent += (sender, args) => Console.WriteLine($"OnButtonReleaseEvent: {args.Event.Button.ToString()} at ({(int)args.Event.X},{(int)args.Event.Y})");

		// Create a Fixed container to manually position widgets
      Fixed container = new Fixed();
      window.Add(container);

		// Create a Button widget and put it in the Fixed container
		Button button = new Button("Click Me");
      button.Clicked += (sender, args) =>	Console.WriteLine("Click!");
		button.SetSizeRequest(100, 50);
		container.Put(button, 20, 20);
		
		// Create an Image and put it in the Fixed container
		Image image = new Image("cover5.jpg");
		container.Put(image, 270, 20);
		
		// Create a Label and put it in the Fixed container
		Label label = new Label("DVA-222");
		container.Put(label, 50, 400);
		
		// Create a DrawingArea and put it in the Fixed container
		DrawingArea drawingarea = new DrawingArea();
      drawingarea.SetSizeRequest(100, 260);
		drawingarea.Drawn += OnDraw;
		container.Put(drawingarea, 20, 80);
		
		// Show all widgets
		window.ShowAll();

		// Run the GTK application
		Gtk.Application.Run();
	}
		
	static void OnDraw(object sender, Gtk.DrawnArgs args)
	{
		DrawingArea drawingarea = (DrawingArea)sender;
		// Draw a black rectangle
		args.Cr.SetSourceRGB(0, 0, 0); // Set color to black
		args.Cr.Rectangle(0, 0, 800, 800); // Set position and size (clip if bigger than drawingarea)
		args.Cr.Fill(); // Draw the filled rectangle
		// Draw a circle in the DrawingArea at random position
		int width, height, radius = 10;
      drawingarea.GetSizeRequest(out width, out height);
		Random rnd = new Random();
		int x = rnd.Next(radius, width-radius);
		int y = rnd.Next(radius, height-radius);
		args.Cr.Arc(x, y, radius, 0, 2 * Math.PI);
		args.Cr.SetSourceRGB(0, 1, 0);
		args.Cr.Stroke(); // Draw the not-filled circle

	}

}