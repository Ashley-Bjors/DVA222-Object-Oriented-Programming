namespace Lab2
{
    using System;

    namespace Task1
    {

        public interface  RaceClass//A framework to define a characters race.
        {
            string Name { get{return GetType().Name;}  }
            int Strength { get; }
            int Agility { get; }
            int Intelligence { get; }
            int InitialHP { get; }
        }


        public class Human : RaceClass
        {
            public int Strength { get { return 5;}}
            public int Agility{ get { return 5;}}
            public int Intelligence{ get { return 6;}}
            public int InitialHP{ get { return 90;}}
        }
        public class Fairy : RaceClass
        {
            public int Strength{ get { return 2;}}
            public int Agility{ get { return 5;}}
            public int Intelligence{ get { return 9;}}
            public int InitialHP{ get { return 60;}}
        }
        public class Orc : RaceClass
        {
            public int Strength{ get { return 11;}}
            public int Agility{ get { return 3;}}
            public int Intelligence{ get { return 2;}}
            public int InitialHP{ get { return 130;}}
        }
        public class Elf : RaceClass
        {
            public int Strength{ get { return 3;}}
            public int Agility{ get { return 7;}}
            public int Intelligence{ get { return 6;}}
            public int InitialHP{ get { return 90;}}
        }

        public interface TypeClass //A framework to define a characters type.
        {
            string Name { get{return GetType().Name;}  }
            public double InitialXP { get; }
            public string AttackMsg { get; }
            public string DefendMsg { get; }
            public double Attack_Formula(int Strength, int Agility, int Intelligence);
            public double Defend_Formula(int Strength, int Agility, int Intelligence);
            
        }
        public class Basic : TypeClass 
        {
            public double InitialXP { get { return 0.7;}}
            public string AttackMsg { get { return " tries to kick";}}
            public string DefendMsg{ get { return " rolls into a ball";}}
            public double Attack_Formula(int s, int a, int i){ return 0.3*s+0.3*a+0.3*i;}
            public double Defend_Formula(int s, int a, int i){return  0.3*s+0.3*a+0.3*i;}
        }
        public class Warrior : TypeClass
        {
            public double InitialXP {get{ return 0.5;}}
            public string AttackMsg {get{ return " swings a mighty sword";}}
            public string DefendMsg {get{ return " blocks with a shield";}}
            public double Attack_Formula(int s, int a, int i){ return 0.6*s+0.3*a+0.1*i;}
            public double Defend_Formula(int s, int a, int i){ return 0.6*s+0.5*a+0.2*i;}
        }
        public class Mage : TypeClass
        {
            public double InitialXP {get{ return 0.3;}}
            public string AttackMsg {get{ return " casts a fireball";}}
            public string DefendMsg {get{ return " creates a magical barrier";}}
            public double Attack_Formula(int s, int a, int i){ return 0.1*s+0.2*a+1*i;}
            public double Defend_Formula(int s, int a, int i){ return 0.1*s+0.4*a+0.8*i;}
        }
        public class Archer : TypeClass
        {
            public double InitialXP {get{ return 0.3;}}
            public string AttackMsg {get{ return " shoots an arrow";}}
            public string DefendMsg {get{ return " dodges swiftly";}}
            public double Attack_Formula(int s, int a, int i){ return 0.4*s+0.7*a+0.2*i;}
            public double Defend_Formula(int s, int a, int i){ return 0.2*s+0.7*a+0.4*i;}
        }

        public class Character 
        {
            public readonly string Name;//Will be accessed
            public readonly RaceClass Race;//Will be accessed
            public readonly TypeClass Type;//Will be accessed
            public int HP;//Will be accessed and changed
            public double XP;//Will be accessed and changed
            private string AttackMsg;//Will be accessed
            private string DefendMsg;//Will be accessed
            public int Kills;//Will be accessed and changed

            public Character(string InputName,RaceClass InputRace,TypeClass InputType)
            {
                Name = InputName;
                Race = InputRace;
                Type = InputType;
                HP = Race.InitialHP;
                XP = Type.InitialXP;
                AttackMsg = Name + Type.AttackMsg;
                DefendMsg = Name + Type.DefendMsg;
                Kills = 0;                
            }    

            public double DefendPts()
            {
                System.Console.WriteLine($"{DefendMsg}");//Easier to print the Msg here than after/before DefendPts is called. Could easily be moved
                Random rnd = new Random();
                return Type.Defend_Formula(Race.Strength,Race.Agility,Race.Intelligence)*rnd.NextDouble();
            }
            public double AttackPts()
            {
                System.Console.WriteLine($"{AttackMsg}");//Easier to print the Msg here than after/before AttckPts is called. Could easily be moved
                Random rnd = new Random();
                return Type.Attack_Formula(Race.Strength,Race.Agility,Race.Intelligence)*rnd.NextDouble();
            }

            public string PrintSheet()//Added kills to make sure xp lined up
            {
                return "NAME: " + Name + "\nXP: " + XP + "\nHP: " + HP + "\nRace: " + Race.Name + "\nType: " + Type.Name+ "\nKills: " + Kills;
            }
        }
        /*
        class Program 
        {
            static void Main()//Test function to see if function calls works
            {
                
                Character character1 = new Character("Bob",new Human(),new Warrior());
                System.Console.WriteLine($"{character1.PrintSheet()}");
                System.Console.WriteLine($"Attack roll: {character1.AttackPts()}");
                System.Console.WriteLine($"Defence roll: {character1.DefendPts()}");
                
            }
        }
        */
    }
    namespace Task2
    {
        using Task1;
        class Program
        {
            public static List<Task1.Character> Roster;
            static void Main()
            {
                Roster = new List<Task1.Character>();
                Random rnd = new Random();
                for (int i = 0; i < 20; i++)//Easily change the nr of randomised combatants
                {
                    int TypeI = rnd.Next(1,4);
                    Task1.TypeClass Type;
                    int RaceI = rnd.Next(1,4);
                    Task1.RaceClass Race;

                    
                    switch (TypeI)
                    {
                        default:Type = new Basic();
                        break;
                        case 2:Type = new Warrior();
                        break;
                        case 3:Type = new Mage();
                        break;
                        case 4:Type = new Archer();
                        break;
                    }
                    switch (RaceI)
                    {
                        default:Race = new Human();
                        break;
                        case 2:Race = new Fairy();
                        break;
                        case 3:Race = new Orc();
                        break;
                        case 4:Race = new Elf();
                        break;
                    }
                    string Name = "The " + Type.Name + " "+ Race.Name +" Nr "+ i.ToString();
                    Roster.Add(new Character(Name,Race,Type));   
                }
                foreach (var Fighter in Roster)
                {
                    System.Console.WriteLine(Fighter.Name);
                }
                while(Roster.Count>1)
                {
                    int P1,P2;
                    P1 = rnd.Next(0,Roster.Count-1);
                    P2 = rnd.Next(0,Roster.Count-1);
                    if(P1 == P2)
                    {
                        P2 = P2+1%Roster.Count;//Ensures the same character doesn't fight themselves
                    }
                    int P1_StartingHealth,P2_StartingHealth;//Save the starting health to restore it after the fight
                    P1_StartingHealth = Roster[P1].HP;
                    P2_StartingHealth = Roster[P2].HP;
                    System.Console.WriteLine($"{Roster[P1].Name} VS {Roster[P2].Name}");

                    double Atk,Def;
                    while(true)//Could make help function run it twice with switched inputs
                    {
                        System.Console.WriteLine($"\n\n\n\n\n");
                        


                        Atk = Roster[P1].AttackPts();//Also prints attackMsg
                        Def = Roster[P2].DefendPts();//Also prints defendMsg
                        if((int)Def<(int)Atk)//Cast it to ints due to HP being an int.
                        {
                            System.Console.WriteLine($"{Roster[P2].Name} took a hit");

                            Roster[P2].HP -= (int)(Atk - Def);//Deal damage
                            if(Roster[P2].HP<=0)
                            {
                                System.Console.WriteLine($"{Roster[P2].Name} Died");
                                Roster[P1].HP = P1_StartingHealth;//Restore victor
                                Roster[P1].XP = Math.Min(Roster[P1].XP+0.05,1);//Add XP to victor
                                Roster[P1].Kills++;//increase killcount
                                Roster.RemoveAt(P2);//Remove loser
                                break;//move outside fight loop
                            }
                        }
                        Atk = Roster[P2].AttackPts();//Also prints attackMsg
                        Def = Roster[P1].DefendPts();//Also prints defendMsg
                        if((int)Def<(int)Atk)//Cast it to ints due to HP being an int.
                        {
                            System.Console.WriteLine($"{Roster[P1].Name} took a hit");

                            Roster[P1].HP -= (int)(Atk - Def);//Deal damage
                            if(Roster[P1].HP<=0)
                            {
                                System.Console.WriteLine($"{Roster[P1].Name} Died");
                                Roster[P2].HP = P2_StartingHealth;//Restore victor
                                Roster[P2].XP = Math.Min(Roster[P2].XP+0.05,1);//Add XP to victor
                                Roster[P2].Kills++;//increase killcount
                                Roster.RemoveAt(P1);//Remove loser
                                break;//move outside fight loop
                            }
                        }
                        System.Console.WriteLine($"{Roster[P1].Name} has {Roster[P1].HP} HP left");
                        System.Console.WriteLine($"{Roster[P2].Name} has {Roster[P2].HP} HP left");
                    }

                }
                System.Console.WriteLine($"The winner is\n{Roster[0].PrintSheet()}");//Pronounce winner of tournament
            }
        }
    }
}