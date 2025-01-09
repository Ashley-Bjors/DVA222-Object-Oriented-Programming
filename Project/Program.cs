using System;
using GLib;
using Gtk;
using Cairo;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Atk;
using Gdk;


/*Tankar inför kommande, behöver ordnas så det blir ett client - program, just nu görs det mesta i main filen 
och sedan skickas till GridWindow. Man ska i stort sett bara ha main program som initierar appen och startar client program
och ansvarar för RUN. 
Men tills task 3 är gjord är det lättast att dela upp det såhär först och sen röra om*/

// Abstract class to be able to make different type of windows in GTK with grid
abstract class GridWindow 
{
	
	public string[,] CircleColor;  //Povider to be able to change color, keeping the colors of each circle in each grid
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
		CircleColor = new string[this.Rows, this.Column];		
	}
	
	//Adds empety labels to the box for the window
	//För task 2-3 kan vi behöva byta ut label mot en figur eller image istället
	public void GridConstructor(Grid box)
	{
		

		
		for(int i = 0; i < this.Rows; i++) //Fyller boxen/gridet som en kolumnarray
		{
			for(int j = 0; j < this.Column; j++)
			{
				
				var square = new DrawingArea{WidthRequest = this.Size, HeightRequest = this.Size};
				square.Drawn += (sender, args) => DrawGrid(args.Cr, square, this);				    //Connects Drawingarea and Draws when called
				
				square.Data["Row"] = i;
				square.Data["Col"] = j;
				
				box.Attach(square,j,i, 1, 1);

			}
		}
		

	}
	

	
	//Function to change colors as wanted and the drawings
	private void DrawGrid(Context cc, DrawingArea circle, GridWindow th)
	{
		double red;
		double green;
		double blue;
		int r = (int)circle.Data["Row"];
		int c = (int)circle.Data["Col"];
		GetColor(th.BackColor, out red, out green, out blue);

		cc.SetSourceRGB(red, green, blue);				
		cc.Rectangle(0, 0, th.Size, th.Size);
		cc.Fill();
		
		GetColor(th.CircleColor[r,c], out red, out green, out blue);
		cc.SetSourceRGB(red, green, blue);	
		cc.Arc(th.Size/2, th.Size/2, th.Size/3,0, 2 * Math.PI);
		cc.Fill();
					
		
		GetColor(th.LineColor, out red, out green, out blue);
		cc.SetSourceRGB(red,green,blue);
		cc.Rectangle(0, 0, th.Size, th.Size);
		cc.LineWidth = 5;
		
		
		//cc.Arc((i*th.Size)-(th.Size/2), (j*th.Size)-(th.Size/2), th.Size/7,0, 2 * Math.PI);
		
		cc.StrokePreserve();

	}

	//Translates string to cairo colours
 	private void GetColor(string color, out double red, out double green, out double blue)
	{
		red = 0;
		green = 0;
		blue = 0;
		switch(color)
		{
			case "red":
				red = 1;
				green = 0;
				blue = 0;
				break; 

			case "rlue":
				red = 0;
				green = 0;
				blue = 1;
				break; 

			case "grey":
				red = 0.5;
				green = 0.5;
				blue = 0.5;
				break; 

		}
		

	}
}

//Easier to use these two for the client program instead of user input or changing numbers all time
class Task1and2 : GridWindow
{
	public Task1and2() : base(2,3,200, "grey", "black"){}

}
class Task3 : GridWindow
{
	public Task3() : base(9,9,100, "grey", "blue"){}

}


class Program
{

	static void Main()
	{
		Gtk.Application.Init();
				
		Gtk.Window window = new Gtk.Window("DVA-222 help me god"); // Create the main window
		//window.SetDefaultSize(640, 480);
	 // Make the window non-resizable
		
		List <GridWindow> grid= new List<GridWindow>{new Task3()}; //gets the grids for task 1,2,3 
		Grid box = new Grid{ColumnSpacing = 1, RowSpacing = 1, Margin = 5}; //grid specifiks

		//For loop to fill color to start with for circles
		for(int i = 0; i < grid[0].Rows; i++)
		{
			for(int j = 0; j < grid[0].Column; j++)
			{
				grid[0].CircleColor[i,j] = 	grid[0].BackColor;
			}
			
		}
		grid[0].GridConstructor(box); //adds all ta labels to the grid
		
		window.Add(box); //adding the box with the grid in it
		
		
		window.DeleteEvent += (sender, args) => Gtk.Application.Quit();
		window.KeyPressEvent += (sender, args) => Console.WriteLine($"KeyPressEvent: {args.Event.Key.ToString()}");
		window.ButtonPressEvent += (sender, args) => Console.WriteLine($"ButtonPressEvent: {args.Event.Button.ToString()} at ({(int)args.Event.X},{(int)args.Event.Y})");
		window.ButtonReleaseEvent += (sender, args) => Console.WriteLine($"OnButtonReleaseEvent: {args.Event.Button.ToString()} at ({(int)args.Event.X},{(int)args.Event.Y})");
		//window.Drawn += (sender, args) => OnDrawn();
		// Show all widgets
		
		/*
		//Runs an animation of task 1.2
		GLib.Timeout.Add(1000,() =>{
			Console.WriteLine($"DEBUG: Circle Added");
			//grid[0].DrawFunction(box);
			Random rand = new Random();
			int r = rand.Next(grid[0].Rows);
			int c = rand.Next(grid[0].Column);
			grid[0].CircleColor[r,c] = "red";
			//grid[0].BackColor = "red";
			box.Children[r*grid[0].Column + c].QueueDraw();
			//box.ShowAll();
			
			//grid[0].DrawFunction.QueueDraw();
			int i = 0;
			foreach(string b in grid[0].CircleColor)
				{
					if(b == "red")
					{
						i++;
					}
					

				}
			if(i == grid[0].Column + grid[0].Size)
				return false;
			else
			{
				return true;

			}
				
			});

			*/
		
		
		
		Gdk.Key lastInput = 0; //Initiate a variable to save the last player input. The use of 0 is arbitrary
		window.KeyPressEvent += (sender, args) => lastInput = args.Event.Key; //Save Said input^^
			//Runs the game of Task 1.3
		GLib.Timeout.Add(100,() =>{
			Console.WriteLine($"DEBUG: Game Engine Tick(1s)");
			if(lastInput != 0) //See if the input has changed from the default
			{
				Console.WriteLine($"DEBUG: KeyPressEvent: {lastInput.ToString()}");
				if("123456789".Contains(lastInput.ToString()[lastInput.ToString().Length - 1]))//See if the last input is a value between 1 & 9. This can be done so much better by using the method used in getting int column a few rows below 
				{
					Console.WriteLine($"DEBUG: KeyPressEvent within Bounds");
					int column = lastInput.ToString()[lastInput.ToString().Length - 1]- '0' - 1; //Turn the latest input from a string into a int and make it line up with the actual value by - '0'
					for (int i = grid[0].Rows - 1; i > 0; i--) //Iterate from the bottom to the top row
					{
						if(grid[0].CircleColor[i,column]=="grey")
						{
							grid[0].CircleColor[i,column] = "red";
							//This only updates when minimised ;_; PLS Fix
							break;
						}
					}
					
					//Add a if player input successful then do Random ENEMY move. Reuse code above?

				}
				 
				lastInput = 0;
			}
			return true;				
			});
		window.ShowAll();
		// Run the GTK application
		Gtk.Application.Run();
		
		
		
	}
	
}