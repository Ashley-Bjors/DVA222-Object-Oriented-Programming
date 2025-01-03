using System;
using GLib;
using Gtk;
using Cairo;

/*

Let's draw a window and define its behavior by attaching some events, i.e., 
connecting events to methods that define the desired behavior.

*/


class Example0
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
		
		// Show all widgets
		window.ShowAll();

		// Run the GTK application
		Gtk.Application.Run();
	}
	
}