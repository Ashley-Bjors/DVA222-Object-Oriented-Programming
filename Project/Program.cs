using System;
using GLib;
using Gtk;
using Cairo;


 /*----------- Project work by Ashley Björs and Ebba Norlin for the DVA-222 course ----------------------
 -------------2025-01-10 Västerås-----------------------------------------------------*/

// Abstract class to be able to make different type of windows in GTK with grid
abstract class GridWindow 
{
	
	public List<DrawingArea> DrawHelper;  //Povider to be able to change color, keeping the colors of each circle in each grid
	public List<string> CircleColor;     //List to keep track on each color and wich drawingarea has wich
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
	
	//Adds DrawingAreas to the grid that is one box for the window
	public void GridConstructor(Grid box)
	{		
		for(int i = 0; i < this.Rows; i++) //Fills the grid
		{
			for(int j = 0; j < this.Column; j++)
			{
				int index = DrawHelper.Count;
				var square = new DrawingArea{WidthRequest = this.Size, HeightRequest = this.Size};
				square.Drawn += (sender, args) => DrawGrid(args.Cr, index, this);				    //Connects Drawingarea and Draws when called
										
				box.Attach(square,j,i, 1, 1); //Attacg to grid
				DrawHelper.Add(square);        //Adds the drawingArea to our list to keep track on them
 
			}
		}
		

	}
	

	//Function to change colors as wanted and the drawings inside them
	private void DrawGrid(Context cc, int i, GridWindow th)
	{
		double red;
		double green;
		double blue;
		
		GetColor(th.BackColor, out red, out green, out blue);  //function to get color to cairo context

		cc.SetSourceRGB(red, green, blue);			//Grid acting as the square	
		cc.Rectangle(0, 0, th.Size, th.Size);
		cc.Fill();
		
		GetColor(th.CircleColor[i], out red, out green, out blue);
		cc.SetSourceRGB(red, green, blue);	
		cc.Arc(th.Size/2, th.Size/2, th.Size/3,0, 2 * Math.PI);    //Circle, initilized in same backgroundcolor to be invisable to begin with
		cc.Fill();
					
		
		GetColor(th.LineColor, out red, out green, out blue);  //Getting outline for the grid and linecolor
		cc.SetSourceRGB(red,green,blue);
		cc.Rectangle(0, 0, th.Size, th.Size);
		cc.LineWidth = 5;
		
		
		
		cc.StrokePreserve();

	}

	//Translates string to cairo colours
 	private void GetColor(string color, out double red, out double green, out double blue)
	{
		red = 0; // Initilizing to start with black
		green = 0;
		blue = 0;
		switch(color)
		{
			case "red":
				red = 1;
				green = 0;
				blue = 0;
				break; 

			case "blue":
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
	public Task3() : base(9,9,100, "grey", "black"){}

}




class Program
{

	static void Main(string[] args)
	{
		Gtk.Application.Init();  //initlising
				
		Gtk.Window window = new Gtk.Window("DVA-222 Task 1-2"); // Create the main window for task 1-2
		Program p = new Program();
		p.ClientProgram(window, 0); //runs first clientprogram
		window.ShowAll();
		
		Gtk.Window window2 = new Gtk.Window("DVA-222 Task 3");  //window for task 3
		GLib.Timeout.Add(22000,() =>{        // makes sure the first client program runs before starting the new one
			window.Hide();                   //hides the first window
			
			p.ClientProgram(window2,1);        //client program for task 3
			window2.ShowAll();
			return false;
		});
		
		// Run the GTK application
		Gtk.Application.Run();		
		
		
	}
	public void ClientProgram(Gtk.Window window, int p)
	{
		
		VBox vbox = new VBox(); //Box for having a grid and a label in the same window
		Label label = new Label(""); //start with empety window
		List <GridWindow> grid= new List<GridWindow>{new Task1and2(), new Task3()}; //gets the grids for task 1,2,3 
		Grid box = new Grid{ColumnSpacing = 1, RowSpacing = 1, Margin = 5}; //grid specifiks
		grid[p].DrawHelper = new List<DrawingArea>(); //Creates list for keeping track on drawings
		Random rand = new Random();
		List<int> numSquare = Enumerable.Range(0, grid[p].Column*grid[p].Rows).OrderBy(x => rand.Next()).ToList(); //makes a list of random grid order
		
		//For loop to fill color to start with for circles
		grid[p].CircleColor = new List<string>();
		for(int i = 0; i < grid[p].Rows*grid[p].Column; i++)
		{
			grid[p].CircleColor.Add(grid[p].BackColor);			
			
		}
		grid[p].GridConstructor(box); //adds all ta drawingAreas to the grid
		
		vbox.PackStart(label, true, true, 0); //adds label to vertical box
		vbox.PackStart(box, true, true, 10);  //adds gridbox to vertical box
		window.Add(vbox); //adding the vertical box with the grid in it and label to the window
		
		//Eventhandeling
		window.DeleteEvent += (sender, args) => Gtk.Application.Quit();
		window.KeyPressEvent += (sender, args) =>{
			if(args.Event.Key ==  Gdk.Key.Escape)
			{
				Gtk.Application.Quit();
			}

		} ;
		window.ButtonPressEvent += (sender, args) => Console.WriteLine($"ButtonPressEvent: {args.Event.Button.ToString()} at ({(int)args.Event.X},{(int)args.Event.Y})");
		window.ButtonReleaseEvent += (sender, args) => Console.WriteLine($"OnButtonReleaseEvent: {args.Event.Button.ToString()} at ({(int)args.Event.X},{(int)args.Event.Y})");
		
		//For task 1 and 2
		if(p == 0)
		{
			label.Text = "This animation will run for 3 rounds proving task 1-2";
			int r = 0;
			//Runs an animation of task 1.2
			GLib.Timeout.Add(1000,() =>{

				if(numSquare.Count == 0) //If the grid is filled 
				{
					for(int i = 0; i < grid[0].Rows*grid[p].Column; i++) //foor loop to go back to original grey grid
					{
						grid[p].CircleColor[i] = grid[p].BackColor;		
						grid[p].DrawHelper[i].QueueDraw();
					}
					
					numSquare = Enumerable.Range(0, grid[p].Column*grid[p].Rows).OrderBy(x => rand.Next()).ToList(); //new list of random grid order
					r++; //amount of runns for viwer
					GLib.Timeout.Add(1000,() =>{return false;});
					return true;
				}			
				if(r == 3) //3 runs of full grid has been run
				{
					return false;
				}
				grid[p].CircleColor[numSquare[0]] = "red"; //Give random grid blocks circle the color red
				
				grid[p].DrawHelper[numSquare[0]].QueueDraw(); //Makes it draw
				
				while(GLib.MainContext.Pending()) //Waits for the grid to be redrawn before continuing
				{
					GLib.MainContext.Iteration(false);				
				}			
				
				numSquare.RemoveAt(0);	//Removes number from grid-order so there is no duplicates
				return true;
								
				
					
				});
				
				return;
		}
		//running task 3
		if(p == 1)
		{
			
			bool computer = false; //Variable for who's turn it is
				
			Random random = new Random();
			int start = random.Next(2); //random of 0 or 1
			if(start == 0)
			{
				label.Text = "Player starts";
			}
			else
			{
				label.Text = "Computer starts"; //computer starts and fills a circle randomly on the board
				
				computer = true;
				/*
					grid[p].CircleColor[numSquare[0]] = "red";
					
					grid[p].DrawHelper[numSquare[0]].QueueDraw();
					
					while(GLib.MainContext.Pending())
					{
						GLib.MainContext.Iteration(false);				
					}
					numSquare.RemoveAt(0);
				*/
			}
			Gdk.Key lastInput = 0; //initial value for key
			
			window.KeyPressEvent += (sender, args) => {
				lastInput = args.Event.Key;//directs the key 
			}; 
			GLib.Timeout.Add(100, () =>{
				if(numSquare.Count == 0)
				{
					label.Text = "Grid full - No winner";
					return false;
				}
				if(computer == false)
				{
					if(lastInput != 0)
					{
						Console.WriteLine($"DEBUG: KeyPressEvent: {lastInput.ToString()}");

							label.Text = "Player time to move";
						
							if("123456789".Contains(lastInput.ToString()[lastInput.ToString().Length - 1]))//See if the last input is a value between 1 & 9.  
							{
								Console.WriteLine($"DEBUG: KeyPressEvent within Bounds");
								int column = lastInput.ToString()[lastInput.ToString().Length - 1]- '0' - 1; //Turn the latest input from a string into a int and make it line up with the actual value by - '0'
								int i = grid[p].Rows - 1;
								for (; i >= 0; i--) //Iterate from the bottom to the top row
								{
									if(grid[p].CircleColor[i*grid[p].Rows+column]=="grey")
									{
										grid[p].CircleColor[i*grid[p].Rows+column] = "blue";

										grid[p].DrawHelper[i*grid[p].Rows+column].QueueDraw();
										while(GLib.MainContext.Pending())
										{
											GLib.MainContext.Iteration(false);				
										}
										numSquare.Remove(i*grid[p].Rows+column);
										break;
									}
								}
								if(i < 0)
								{
									label.Text = "Column filled try again";	
								}
								else
								{
									computer = true;
								}
								lastInput = 0;				
								return Winner(grid[p],label,column,i);
							}
						// A LOT of if statemnts to check if a player has won
						
						/*
						if(Winner(grid[p], label))
						{ 		
							return false; //Game is over
						}
						*/
					}
				}
				else
				{	
					label.Text = "Computer time to move";
					
					while(computer == true)
					{
						int column = random.Next(8);
						int i = grid[p].Rows - 1;
						for (; i >= 0; i--) //Iterate from the bottom to the top row
						{
							if(grid[p].CircleColor[i*grid[p].Rows+column]=="grey")
							{
								grid[p].CircleColor[i*grid[p].Rows+column] = "red";

								grid[p].DrawHelper[i*grid[p].Rows+column].QueueDraw();
								while(GLib.MainContext.Pending())
								{
									GLib.MainContext.Iteration(false);				
								}
								numSquare.Remove(i*grid[p].Rows+column);
								break;
							}
						}
						if(i < 0)
						{
							label.Text = "Column filled try again";	
						}
						else
						{
							computer = false;
						}		
					return Winner(grid[p],label,column,i);
					}
					/*
					if(Winner(grid[p], label))
					{ 		
						return false; //Game is over
					}
					*/
				}
				Console.WriteLine("Computer runnnnn");
				return true;
			});
		}
	}

//A return value of False means the game is over
	private bool Winner(GridWindow grid, Label label, int column, int row)
	{
		string lastaddedcolour = grid.CircleColor[row*grid.Rows+column];
		if (row > 2 && column < grid.Column-3)
		{
			//check north east
			for (int i = 1; i <= 3; i++)
			{
				if(grid.CircleColor[(row-i)*grid.Rows+column+i] != lastaddedcolour)
				{
					break;
				}
				if(i == 3)
				{
					label.Text = $"Winner is {lastaddedcolour}";
					return false;
				}
			}
		}
		if (column < grid.Column-3)
		{
			//check east
			for (int i = 1; i <= 3; i++)
			{
				if(grid.CircleColor[(row)*grid.Rows+column+i] != lastaddedcolour)
				{
					break;
				}
				if(i == 3)
				{
					label.Text = $"Winner is {lastaddedcolour}";
					return false;
				}
			}
		}
		if (row < grid.Rows-3 && column < grid.Column-3)
		{
			//check south east
			for (int i = 1; i <= 3; i++)
			{
				if(grid.CircleColor[(row+i)*grid.Rows+column+i] != lastaddedcolour)
				{
					break;
				}
				if(i == 3)
				{
					label.Text = $"Winner is {lastaddedcolour}";
					return false;
				}
			}
		}
		if(row < grid.Rows-3)
		{
			//check south
			for (int i = 1; i <= 3; i++)
			{
				if(grid.CircleColor[(row+i)*grid.Rows+column] != lastaddedcolour)
				{
					break;
				}
				if(i == 3)
				{
					label.Text = $"Winner is {lastaddedcolour}";
					return false;
				}
			}
		}
		if(row < grid.Rows-3 && column > 2)
		{
			//check south west
			for (int i = 1; i <= 3; i++)
			{
				if(grid.CircleColor[(row+i)*grid.Rows+column-i] != lastaddedcolour)
				{
					break;
				}
				if(i == 3)
				{
					label.Text = $"Winner is {lastaddedcolour}";
					return false;
				}
			}
		}
		if(column > 2) 
		{
			//check west
			for (int i = 1; i <= 3; i++)
			{
				if(grid.CircleColor[(row)*grid.Rows+column-i] != lastaddedcolour)
				{
					break;
				}
				if(i == 3)
				{
					label.Text = $"Winner is {lastaddedcolour}";
					return false;
				}
			}
		}
		if(row > 2 && column > 2)
		{
			//check north west
			for (int i = 1; i <= 3; i++)
			{
				if(grid.CircleColor[(row-1)*grid.Rows+column-1] != lastaddedcolour)
				{
					break;
				}
				if(i == 3)
				{
					label.Text = $"Winner is {lastaddedcolour}";
					return false;
				}
			}
		}
		return true;
	}

/*
	private bool Winner(GridWindow grid, Label label)
	{
		if(grid.CircleColor[0] == grid.CircleColor[1] && grid.CircleColor[0] == grid.CircleColor[2] && grid.CircleColor[0] != "grey") // first vertical
					{
						label.Text = $"Winner is {grid.CircleColor[0].ToString()}";
						return true;
					}
					if(grid.CircleColor[3] == grid.CircleColor[4] && grid.CircleColor[3] == grid.CircleColor[5] && grid.CircleColor[3] != "grey") // second vertical
					{
						label.Text = $"Winner is {grid.CircleColor[3]}";
						return true;
					}
					if(grid.CircleColor[6] == grid.CircleColor[7] && grid.CircleColor[6] == grid.CircleColor[8] && grid.CircleColor[6] != "grey") // third vertical
					{
						label.Text = $"Winner is {grid.CircleColor[6]}";
						return true;

					}

					if(grid.CircleColor[0] == grid.CircleColor[3] && grid.CircleColor[0] == grid.CircleColor[6] && grid.CircleColor[0] != "grey") // first horizontal
					{
						label.Text = $"Winner is {grid.CircleColor[0]}";
						return true;

					}
					if(grid.CircleColor[1] == grid.CircleColor[4] && grid.CircleColor[1] == grid.CircleColor[7] && grid.CircleColor[1] != "grey") // second horizontal
					{
						label.Text = $"Winner is {grid.CircleColor[1]}";
						return true;

					}
					if(grid.CircleColor[2] == grid.CircleColor[5] && grid.CircleColor[2] == grid.CircleColor[8] && grid.CircleColor[2] != "grey") // third horizontal
					{
						label.Text = $"Winner is {grid.CircleColor[2]}";
						return true;
					}
					if(grid.CircleColor[0] == grid.CircleColor[4] && grid.CircleColor[0] == grid.CircleColor[8] && grid.CircleColor[0] != "grey") // first cross
					{
						label.Text = $"Winner is {grid.CircleColor[0]}";
						return true;

					}
					if(grid.CircleColor[2] == grid.CircleColor[4] && grid.CircleColor[2] == grid.CircleColor[6] && grid.CircleColor[2] != "grey") // second cross
					{
						label.Text = $"Winner is {grid.CircleColor[2]}";
						return true;

					}
					return false;
	

	}
*/	
	
}