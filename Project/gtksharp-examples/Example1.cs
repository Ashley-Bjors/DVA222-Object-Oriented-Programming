using System;
using GLib;
using Gtk;
using Cairo;

/*

Now we add a Button widget to the window. However, without a container that manage the space,
the button occupies the entire space.

*/

class Example1
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

		// Create a Button widget and add it directly to the Window
		Button button = new Button("Click Me");
      button.Clicked += (sender, args) =>	Console.WriteLine("Click!");
		button.SetSizeRequest(100, 50);
      window.Add(button);
		
		// Show all widgets
		window.ShowAll();

		// Run the GTK application
		Gtk.Application.Run();
	}
	
}