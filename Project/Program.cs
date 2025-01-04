using System;
using GLib;
using Gtk;
using Cairo;
using System.Runtime.CompilerServices;

// Abstract class to be able to make different type of windows in GTK with grid

abstract class GridWindow
{
	private static CssProvider css;  //Povider to be able to change color
	internal int Rows{get; set;}			//Number of rows
	internal int Column{get; set;}		//number of columns
	internal int Size{get; set;}		//Size of cell
	internal string BackColor{get; set;}		//Background Color
	internal string LineColor{get; set;}		//Color of line that limits cell
	public GridWindow(int rows, int column, int size, string backColor, string lineColor)
	{
		Rows = rows;
		Column = column;
		Size = size;
		BackColor = backColor;
		LineColor = lineColor;
	}
	
	//Adds empety labels to the box for the window
	//För task 2-3 kan vi behöva byta ut label mot en figur eller image istället
	public void GridConstructor(Grid box)
	{
		css = new CssProvider();
		for(int i = 0; i < this.Column; i++) //Fyller boxen/gridet som en kolumnarray
		{
			for(int j = 0; j < this.Rows; j++)
			{
				var label = new Label("");
				label.WidthRequest = this.Size;
				label.HeightRequest = this.Size;
				GridColors(label, this.BackColor, this.LineColor); //Sets the colors
				box.Attach(label,i,j, 1, 1);

			}
		}
		

	}

	//Function to change colors as wanted
	private static void GridColors(Widget widget, string bcolor, string lcolor)
	{
		css.LoadFromData($@"
            * {{
                border: 2px solid {lcolor};
                padding: 5px;
                background-color: {bcolor};
            }}
        ");

        // Koppla CSS-providern till widgetens StyleContext
        widget.StyleContext.AddProvider(css, 800);

	}
}

//Easier to use these two for the client program instead of user input or changing numbers all time
class Task1and2 : GridWindow
{
	public Task1and2() : base(2,3,200, "grey", "Red"){}

}
class Task3 : GridWindow
{
	public Task3() : base(3,3,100, "grey", "blue"){}

}

class Program
{

	static void Main()
	{
		Gtk.Application.Init();
				
		Gtk.Window window = new Gtk.Window("DVA-222 help me god"); // Create the main window
		//window.SetDefaultSize(640, 480);
	 // Make the window non-resizable
		
		List <GridWindow> grid= new List<GridWindow>{new Task1and2()}; //gets the grids for task 1,2,3 
		Grid box = new Grid{ColumnSpacing = 1, RowSpacing = 1, Margin = 5}; //grid specifiks
		grid[0].GridConstructor(box); //adds all ta labels to the grid
				
		window.Add(box); //adding the box with the grid in it
		window.DeleteEvent += (sender, args) => Gtk.Application.Quit();
		window.KeyPressEvent += (sender, args) => Console.WriteLine($"KeyPressEvent: {args.Event.Key.ToString()}");
		window.ButtonPressEvent += (sender, args) => Console.WriteLine($"ButtonPressEvent: {args.Event.Button.ToString()} at ({(int)args.Event.X},{(int)args.Event.Y})");
		window.ButtonReleaseEvent += (sender, args) => Console.WriteLine($"OnButtonReleaseEvent: {args.Event.Button.ToString()} at ({(int)args.Event.X},{(int)args.Event.Y})");

		// Show all widgets
		window.ShowAll();

		// Run the GTK application
		Gtk.Application.Run();

		
	}
}